using Facepunch.Entities;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Text;
using System.IO;

namespace Facepunch
{
    public class FPContext
    {
        const string FORUMBASE = "facepunchforum.azurewebsites.net";
        const string FORUMURI = "http://" + FORUMBASE;
        private CookieContainer _cookies;

        #region Sessions
        /// <summary>
        /// Set the login token. This will allow privileged requests to occur.
        /// </summary>
        /// <param name="token">The login token</param>
        public void SetLoginToken(string token)
        {
            this._makeCookies(token);
        }

        /// <summary>
        /// Fetch the current session. Must have set a login token.
        /// </summary>
        /// <returns></returns>
        public FPSession GetCurrentSession()
        {
            return ApiPOST<FPSession>("session/current", null);
        }
        #endregion

        #region Forums
        /// <summary>
        /// Fetch all forums
        /// </summary>
        public FPForum[] ListForums()
        {
            return ApiGET<FPList<FPForum>>("forum/list").Result;
        }

        /// <summary>
        /// Create a forum with the specified name
        /// </summary>
        /// <param name="name"></param>
        public FPForum CreateForum(string name)
        {
            string postData = JsonConvert.SerializeObject(new FPForum() { Name = name });
            return ApiPOST<FPForum>("forum/create", postData);
        }

        /// <summary>
        /// Get the specified forum's settings
        /// </summary>
        /// <param name="shortName">The shortName of the forum to fetch the settings of</param>
        public FPForum.FPForumSettings GetForumSettings(string shortName)
        {
            return ApiGET<FPForum.FPForumSettings>("forum/settings/" + shortName + "/get");
        }

        /// <summary>
        /// Get the specified forum
        /// </summary>
        /// <param name="shortName">The shortName of the forum to fetch</param>
        public FPForum GetForum(string shortName)
        {
            return ApiGET<FPForum>("forum/info/" + shortName);
        }

        /// <summary>
        /// Save the specified forum's settings
        /// </summary>
        /// <param name="shortName">The shortname of the forum</param>
        /// <param name="settings">The new settings for the forum</param>
        /// <returns>The new forum settings</returns>
        public FPForum.FPForumSettings SaveForumSettings(string shortName, FPForum.FPForumSettings settings)
        {
            string postData = JsonConvert.SerializeObject(settings);
            return ApiPOST<FPForum.FPForumSettings>("forum/settings/" + shortName + "/save", postData);
        }
        #endregion

        #region Threads
        /// <summary>
        /// Fetch threads from the specified forum
        /// </summary>
        /// <param name="shortName">The shortname of the forum</param>
        /// <param name="start">The amount of threads to skip</param>
        /// <param name="num">The amount of threads to fetch</param>
        public FPThread[] ListThreads(string shortName, int start = 0, int num = 20)
        {
            return ApiGET<FPList<FPThread>>("thread/list/" + shortName + "/" + num + "/" + start).Result;
        }

        /// <summary>
        /// Fetch the read thread list. Must be logged in.
        /// </summary>
        /// <param name="start">The amount of threads to skip</param>
        /// <param name="num">The amount of threads to fetch</param>
        public FPThread[] GetReadThreads(int start = 0, int num = 25) 
        {
            return ApiGET<FPList<FPThread>>("/thread/list/special/read/" + num + "/" + start).Result;
        }

        /// <summary>
        /// Fetch the subscribed thread list. Must be logged in.
        /// </summary>
        /// <param name="start">The amount of threads to skip</param>
        /// <param name="num">The amount of threads to fetch</param>
        public FPThread[] GetSubscribedThreads(int start = 0, int num = 25)
        {
            return ApiGET<FPList<FPThread>>("/thread/list/special/subscribed/" + num + "/" + start).Result;
        }

        /// <summary>
        /// Fetches the specified thread
        /// </summary>
        /// <param name="threadId">The Id of the thread to fetch</param>
        public FPThread GetThread(int threadId)
        {
            return GetThread(Util.ConvertToBase36(threadId));
        }

        /// <summary>
        /// Fetches the specified thread
        /// </summary>
        /// <param name="base36ThreadId">The base-36 Id of the thread to fetch</param>
        public FPThread GetThread(string base36ThreadId)
        {
            return ApiGET<FPThread>("thread/info/" + base36ThreadId);
        }

        /// <summary>
        /// Posts a thread in the specified forum
        /// </summary>
        /// <param name="forumName">The shortname of the forum to post the thread in</param>
        /// <param name="title">The title of the thread</param>
        /// <param name="text">The body of the thread</param>
        /// <param name="icon">The icon of the thread</param>
        public FPThread.FPPostThreadResponse PostThread(string forumName, string title, string text, string icon = "2笠籧ᔢ簧硼⚨㐴ᠣ㙉ល曩䣬〺伤柌⣳㡿ૠ⏠᝻㞌㛚㈊㛧㚊恕垬㝡尳⋈ᤅҨ嘸ࠠ ")
        {
            var newThread = new FPThread.FPPostThreadRequest();
            newThread.Icon = icon;
            newThread.Title = title;
            newThread.Text = text;
            string postData = JsonConvert.SerializeObject(newThread);

            return ApiPOST<FPThread.FPPostThreadResponse>("post/put/" + forumName + "/0", postData);
        }
        #endregion

        #region Posts
        /// <summary>
        /// Fetch posts from the specified thread
        /// </summary>
        /// <param name="threadId">The ID of the thread</param>
        /// <param name="start">The starting index</param>
        /// <param name="num">The amount of posts to fetch</param>
        public FPPost[] ListPosts(int threadId, int start = 0, int num = 20)
        {
            return ListPosts(Util.ConvertToBase36(threadId), start, num);
        }

        /// <summary>
        /// Fetch posts from the specified thread
        /// </summary>
        /// <param name="base36Id">The base-36 ID of the thread</param>
        /// <param name="start">The starting index</param>
        /// <param name="num">The amount of posts to fetch</param>
        public FPPost[] ListPosts(string base36Id, int start = 0, int num = 20)
        {
            return ApiGET<FPList<FPPost>>("post/get/" + base36Id + "/" + num + "/" + start).Result;
        }
        #endregion

        #region Icons
        /// <summary>
        /// Fetch the specified icon
        /// </summary>
        /// <param name="name">The icon name to fetch</param>
        public FPIcon GetIcon(string name)
        {
            var icon = ApiGET<FPIcon>("icon/get/" + name);
            icon.Name = name;
            return icon;
        }
        #endregion

        /// <summary>
        /// Perform a GET request on the API
        /// </summary>
        /// <typeparam name="T">The type of entity to expect</typeparam>
        /// <param name="url">The url to fetch</param>
        public T ApiGET<T>(string url) where T : FPEntity
        {
            return ApiRequest<T>(url, "GET", null);
        }

        /// <summary>
        /// Perform a POST request on the API
        /// </summary>
        /// <typeparam name="T">The type of entity to expect</typeparam>
        /// <param name="url">The url to POST</param>
        /// <param name="postParams">The parameters to POST</param>
        public T ApiPOST<T>(string url, string postParams) where T : FPEntity
        {
            return ApiRequest<T>(url, "POST", postParams);
        }

        /// <summary>
        /// Perform a request on the API
        /// </summary>
        /// <typeparam name="T">The type of entity to expect</typeparam>
        /// <param name="url">The url to query</param>
        /// <param name="postParams"></param>
        internal T ApiRequest<T>(string url, string method, string postParams = null) where T : FPEntity
        {
            url = "http://facepunchforum.azurewebsites.net/api/" + url;
            string webResponse;
            try
            {
                webResponse = _makeRequest(url, method, postParams);
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                if (response == null)
                    throw new FPException("Null response from server");
                webResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

                try
                {
                    FPError parsedError = JsonConvert.DeserializeObject<FPError>(webResponse);
                    if (String.IsNullOrEmpty(parsedError.Message))
                        throw new FPException("Empty response from server");

                    throw new FPException(parsedError.Message);
                }
                catch (JsonException)
                {
                    webResponse = null;
                }
            }

            if (webResponse == null || webResponse.Equals("null"))
                throw new FPException("Null response from server");

            if (!typeof(IFPPlaintextEntity).IsAssignableFrom(typeof(T)))
            {
                try
                {
                    T parsedresponse = JsonConvert.DeserializeObject<T>(webResponse);
                    parsedresponse.ApplyContext(this);
                    return parsedresponse;
                }
                catch (JsonException e)
                {
                    throw new FPException("Error parsing response");
                }
            }

            T plaintextEntity = Activator.CreateInstance<T>();
            ((IFPPlaintextEntity)plaintextEntity).HandlePlaintext(webResponse);
            plaintextEntity.ApplyContext(this);

            return plaintextEntity;
        }

        private string _makeRequest(string url, string method, string postData = null)
        {
            if (_cookies == null)
                _makeCookies();

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = method;

            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=UTF-8";
            request.Headers["X-XSRF-TOKEN"] = "413149147";
            request.Host = FORUMBASE;
            request.Headers["Origin"] = FORUMURI;
            request.Referer = FORUMURI;
            request.Timeout = 15000;

            // Cookies
            request.CookieContainer = _cookies;

            request.ContentLength = 0;

            // Request data
            if (postData != null && method == "POST")
            {
                byte[] dataBytes = Encoding.ASCII.GetBytes(postData);
                request.ContentLength = dataBytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            // Get the response
            var response = request.GetResponse() as HttpWebResponse;
            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        private void _makeCookies(string tk = null)
        {
            this._cookies = new CookieContainer();

            _cookies.Add(new Uri(FORUMURI), new Cookie("XSRF-TOKEN", "413149147"));
            if (tk != null)
            {
                var tkToken = new Cookie("tk", tk);
                //tkToken.Domain = FORUMBASE;
                tkToken.HttpOnly = true;
                tkToken.Path = "/";

                _cookies.Add(new Uri(FORUMURI), tkToken);
            }
        }
    }
}

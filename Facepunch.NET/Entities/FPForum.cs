using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPForum : FPEntity
    {
        [JsonProperty("ForumId")]
        public int ForumId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShortName")]
        public string ShortName { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("ThreadCount")]
        public int ThreadCount { get; set; }

        [JsonProperty("PostCount")]
        public int PostCount { get; set; }

        [JsonProperty("MemberCount")]
        public int MemberCount { get; set; }

        [JsonProperty("Membership")]
        public int Membership { get; set; }

        [JsonProperty("Permissions")]
        public int Permissions { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Fetch threads from this forum
        /// </summary>
        /// <param name="start">The amount of threads to skip</param>
        /// <param name="num">The amount of threads to fetch</param>
        /// <returns>An array of threads</returns>
        public FPThread[] ListThreads(int start = 0, int num = 20)
        {
            return Context.ListThreads(this.ShortName, start, num);
        }

        /// <summary>
        /// Posts a thread in this forum
        /// </summary>
        /// <param name="title">The title of the thread</param>
        /// <param name="text">The body of the thread</param>
        /// <param name="icon">The icon of the thread</param>
        public FPThread.FPPostThreadResponse PostThread(string title, string text, string icon = "2笠籧ᔢ簧硼⚨㐴ᠣ㙉ល曩䣬〺伤柌⣳㡿ૠ⏠᝻㞌㛚㈊㛧㚊恕垬㝡尳⋈ᤅҨ嘸ࠠ ")
        {
            return this.Context.PostThread(this.ShortName, title, text, icon);
        }

        public class FPForumSettings : FPEntity
        {
            [JsonProperty("ForumId")]
            public int ForumId { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("ShortName")]
            public string ShortName { get; set; }

            [JsonProperty("Icon")]
            public string Icon { get; set; }

            [JsonProperty("Stylesheet")]
            public string Stylesheet { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("Permissions")]
            public int Permissions { get; set; }
        }
    }
}

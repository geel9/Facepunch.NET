﻿using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPThread : FPEntity
    {
        [JsonProperty("ThreadId")]
        public int ThreadId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("Flags")]
        public int Flags { get; set; }

        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("Updated")]
        public string Updated { get; set; }

        [JsonProperty("PostCount")]
        public int PostCount { get; set; }

        [JsonProperty("Author")]
        public FPUser Author { get; set; }

        [JsonProperty("Read")]
        public FPThreadRead Read { get; set; }

        [JsonProperty("Forum")]
        public FPForum Forum { get; set; }

        /// <summary>
        /// Calculate the amount of posts in this thread that have not been read
        /// </summary>
        public int GetNumUnreadPosts()
        {
            if (this.Read == null) return -1;

            return this.PostCount - this.Read.PostsRead;
        }

        /// <summary>
        /// Fetch posts from this thread
        /// </summary>
        /// <param name="base36Id">The base-36 ID of the thread</param>
        /// <param name="start">The starting index</param>
        /// <param name="num">The amount of posts to fetch</param>
        public FPPost[] ListPosts(int start = 0, int num = 20)
        {
            return this.Context.ListPosts(this.ThreadId, start, num);
        }

        public class FPThreadRead : FPEntity
        {
            [JsonProperty("PostsRead")]
            public int PostsRead { get; set; }

            [JsonProperty("LastRead")]
            public string LastRead { get; set; }
        }

        public class FPPostThreadRequest
        {
            [JsonProperty("Icon")]
            public string Icon { get; set; }

            [JsonProperty("Title")]
            public string Title { get; set; }

            [JsonProperty("Text")]
            public string Text { get; set; }
        }

        public class FPPostThreadResponse : FPEntity
        {
            [JsonProperty("ForumId")]
            public string ForumId { get; set; }

            [JsonProperty("Thread")]
            public FPThread Thread { get; set; }
        }
    }
}

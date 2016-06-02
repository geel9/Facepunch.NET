using Facepunch;
using System;

namespace FacepunchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseIcon = "2笠籧ᔢ簧硼⚨㐴ᠣ㙉ល曩䣬〺伤柌⣳㡿ૠ⏠᝻㞌㛚㈊㛧㚊恕垬㝡尳⋈ᤅҨ嘸ࠠ ";

            FPContext fp = new FPContext();
            var f = fp.GetForum("facepunchdotnet");

            var forums = fp.ListForums();

            foreach(var forum in forums)
            {
                Console.WriteLine(forum.Name);
                var threads = forum.ListThreads(0, 1);
                foreach(var thread in threads)
                {
                    Console.WriteLine("--" + thread.Name);
                }
            }

            Console.ReadLine();
        }
    }
}

using Facepunch;
using System;

namespace FacepunchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseIcon = "ဤ㘌័௧ϸߑ╡瞠䣘ࡌ፮㖍֮ᘒ⍐汮浮㱡唣ॷ嵩勅䆃ᕐ焷〾暴䨁㤓晬⧡ᆿ㽹滹அ㣫⍮ồⵆ㢰滚ᆢغ᭢Р ";

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

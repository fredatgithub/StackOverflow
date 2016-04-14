using System;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var webRoot = Path.Combine(contentRoot, "wwwroot");
            var dataRoot = Path.Combine(contentRoot, "appdata");
            
            Console.WriteLine("---");
            Console.WriteLine($"contentRoot:{contentRoot}");
            Console.WriteLine($"webRoot:{webRoot}");
            Console.WriteLine($"dataRoot:{dataRoot}");
            Console.WriteLine("---");
        }
    }
}

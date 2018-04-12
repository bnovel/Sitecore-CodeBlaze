using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://tcom-poc-icrs-int-468080-cd.azurewebsites.net/";
            var segments = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            segments = new String[] { };
            if (segments[0].ToLower().Equals("espanol"))
            {
                Console.WriteLine("espanol");
            }
            else
            {
                Console.WriteLine("English");
            }
            Console.Read();
        }
    }
}

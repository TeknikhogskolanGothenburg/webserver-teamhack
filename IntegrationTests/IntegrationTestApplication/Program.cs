using IntegrationTests;
using System;

namespace IntegrationTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting integration tests");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var ressource = new RessourceTest();
                TestRunner.RunTheMethod(ressource.Webserver_NonExsitingFile_200);
                TestRunner.RunTheMethod(ressource.Webserver_RootIndexHtml_200);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tests ran in "+elapsedMs+ " ms");
            Console.ReadKey();
        }
    }
}

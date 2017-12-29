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

            var ressource = new RessourceTest();
            TestRunner.RunTheMethod(ressource.Ressource_Root_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_Index_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_NonExsiting_NotFound);
            TestRunner.RunTheMethod(ressource.Ressource_AnotherPage_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_Document_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_CatGif_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_PandaJpg_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_Script_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_Style_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_Subfolder_Ok);
            TestRunner.RunTheMethod(ressource.Ressource_SubfolderIndex_Ok);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tests ran in "+elapsedMs+ " ms");
            Console.ReadKey();
        }
    }
}

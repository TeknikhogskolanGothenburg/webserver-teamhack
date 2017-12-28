using System;

namespace IntegrationTestApplication
{
    public static class TestRunner
    {
        public static void RunTheMethod(Action testMethod)
        {
            var testWatch = System.Diagnostics.Stopwatch.StartNew();
            bool testCompleted = false;
            Exception failException = null;
            try
            {
            
                testMethod();
                testCompleted = true;
            }
            catch(Exception ex)
            {
                failException = ex;
                testCompleted = false;
            }

            testWatch.Stop();
            var elapsedMs = testWatch.ElapsedMilliseconds;
            string methidName = testMethod.Method.Name ;
            if (testCompleted)
            {
                OnExecutionComplete(elapsedMs, methidName);
            }
            else
            {
                OnTestFailed(elapsedMs, methidName, failException);
            }
        }

        static void OnExecutionComplete(Int64 executionTimeMs, string testName) //ExecutionCompleteInfo info)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[OK] "+testName+" ("+executionTimeMs+" ms)");
            Console.ResetColor();
        }

        static void OnTestFailed(Int64 executionTimeMs, string testName, Exception exception)//TestFailedInfo info)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[FAIL] " + testName + " (" + executionTimeMs + " ms)");
            Console.WriteLine("\t" + exception.Message);
            Console.ResetColor();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server2
{
    class Program
    {
        private static String ContentFolderName = "Content";
        private static String[] FileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar

        static void Main(string[] prefixes)
        {
            //https://github.com/skjohansen/AssignmentWebserver/blob/master/Hints.md
            //https://msdn.microsoft.com/en-us/library/system.net.httplistener(v=vs.110).aspx
            bool printFiles = true;
            string[] urls = new string[FileEntries.Length];
            if (printFiles)
            {
                Console.WriteLine("Files in " + ContentFolderName + " folder:");
            }
            for (int i = 0; i < FileEntries.Length; i++)
            {
                if (printFiles)
                {
                    Console.WriteLine(FileEntries[i]);
                }
                urls[i] = "http://localhost:8080/" + FileEntries[i].Substring(ContentFolderName.Length + 1).Replace('\\', '/') + "/";
            }
            if (printFiles)
            {
                Console.WriteLine();
            }
            SimpleListenerExample(urls);
        }

        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                //string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                //string responseString = File.ReadAllText(@fileEntries[i]);
                Console.WriteLine("Current page: " + request.RawUrl);
                string responseString = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            //listener.Stop();
        }
    }
}
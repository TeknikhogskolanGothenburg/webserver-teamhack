using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server2
{
    class Program
    {
        private static string ContentFolderName = "Content";
        private static string[] PictureExtensions = new String[] { ".png", ".jpg", ".gif" };
        private static string[] FileExtensions = new String[] { ".pdf" };

        static void Main(string[] prefixes)
        {
            //https://github.com/skjohansen/AssignmentWebserver/blob/master/Assignment.md
            //https://github.com/skjohansen/AssignmentWebserver/blob/master/Hints.md
            //https://msdn.microsoft.com/en-us/library/system.net.httplistener(v=vs.110).aspx
            bool printFiles = true;
            string[] fileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar
            string[] urls = new string[fileEntries.Length];
            if (printFiles)
            {
                Console.WriteLine("Files in " + ContentFolderName + " folder:");
            }
            for (int i = 0; i < fileEntries.Length; i++)
            {
                if (printFiles)
                {
                    Console.WriteLine(fileEntries[i]);
                }
                urls[i] = "http://localhost:8080/" + fileEntries[i].Substring(ContentFolderName.Length + 1).Replace('\\', '/') + "/";
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
                try
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
                    string resourcePath = Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl;
                    string responseString = File.ReadAllText(resourcePath);
                    if (StringEndsWith(request.RawUrl, PictureExtensions))
                    {
                        response.ContentType = "image/gif";
                    }
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    if (StringEndsWith(request.RawUrl, PictureExtensions) || StringEndsWith(request.RawUrl, FileExtensions))
                    {

                    }
                    else
                    {
                        // Get a response stream and write the response to it.
                        response.ContentLength64 = buffer.Length;
                        Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        // You must close the output stream.
                        output.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //listener.Stop();
        }

        public static bool StringEndsWith(String input, String[] endSequences)
        {
            if (input != null && endSequences != null)
            {
                foreach (String end in endSequences)
                {
                    if (input.EndsWith(end))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static string[] fileEntries;
        static void Main(string[] prefixes)
        {
            //https://msdn.microsoft.com/en-us/library/system.net.httplistener(v=vs.110).aspx
            
            fileEntries = Directory.GetFiles("Content");
            string[] URL = new string[fileEntries.Length];
            for (int i = 0; i < fileEntries.Length; i++)
            {
                URL[i] = "http://localhost:8080/"+fileEntries[i].Substring(8)+"/";

            }
            SimpleListenerExample(URL);
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
            
            for (int i = 0; i < fileEntries.Length; i++)
            {
                listener.Start();
                Console.WriteLine("Listening...");


                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                //string[] fileEntries = Directory.GetFiles("Content");
                //foreach (string i in fileEntries)
                //{


                string responseString = File.ReadAllText(@fileEntries[i]);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                listener.Stop();
                //}
            }
            
        }
    }
}
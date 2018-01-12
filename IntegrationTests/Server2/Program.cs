using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server2
{
    class Program
    {
        private static string ContentFolderName = "Content";

        static void Main(string[] prefixes)
        {
            // https://msdn.microsoft.com/en-us/library/system.net.httplistener(v=vs.110).aspx
            // https://github.com/skjohansen/AssignmentWebserver/blob/master/Hints.md
            // https://github.com/skjohansen/AssignmentWebserver/blob/master/Assignment.md
            // When making a request to a resouse at the webserver (eg. a file in the content folder) should the following HTTP header be set to the correct value:
            // ✓ ContentType, the correct content type of the file (eg. text/html)
            // ✓ Etag, should be implemented as a MD5 hash of the file content
            // ✓ Expires, should be set to one year from "now"
            // ?  StatusCodes, a usable status code should be returned (eg. 200 OK)
            // x  Cookie, the server should return a cookie (see cookie subsection)
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
            
            Console.WriteLine("Listening...");
            listener.Start();
            while (true)
            {
                try
                {
                    // Note: The GetContext method blocks while waiting for a request.
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;

                    string resourcePath = Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl;
                    string contentType = GetContentType(request.RawUrl);
                    string hash = ComputeHash(resourcePath);
                    response.AddHeader("Content-Type", contentType);
                    response.AddHeader("ETag", hash);
                    response.AddHeader("Expires", (60*60*24*365).ToString()); // ett år är innehållet cachat

                    Console.WriteLine("Current resource: " + request.RawUrl + " (type: " + contentType + ", hash: " + hash + ")");
                    //string responseString = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl);
                  
                    byte[] buffer = File.ReadAllBytes(resourcePath);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //listener.Stop();
        }

        private static string GetContentType(string resource)
        {
            if (resource != null)
            {
                resource = resource.ToLower();
                if (IsPicture(resource))
                {
                    return "image/" + resource.Substring(resource.Length-3);
                }
                else if (resource.EndsWith(".pdf"))
                {
                    return "application/pdf";
                }
                else if (resource.EndsWith(".css"))
                {
                    return "text/css";
                }
                else if (resource.EndsWith(".js"))
                {
                    return "text/javascript";
                }
            }
            return "text/html";
        }

        private static bool IsPicture(string resource)
        {
            if (resource != null)
            {
                string[] imageExtensions = new string[] { ".jpg", ".png", ".gif", ".bmp", ".tif" };
                for (int i = 0; imageExtensions.Length > i; i++)
                {
                    if (resource.EndsWith(imageExtensions[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static string ComputeHash(string filePath) // modifierat från https://stackoverflow.com/a/27481514
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(File.ReadAllBytes(filePath));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

    }
}
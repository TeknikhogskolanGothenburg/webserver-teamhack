using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Server2
{
    class Program
    {
        private static string ContentFolderName = "Content";
        private static bool PrintDebug = true;

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
            // ✓ Cookie, the server should return a cookie (see cookie subsection)
            
            string[] fileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar
            string[] urls = new string[fileEntries.Length];
            if (PrintDebug)
            {
                Console.WriteLine("Files in " + ContentFolderName + " folder:");
            }
            for (int i = 0; i < fileEntries.Length; i++)
            {
                if (PrintDebug)
                {
                    Console.WriteLine(fileEntries[i]);
                }
                urls[i] = "http://localhost:8080/" + fileEntries[i].Substring(ContentFolderName.Length + 1).Replace('\\', '/') + "/";
            }
            if (PrintDebug)
            {
                Console.WriteLine();
            }
            SimpleListenerExample(urls);
        }

        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (prefixes == null || prefixes.Length == 0) // URI prefixes are required, for example "http://contoso.com:8080/index/".
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
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
                    HttpListenerContext context = listener.GetContext(); // Note: The GetContext method blocks while waiting for a request.
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response; // Obtain a response object.

                    Console.WriteLine("Current resource: " + request.RawUrl);
                    string filePath = Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl;
                    UpdateCounterCookie(request, response, PrintDebug);
                    AddHeaders(response, filePath, PrintDebug);

                    //string responseString = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl);
                    byte[] buffer = File.ReadAllBytes(filePath);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //listener.Stop();
        }

        private static void AddHeaders(HttpListenerResponse response, string filePath, bool printHeaders)
        {
            response.AddHeader("Content-Type", MimeMapping.GetMimeMapping(filePath));
            response.AddHeader("ETag", ComputeHash(filePath));
            response.AddHeader("Expires", ToHTTPDate(DateTime.Now.AddYears(1)));
            if (printHeaders)
            {
                Console.WriteLine(response.Headers);
            }
        }

        private static void UpdateCounterCookie(HttpListenerRequest request, HttpListenerResponse response, bool printValue)
        {
            Cookie cookie = request.Cookies["CounterCookie"];
            if (cookie != null)
            {
                cookie.Value = (Int32.Parse(cookie.Value) + 1).ToString();
            }
            else
            {
                cookie = new Cookie("CounterCookie", "1");
            }
            response.Cookies.Add(cookie);
            if (printValue)
            {
                Console.WriteLine("Cookie counter: " + cookie.Value);
            }
        }

        private static String ToHTTPDate(DateTime date) // taget från https://stackoverflow.com/a/13089
        {
            return date.ToUniversalTime().ToString("r");
        }

        private static string ComputeHash(string filePath) // modifierat från https://stackoverflow.com/a/27481514
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(File.ReadAllBytes(filePath));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        //private static string GetContentType(string filePath)
        //{
        //    if (filePath != null)
        //    {
        //        filePath = filePath.ToLower();
        //        if (IsPicture(filePath))
        //        {
        //            return "image/" + filePath.Substring(filePath.Length - 3);
        //        }
        //        else if (filePath.EndsWith(".pdf"))
        //        {
        //            return "application/pdf";
        //        }
        //        else if (filePath.EndsWith(".css"))
        //        {
        //            return "text/css";
        //        }
        //        else if (filePath.EndsWith(".js"))
        //        {
        //            return "text/javascript";
        //        }
        //    }
        //    return "text/html";
        //}

        //private static bool IsPicture(string filePath)
        //{
        //    if (filePath != null)
        //    {
        //        string[] imageExtensions = new string[] { ".jpg", ".png", ".gif", ".bmp", ".tif" };
        //        for (int i = 0; imageExtensions.Length > i; i++)
        //        {
        //            if (filePath.EndsWith(imageExtensions[i]))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

    }
}
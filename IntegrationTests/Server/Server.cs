using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private static HttpListenerContext context { get; set; }
        private static String ContentFolderName = "Content";
       

        public static void RunServer()
        {
            StartServer();

        }

        private static void StartServer()
        {
            try
            {

                SimpleListenerExample("http://localhost:8080/");

            }
            catch (Exception e)
            {
                if (!Directory.Exists("Content"))
                {
                    CreateDirectory();
                }
                else
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }

            }


        }
        private static void CreateDirectory()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Content Directory Was Deleted");
            Console.ForegroundColor = ConsoleColor.White;
            Directory.CreateDirectory("Content");
            Console.WriteLine("New Content Directory Was Created");
            Console.ReadKey();
        }


        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string prefix)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (prefix == null)
            {
                throw new ArgumentException("prefix");
            }

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            Console.WriteLine("Listening...");

            listener.Start();
            byte[] buffer;


            while (true)
            {
                try
                {
                    // Note: The GetContext method blocks while waiting for a request.
                    context = listener.GetContext();
                    
                    // Obtain a response object.

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Current page: " + context.Request.RawUrl);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Status Code ");
                    Console.WriteLine(DateTime.UtcNow.AddYears(1).ToString("o"));
                    Console.WriteLine(context.Response.StatusCode);
                    if (context.Request.RawUrl == "/" || context.Request.RawUrl == "/index"|| Directory.Exists(Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl))
                    {
                        Redirect(context);
                    }
                    else
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl))
                        {
                            string filePath = Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl;
        
                            context.Response.AddHeader("ETag", ComputeHash(filePath));
                            context.Response.AddHeader("Expires", DateTime.UtcNow.AddYears(1).ToString("o")); // detta funkar 
                            context.Response.AddHeader("Content-Type", GetContentType(context.Request.RawUrl));
                          


                            buffer = File.ReadAllBytes(filePath);
                            // Get a response stream and write the response to it.
                            context.Response.ContentLength64 = buffer.Length;
                            System.IO.Stream output = context.Response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                           
                            // You must close the output stream.
                            output.Close();
                            
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            Console.WriteLine("Not Found Object");
                            Console.WriteLine(context.Response.StatusCode);
                            context.Response.StatusDescription = "The link was broken";
                            string rstr = "<Html><Body><h1>The Website was not Found </h1></Body> </Html>";
                            buffer = Encoding.UTF8.GetBytes(rstr);
                            context.Response.ContentLength64 = buffer.Length;
                            System.IO.Stream output = context.Response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                            // You must close the output stream.
                            output.Close();

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                   
                }
            }
        }
       
        
        static void Redirect(HttpListenerContext context)
        {

            byte[] buffer;
            if (context.Request.RawUrl == "index.html") {
                if (File.Exists(ContentFolderName + "/index.html"))
                {
                    buffer = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/" + ContentFolderName + "/index.html");
                }
                else
                {
                    string rstr = "<Html><Body><h1>Server is Running </h1></Body> </Html>";
                    buffer = Encoding.UTF8.GetBytes(rstr);
                }
        }
            else
            {
                buffer = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl+"index.html");
            }
            // Get a response stream and write the response to it.
            context.Response.ContentLength64 = buffer.Length;
            System.IO.Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
        private static string GetContentType(string resource)
        {
            if (resource != null)
            {
                resource = resource.ToLower();
                if (IsPicture(resource))
                {
                    return "image/" + resource.Substring(resource.Length - 3);
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
                    return "application/javascript";
                   
                }
            }
            return "text/html";
        }

        private static bool IsPicture(string resource)
        {
            if (resource != null)
            {
                string[] imageExtensions = new string[] { ".jpeg", ".png", ".gif" , ".jpg" };
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








    }
}

    




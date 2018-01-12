using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private static String ContentFolderName = "Content";
        private static int w;

        public static  void RunServer()
        {
             GetPrefixes();
  
        }
      
        private static void GetPrefixes()
        {
            try
            {
                CreatePrefixes();


            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Content Directory Was Deleted");
                Console.ForegroundColor = ConsoleColor.White;
                Directory.CreateDirectory("Content");
                CreatePrefixes();
                Console.WriteLine("New Content Directory Was Created");
                Console.ReadKey();

            }


        }
        private static void CreatePrefixes()
        {
            string[] fileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar
            string[] urls = new string[fileEntries.Length];
            if (fileEntries.Length >= 1)
            {
                Console.WriteLine("Files in " + ContentFolderName + " folder:");

                for (int i = 0; i < fileEntries.Length; i++)
                {
                    if (ContentFolderName.Length >= 1)
                    {
                        Console.WriteLine(fileEntries[i]);
                    }
                    urls[i] = "http://localhost:8080/" + fileEntries[i].Substring(ContentFolderName.Length + 1).Replace('\\', '/') + "/";
                }

                SimpleListenerExample(urls);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not find any files!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You need to have at least one File to run the server");
                Console.WriteLine("Press any Key....");
                Console.ReadKey();



            }






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
            // Add the prefixes.//
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
                //Cookie 
                //    l= new Cookie("contoso", "123,456", "", "https://contoso.com");
                //CookieContainer e = new CookieContainer();
            }
            
                Console.WriteLine("Listening...");
                while (true)
                {
                    listener.Start();
                
                    // Note: The GetContext method blocks while waiting for a request.
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                // Obtain a response object.
                     HttpListenerResponse response = context.Response;
             

                Console.WriteLine("Current page: " + request.RawUrl);
              
                    byte[] buffer = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/" + ContentFolderName + request.RawUrl);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
         
            }
            }
        



    }
}

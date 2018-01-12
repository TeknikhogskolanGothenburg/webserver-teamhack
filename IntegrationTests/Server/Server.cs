﻿using System;
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

        public static void RunServer()
        {
            GetPrefixes();

        }

        private static void GetPrefixes()
        {
            try
            {
                CreatePrefixes();


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
            CreatePrefixes();
            Console.WriteLine("New Content Directory Was Created");
            Console.ReadKey();
        }
        private static void CreatePrefixes()
        {
            string[] fileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar
            List<string> urls = new List<string>();
            if (fileEntries.Length >= 1)
            {
                Console.WriteLine("Files in " + ContentFolderName + " folder:");

                //for (int i = 0; i < fileEntries.Length; i++)
                //{
                //    if (ContentFolderName.Length >= 1)
                //    {
                //        Console.WriteLine(fileEntries[i]);
                //    }
                //    urls.Add("http://localhost:8080/" + fileEntries[i].Substring(ContentFolderName.Length + 1).Replace('\\', '/') + "/");
                //}
                urls.Add("http://localhost:8080/");
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
        public static void SimpleListenerExample(List<string> prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Count == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.//
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
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    // Obtain a response object.

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Current page: " + context.Request.RawUrl);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Status Code ");
                    Console.WriteLine(context.Response.StatusCode);
                    if (context.Request.RawUrl == "/")
                    {
                        Redirect(context);
                    }
                    else
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl))
                        {
                            byte[] buffer = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/" + ContentFolderName + context.Request.RawUrl);
                            // Get a response stream and write the response to it.
                            context.Response.ContentLength64 = buffer.Length;
                            System.IO.Stream output = context.Response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                            // You must close the output stream.
                            output.Close();
                        }
                        else
                        {
                            Redirect(context);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        static void Redirect(HttpListenerContext context)
        {
            byte[] buffer;
            if (File.Exists(ContentFolderName + "/index.html"))
            {
                buffer = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/" + ContentFolderName + "/index.html");
            }
            else
            {
                string rstr = "<Html><Body><h1>Hello Wolrd </h1></Body> </Html>";
                buffer = Encoding.UTF8.GetBytes(rstr);
        }
        // Get a response stream and write the response to it.
        context.Response.ContentLength64 = buffer.Length;
            System.IO.Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }


    }
}
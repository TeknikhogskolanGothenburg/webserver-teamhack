﻿using System;
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
        private static String ContentFolderName = "Content";
        static void Main(string[] prefixes)
        {
            //https://github.com/skjohansen/AssignmentWebserver/blob/master/Hints.md
            //https://msdn.microsoft.com/en-us/library/system.net.httplistener(v=vs.110).aspx
            try
            {
                String[] fileEntries = Directory.GetFiles(ContentFolderName, "*", SearchOption.AllDirectories); // hämtar även filer i submappar
                string[] urls = new string[fileEntries.Length];
                if (ContentFolderName.Length >= 1)
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
                    Console.WriteLine("Could not find any files!");
                }
            }
            catch
            {
                Directory.CreateDirectory("Content");
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
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
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
                    listener.Stop();
                  
                }
            }
            
        }
    }



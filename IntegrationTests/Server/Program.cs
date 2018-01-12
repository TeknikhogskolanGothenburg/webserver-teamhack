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
       
        static void Main(string[] args)
        {
           
            try
            {
               Server.RunServer();
            }
            catch
            {
                Console.WriteLine("Could not run the server");
                Console.ReadKey();

            }
        }

      
    }
}


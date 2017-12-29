using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class DynamicTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod, Ignore]
        public void Dynamic_Add1And2_3AsText()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost+"dynamic?input1=1&input2=2");
            

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Arrange
            Assert.AreEqual(3, requestContent);   
        }

        [TestMethod, Ignore]
        public void Dynamic_Add1And2_3AsXml()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "dynamic?input1=1&input2=2");


            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Arrange
            Assert.AreEqual(3, requestContent);
        }

        [TestMethod, Ignore]
        public void Dynamic_PostFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "dynamic?input1=1&input2=2");


            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Arrange
            Assert.AreEqual(3, requestContent);
        }
    }
}

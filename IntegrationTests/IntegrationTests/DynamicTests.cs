using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class DynamicTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
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
            Assert.AreEqual("3", requestContent);   
        }

        [TestMethod]
        public void Dynamic_Add2And3_3AsXml()
        {
            // Arrange
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Localhost + "dynamic?input1=2&input2=3");
            myHttpWebRequest.Accept = "application/xml";

            // Act
            WebResponse response = myHttpWebRequest.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Arrange
            Assert.AreEqual("<result><value>5</value></result>", requestContent);
        }

        [TestMethod]
        public void Dynamic_JustOneParameter_InternalServerError()
        {
            // Arrange
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Localhost + "dynamic?input1=2");
  
            // Act
            WebResponse response = myHttpWebRequest.GetResponseNoException();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Arrange
            Assert.AreEqual(HttpStatusCode.InternalServerError, ((HttpWebResponse)response).StatusCode);
            Assert.AreEqual("Missing input value", requestContent);
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

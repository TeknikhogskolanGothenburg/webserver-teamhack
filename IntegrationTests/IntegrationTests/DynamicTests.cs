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
            Assert.AreEqual("<html><body>3</body></html>", requestContent);
            Assert.AreEqual("text/html", response.ContentType);   
        }

        [TestMethod]
        public void Dynamic_Add2And3_5AsXml()
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
            Assert.AreEqual("application/xml", response.ContentType);
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
        
    }
}

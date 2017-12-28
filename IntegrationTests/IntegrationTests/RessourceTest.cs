using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class RessourceTest
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void Webserver_RootIndexHtml_200()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost);
            
            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void Webserver_NonExsitingFile_200()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost+"xcvbadfg.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, ((HttpWebResponse)response).StatusCode);
        }
    }
}

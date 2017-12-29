using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class RequestParametersTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod, Ignore]
        public void RequestParameters_Accept_xx()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");
            request.Headers.Add(HttpRequestHeader.Accept, "");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }

        [TestMethod, Ignore]
        public void RequestParameters_Cookie_xx()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");
            request.Headers.Add(HttpRequestHeader.Accept, "");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }
    }
}

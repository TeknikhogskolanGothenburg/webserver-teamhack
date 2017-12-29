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
    public class ResponseParametersTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void ResponseParameters_ContentType_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }

        [TestMethod]
        public void ResponseParameters_ContentType_Javascript()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "script.js");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("application/javascript"));
        }

        [TestMethod]
        public void ResponseParameters_ContentType_Stylesheet()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "style.css");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/css"));
        }

        [TestMethod]
        public void ResponseParameters_ContentType_PdfDocument()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "document.pdf");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("application/pdf"));
        }

        [TestMethod]
        public void ResponseParameters_ContentType_JpgImage()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "laughing_panda.jpg");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("image/jpeg"));
        }

        [TestMethod]
        public void ResponseParameters_ContentType_GifImage()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "pianocat.gif");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("image/gif"));
        }

        [TestMethod, Ignore]
        public void ResponseParameters_Etag_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }

        [TestMethod, Ignore]
        public void ResponseParameters_Expires_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }
    }
}

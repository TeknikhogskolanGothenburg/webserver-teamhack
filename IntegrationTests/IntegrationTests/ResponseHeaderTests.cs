using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace IntegrationTests
{
    [TestClass]
    public class ResponseHeaderTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void ResponseHeaders_ContentType_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/html"));
        }

        [TestMethod]
        public void ResponseHeaders_ContentType_Javascript()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "script.js");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("application/javascript"));
        }

        [TestMethod]
        public void ResponseHeaders_ContentType_Stylesheet()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "style.css");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("text/css"));
        }

        [TestMethod]
        public void ResponseHeaders_ContentType_PdfDocument()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "document.pdf");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("application/pdf"));
        }

        [TestMethod]
        public void ResponseHeaders_ContentType_JpgImage()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "laughing_panda.jpg");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("image/jpeg"));
        }

        [TestMethod]
        public void ResponseHeaders_ContentType_GifImage()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "pianocat.gif");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.ContentType].Contains("image/gif"));
        }

        [TestMethod]
        public void ResponseHeaders_Etag_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual("cec994848ca6b58f6831a0676cd8670f", response.Headers[HttpResponseHeader.ETag],true);
        }

        [TestMethod]
        public void ResponseHeaders_ExpiresInOneYear_HtmlFile()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            string oneYearFromNow = DateTime.UtcNow.AddYears(1).ToString("o");
            string responseExpires = response.Headers[HttpResponseHeader.Expires];
            Assert.AreEqual(oneYearFromNow.Substring(0, 17), responseExpires.Substring(0, 17));
            Assert.AreEqual('Z', responseExpires[responseExpires.Length-1]);
        }
    }
}

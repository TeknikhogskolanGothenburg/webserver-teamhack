using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class StatusCodeTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void StatusCode_Root_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost);
            
            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_NonExsiting_NotFound()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "xyzqwer.html");


            // Act
            WebResponse response = request.GetResponseNoException();

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_Index_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_AnotherPage_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "anotherpage.htm");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_Document_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "document.pdf");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_PandaJpg_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "laughing_panda.jpg");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_CatGif_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "pianocat.gif");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_Script_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "script.js");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_Style_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "style.css");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_Subfolder_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "subfolder/");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }

        [TestMethod]
        public void StatusCode_SubfolderIndex_Ok()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "subfolder/index.html");

            // Act
            WebResponse response = request.GetResponse();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, ((HttpWebResponse)response).StatusCode);
        }
    }
}

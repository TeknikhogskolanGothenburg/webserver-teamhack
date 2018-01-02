using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class ContentApprovals
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void ContentApproval_Root()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost);

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.VerifyHtml(requestContent);
        }

        [TestMethod]
        public void ContentApproval_Index()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost+"index.html");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.VerifyHtml(requestContent);
        }

        [TestMethod]
        public void ContentApproval_Anotherpage()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "anotherpage.htm");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.VerifyHtml(requestContent);
        }

        [TestMethod]
        public void ContentApproval_SubfolderRoot()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "subfolder/");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.VerifyHtml(requestContent);
        }

        [TestMethod]
        public void ContentApproval_SubfolderIndex()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "subfolder/index.html");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.VerifyHtml(requestContent);
        }

        [TestMethod]
        public void ContentApproval_Script()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "script.js");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.Verify(requestContent);
        }

        [TestMethod]
        public void ContentApproval_Style()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "style.css");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadToEnd();

            // Assert
            Approvals.Verify(requestContent);
        }

        [TestMethod]
        public void ContentApproval_Cat()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "pianocat.gif");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            BinaryReader readStream = new BinaryReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadBytes(Convert.ToInt32(response.ContentLength));

            // Assert
            Approvals.VerifyBinaryFile(requestContent, "gif");
        }

        [TestMethod]
        public void ContentApproval_Panda()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "laughing_panda.jpg");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            BinaryReader readStream = new BinaryReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadBytes(Convert.ToInt32(response.ContentLength));

            // Assert
            Approvals.VerifyBinaryFile(requestContent, "jpg");
        }

        [TestMethod]
        public void ContentApproval_Document()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "document.pdf");

            // Act
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            BinaryReader readStream = new BinaryReader(receiveStream, Encoding.UTF8);
            var requestContent = readStream.ReadBytes(Convert.ToInt32(response.ContentLength));

            // Assert
            Approvals.VerifyBinaryFile(requestContent, "pdf");
        }
    }
}

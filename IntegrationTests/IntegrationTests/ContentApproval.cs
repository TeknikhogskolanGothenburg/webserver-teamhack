using ApprovalTests;
using ApprovalTests.Reporters;
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
    [UseReporter(typeof(DiffReporter))]
    public class ContentApproval
    {
        public const string Localhost = "http://localhost:8080/";
        public const string LocalContentPath = "..//..//..//Content";

        [TestMethod]
        public void ContentApproval_Index()
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
    }
}

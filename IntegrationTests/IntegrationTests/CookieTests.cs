using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace IntegrationTests
{
    [TestClass]
    public class CookieTests
    {
        public const string Localhost = "http://localhost:8080/";

        [TestMethod]
        public void Cookie_RequestWithoutCookie_OneCookie()
        {
            // Arrange
            WebRequest request = WebRequest.Create(Localhost + "index.html");

            // Act
            var response = (HttpWebResponse)request.GetResponse();

            // Assert
            Assert.IsTrue(response.Headers[HttpResponseHeader.SetCookie].Contains("counter="));
        }

        [TestMethod]
        public void Cookie_RequestCounterPageWithCookie_TwoHits()
        {
            // Arrange
            WebRequest requestWithoutCookie = WebRequest.Create(Localhost + "index.html");
            var responseWithCookie = (HttpWebResponse)requestWithoutCookie.GetResponse();
            var counterCookie = GetValueFromCounterCookie(responseWithCookie.Headers[HttpResponseHeader.SetCookie]);
            HttpWebRequest requestWithCookie = (HttpWebRequest)WebRequest.Create(Localhost + "counter");
            requestWithCookie.CookieContainer = new CookieContainer();
            requestWithCookie.CookieContainer.Add(new Cookie("counter", counterCookie, "/", "localhost"));

            // Act
            var response = (HttpWebResponse)requestWithCookie.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var responseContent = readStream.ReadToEnd();

            // Assert
            Assert.AreEqual(1, response.Cookies.Count);
            Assert.AreEqual(counterCookie, GetValueFromCounterCookie(response.Headers[HttpResponseHeader.SetCookie]));
            Assert.AreEqual("2", responseContent);
        }

        private string GetValueFromCounterCookie(string setCookieHeader)
        {
            Match match = Regex.Match(setCookieHeader, @"(counter=)(\d+)");
            if (match.Captures.Count == 0)
                return string.Empty;

            return match.Groups[2].Value;
        }
    }
}

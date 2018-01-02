using System.Net;

namespace IntegrationTests
{
    public static class HttpExtension
    {
        public static HttpWebResponse GetHttpResponse(this HttpWebRequest request)
        {
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException exception)
            {
                if (exception.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)exception.Response;
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        public static WebResponse GetResponseNoException(this WebRequest request)
        {
            WebResponse response = null;

            try
            {
                response = (WebResponse)request.GetResponse();
            }
            catch (WebException exception)
            {
                if (exception.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (WebResponse)exception.Response;
                }
                else
                {
                    throw;
                }
            }

            return response;
        }
    }


}

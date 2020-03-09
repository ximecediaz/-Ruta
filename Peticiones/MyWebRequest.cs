using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Peticiones
{
    class MyWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        public string status;

        public MyWebRequest(string url)
        {
        // Create a request using a URL that can receive a post.
            string directionB = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=";
            string directionA = "&inputtype=textquery&fields=formatted_address,name,geometry&key=AIzaSyCEjzmUzn_ElqGgo3GP0QuAB9lZA2-0M5E";
            string realDirection = directionB + url + directionA;
            request = WebRequest.Create(realDirection);
            request.Method = "GET";
        }

        public MyWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public MyWebRequest(string url, string method, string data)
            : this(url, method)
        {

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public String POSTresponse(string postData, string url)
        {
            string responseFromServer = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";

            //request.Credentials = " ";//"AIzaSyCEjzmUzn_ElqGgo3GP0QuAB9lZA2 - 0M5E";

            ((HttpWebRequest)request).UserAgent =
                "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
            request.Accept = "/";

            request.UseDefaultCredentials = true;

            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;


        }


    }
}


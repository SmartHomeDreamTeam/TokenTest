using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TokenTest
{
    class GetService
    {
        public async Task<string> Get(string parameter, string token)
        {
            //System.Net.HttpWebRequest request = new System.Net.HttpWebRequest(new Uri(String.Format("{0}Token", Constants.BaseAddress)));

            var uri = String.Format("{0}api/values/{1}", Constants.BaseAddress, parameter);

            WebRequest request = WebRequest.Create(new Uri(uri));
            request.Method = "GET";
            request.ContentType = "text/html";
            request.Headers.Add( HttpRequestHeader.Authorization, string.Format("Bearer {0}",token));

/*
            // to capture the service message
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream());
            var strHtml = sr.ReadToEnd();
*/
            try
            {
                var httpResponse = request.GetResponse();
                string json;
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }
                return JsonConvert.DeserializeObject<string>(json);
            }
            catch (Exception ex)
            {
                // throw new SecurityException("Bad credentials", ex);
                throw;
            }
        }
    }
}

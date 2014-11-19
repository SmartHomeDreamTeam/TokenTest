using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TokenTest
{
    class PostService
    {
        public async Task<string> Post<T>(T entity, string token)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(String.Format("{0}api/values", Constants.BaseAddress)));
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", token));


            // method 1
//            string json = JsonConvert.SerializeObject(entity, Formatting.Indented);
//
//            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
//            {
//                json = json.Replace("\r\n", "");
//                streamWriter.Write(json);
//                streamWriter.Flush();
//                streamWriter.Close();
//            }
//
//            var httpResponse = (HttpWebResponse)request.GetResponse();
//            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
//            {
//                var result = streamReader.ReadToEnd();
//                var text = result.ToString();
//                return text;
//            }


            // method 2
            string json;
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));

            using (Stream requestStream = await request.GetRequestStreamAsync())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            try
            {

                HttpWebResponse httpResponse = (HttpWebResponse)(await request.GetResponseAsync());
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }
                //var tokenResponse = JsonConvert.DeserializeObject<T>(json);
                return json;
            }
            catch (Exception ex)
            {
                // throw new SecurityException("Bad credentials", ex);
                throw;
            }
        }
    }
}

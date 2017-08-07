using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpLib.FritzBox
{

    /// <summary>
    /// 
    /// </summary>
    public class Client : HttpClient
    {

        //private string iCurrentSid = string.Empty;

        public Client()
        {
            BaseAddress = new Uri("http://fritz.box/");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<SessionInfo> GetSessionInfoAsync(string aParams = "")
        {
            SessionInfo info = null;
            HttpResponseMessage response = await GetAsync("login_sid.lua" + aParams);
            if (response.IsSuccessStatusCode)
            {
                //dynamic responseContent = await response.Content.ReadAsAsync<object>();
                //string returnedToken = responseContent.Token;
                //    
                DataContractSerializer serializer = new DataContractSerializer(typeof(SessionInfo));
                var stream = await response.Content.ReadAsStreamAsync();
                info = serializer.ReadObject(stream) as SessionInfo;
            }

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aUserName"></param>
        /// <param name="aPassword"></param>
        /// <returns></returns>
        public async Task<string> GetSessionId(string aUserName, string aPassword)
        {
            string sessionId = string.Empty;
            SessionInfo info = await GetSessionInfoAsync();
            if (info != null && info.IsSessionIdNull())
            {
                string request = @"?username=" + aUserName + @"&response=" + GetResponse(info.Challenge, aPassword);
                info = await GetSessionInfoAsync(request);
                if (info != null && !info.IsSessionIdNull())
                {
                    sessionId = info.SessionId;
                }
            }

            return sessionId;
        }


        public string GetResponse(string challenge, string kennwort)
        {
            return challenge + "-" + GetMD5Hash(challenge + "-" + kennwort);
        }

        public string GetMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data =
            md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }



    }
}

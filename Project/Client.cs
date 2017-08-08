using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharpLib.FritzBox.SmartHome
{

    /// <summary>
    /// 
    /// </summary>
    public class Client : HttpClient
    {

        public string SessionId { get; private set; }

        public Client(string aBaseAddress = "http://fritz.box/")
        {
            BaseAddress = new Uri(aBaseAddress);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<DeviceList> GetDeviceListAsync()
        {
            // TODO: try the query before in case no login needed?
            if (string.IsNullOrEmpty(SessionId))
            {
                throw new UnauthorizedAccessException("Need to authenticate first!");
            }
            
            // Now issue our request and get our list
            DeviceList list = null;
            HttpResponseMessage response = await GetAsync("webservices/homeautoswitch.lua?sid=" + SessionId + "&switchcmd=getdevicelistinfos");
            if (response.IsSuccessStatusCode)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DeviceList));
                var stream = await response.Content.ReadAsStreamAsync();
                list = serializer.Deserialize(stream) as DeviceList;
            }

            return list;
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
        /// Authenticate using given credentials.
        /// </summary>
        /// <param name="aUserName"></param>
        /// <param name="aPassword"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(string aUserName, string aPassword)
        {
            SessionId = await GetSessionId(aUserName, aPassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aUserName"></param>
        /// <param name="aPassword"></param>
        /// <returns></returns>
        private async Task<string> GetSessionId(string aUserName, string aPassword)
        {
            string sessionId = string.Empty;
            SessionInfo info = await GetSessionInfoAsync();
            if (info != null)            
            {
                // If we don't have a session ID, just get one
                if (info.IsSessionIdNull())
                {
                    string request = @"?username=" + aUserName + @"&response=" + GetLoginResponse(info.Challenge, aPassword);
                    info = await GetSessionInfoAsync(request);
                    if (info != null && !info.IsSessionIdNull())
                    {
                        sessionId = info.SessionId;
                    }
                }
            }

            return sessionId;
        }



        /// <summary>
        /// Provide our login response token.
        /// </summary>
        /// <param name="challenge"></param>
        /// <param name="kennwort"></param>
        /// <returns></returns>
        private string GetLoginResponse(string challenge, string kennwort)
        {
            return challenge + "-" + GetMD5Hash(challenge + "-" + kennwort);
        }

        /// <summary>
        /// Create an MD5 hash for our authentication token.
        /// See: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMD5Hash(string input)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpLib.FritzBox
{

    [DataContract(Namespace = "")]
    public class SessionInfo
    {
        [DataMember]
        string SID { get; set; }

        /// <summary>
        /// Order attribute property is important as explained there: 
        /// https://stackoverflow.com/questions/19989883/some-properties-are-not-being-deserialized-using-datacontractserializer
        /// </summary>
        [DataMember(Name = "Challenge", IsRequired = true, Order = 1)]
        string Challenge { get; set; }

        [DataMember(Name = "BlockTime", Order = 2)]
        string BlockTime { get; set; }
        //string Rights { get; set; }
    }


    public class Client : HttpClient
    {
        public Client()
        {
            BaseAddress = new Uri("http://fritz.box/");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public async Task<SessionInfo> GetSessionInfoAsync()
        {
            SessionInfo info = null;
            HttpResponseMessage response = await GetAsync("login_sid.lua");
            if (response.IsSuccessStatusCode)
            {
                //dynamic responseContent = await response.Content.ReadAsAsync<object>();
                //string returnedToken = responseContent.Token;

                //info = await response.Content.ReadAsAsync<SessionInfo>();

                DataContractSerializer serializer = new DataContractSerializer(typeof(SessionInfo));
                var stream = await response.Content.ReadAsStreamAsync();
                info = serializer.ReadObject(stream) as SessionInfo;



            }
            return info;
        }

    }
}

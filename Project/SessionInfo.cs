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

    [DataContract(Namespace = "")]
    public class SessionInfo
    {
        /// <summary>
        /// Session ID
        /// 
        /// </summary>
        [DataMember(Name = "SID", IsRequired = true)]
        public string SessionId { get; set; }

        /// <summary>
        /// Order attribute property is important as explained there: 
        /// https://stackoverflow.com/questions/19989883/some-properties-are-not-being-deserialized-using-datacontractserializer
        /// </summary>
        [DataMember(Name = "Challenge", IsRequired = true, Order = 1)]
        public string Challenge { get; set; }

        
        [DataMember(Name = "BlockTime", Order = 2)]
        public string BlockTime { get; set; }
        //string Rights { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsSessionIdNull()
        {
            return SessionId == KNullSessionId;
        }

        /// <summary>
        /// 
        /// </summary>
        private const string KNullSessionId = "0000000000000000";
    }

}

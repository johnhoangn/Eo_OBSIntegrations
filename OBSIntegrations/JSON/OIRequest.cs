using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OBSIntegrations {
    abstract class OIRequest {
        static int RequestId = 0;

        [JsonProperty("request-type")]
        internal string RequestType = "";

        [JsonProperty("message-id")]
        string MessageId = RequestId++.ToString();

        public OIRequest GetBlueprint() {
            return this;
        }
    }
}

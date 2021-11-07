using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OBSIntegrations {
    class SceneChangeRequest : OIRequest {
        [JsonProperty("scene-name")]
        string SceneName { get; set; }

        public SceneChangeRequest(string scene) {
            RequestType = "SetCurrentScene";
            SceneName = scene;
        }
    }
}

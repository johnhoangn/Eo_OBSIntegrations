using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OBSIntegrations.JSON {
    public class ActionParams {
        [JsonProperty("scene-name")]
        public string SceneName { get; set; }
    }

    public class Binding {
        public string eventType { get; set; }
        public string actionType { get; set; }
        public ActionParams actionParams { get; set; }
    }

    public class SavedBindings {
        public List<Binding> bindings { get; set; }
    }
}

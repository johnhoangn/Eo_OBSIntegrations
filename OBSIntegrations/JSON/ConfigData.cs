using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OBSIntegrations.JSON {
    public class Binding {
        public string eventType { get; set; }
        public string actionType { get; set; }
        public string actionParams { get; set; }
    }

    public class ConfigData {
        public List<Binding> bindings { get; set; }
    }
}

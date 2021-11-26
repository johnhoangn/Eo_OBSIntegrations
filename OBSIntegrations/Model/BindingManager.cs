using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OBSIntegrations.JSON;

namespace OBSIntegrations.Model {
    class BindingManager {
        private static BindingManager Instance = null;
        public Dictionary<OIEventType, List<OIBinding>> bindingSets = new Dictionary<OIEventType, List<OIBinding>>();

        private BindingManager() {
            Instance = this;
        }

        public static BindingManager GetInstance() {
            if (Instance == null) {
                new BindingManager();
            }

            return Instance;
        }

        public OIBinding Bind(OIEventType bsEvent, OIActionType action, JSON.OIRequest request) {
            OIBinding bind = new OIBinding(bsEvent, action, request);

            if (!bindingSets.TryGetValue(bsEvent, out List<OIBinding> subset)) {
                subset = new List<OIBinding>();
                bindingSets.Add(bsEvent, subset);
            }

            subset.Add(bind);
            
            return bind;
        }

        public bool Unbind(OIBinding bind) {
            if (!bindingSets.TryGetValue(bind.bsEvent, out List<OIBinding> subset)) {
                throw new KeyNotFoundException("No binds currently hooked to that Event! " + bind.bsEvent.ToString());
            } else {
                return subset.Remove(bind);
            }
        }
    }
}
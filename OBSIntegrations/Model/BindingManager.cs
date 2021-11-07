using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OBSIntegrations.JSON;

namespace OBSIntegrations.Model {
    class BindingManager {
        private static BindingManager instance = null;
        public Dictionary<OIEventType, List<OIBinding>> bindingSets = new Dictionary<OIEventType, List<OIBinding>>();

        public static BindingManager GetInstance() {
            return instance ?? new BindingManager();
        }

        public OIBinding Bind(OIEventType bsEvent, OIActionType action, JSON.OIRequest request) {
            OIBinding bind = new OIBinding(bsEvent, action, request);

            if (!bindingSets.TryGetValue(bsEvent, out List<OIBinding> subset)) {
                bindingSets.Add(bsEvent, new List<OIBinding>());
                subset = bindingSets[bsEvent];
            }

            subset.Add(bind);
            
            return bind;
        }

        public bool Unbind(OIBinding bind) {
            if (!bindingSets.TryGetValue(bind.bsEvent, out List<OIBinding> subset)) {
                throw new KeyNotFoundException("No binds currently be hooked to that Event! " + bind.bsEvent.ToString());
            } else {
                return subset.Remove(bind);
            }
        }
    }
}
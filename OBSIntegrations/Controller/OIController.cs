using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OBSIntegrations.Model;

namespace OBSIntegrations.Controller {
    class OIController {
        private static OIController instance = null;
        private static Dictionary<OIEventType, int> bindCounts = new Dictionary<OIEventType, int>();

        private static BindingManager bindManager = BindingManager.GetInstance();
        private static SubManager subManager = SubManager.GetInstance();

        public static OIController GetInstance() {
            return instance ?? new OIController();
        }

        // OIActionType param might not be necessary. OIRequest could hold sufficient context
        public OIBinding CreateBinding(OIEventType bsEvent, OIActionType action, JSON.OIRequest request) {
            var bind = bindManager.Bind(bsEvent, action, request);

            if (bindCounts.TryGetValue(bsEvent, out _)) {
                bindCounts[bsEvent]++;
            } else {
                bindCounts.Add(bsEvent, 1);
                subManager.SubscribeTo(bsEvent, bindManager.bindingSets[bsEvent]);
            }

            return bind;
        }

        public bool DestroyBinding(OIBinding bind) {
            OIEventType bsEvent = bind.bsEvent;

            if (--bindCounts[bsEvent] == 0) {
                 subManager.UnubscribeFrom(bsEvent);
            }

            return bindManager.Unbind(bind);
        }
    }
}

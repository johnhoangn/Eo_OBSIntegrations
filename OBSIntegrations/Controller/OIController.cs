using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OBSIntegrations.Model;
using Newtonsoft.Json;

namespace OBSIntegrations.Controller {
    class OIController {
        private static OIController Instance = null;
        private static Dictionary<OIEventType, int> bindCounts = new Dictionary<OIEventType, int>();

        private static BindingManager bindManager = BindingManager.GetInstance();
        private static SubManager subManager = SubManager.GetInstance();
        private Configuration config = new Configuration();

        private OIController() {
            /* config.ReadConfig();

             config.data.bindings.ForEach(bind => {
                 CreateBinding(
                     (OIEventType)Enum.Parse(typeof(OIEventType), bind.eventType),
                     (OIActionType)Enum.Parse(typeof(OIActionType), bind.actionType),
                     JsonConvert.DeserializeObject<JSON.OIRequest>(bind.actionParams)
                 );
             });*/
            Instance = this;
        }

        public static OIController GetInstance() {
            return Instance ?? new OIController();
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

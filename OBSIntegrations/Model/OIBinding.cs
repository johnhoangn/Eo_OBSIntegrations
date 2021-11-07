using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OBSIntegrations.JSON;

namespace OBSIntegrations.Model {
    class OIBinding {
        public OIEventType bsEvent;
        public OIActionType obsAction;
        public OIRequest request;

        public OIBinding(OIEventType ev, OIActionType ac, OIRequest rq) {
            bsEvent = ev;
            obsAction = ac;
            request = rq;
        }
    }
}
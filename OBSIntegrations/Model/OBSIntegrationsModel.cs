using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSIntegrations
{
    class OBSIntegrationsModel
    {
        private List<OBSBinding> bindings;

        OBSIntegrationsModel()
        {
            bindings = new List<OBSBinding>();
        }

        public OBSBinding Bind(BeatsaberEvent bsEvent, OBSAction action)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSIntegrations
{
    class OBSBinding
    {
        private OBSAction action;
        private BeatsaberEvent bsEvent;
        OBSBinding(BeatsaberEvent _bsEvent, OBSAction _action)
        {
            action = _action;
            bsEvent = _bsEvent;
        }
    }
}

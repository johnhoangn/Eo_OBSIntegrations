using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BS_Utils.Utilities;
using WebSocketSharp;
using Newtonsoft.Json;

namespace OBSIntegrations {
    // TODO: Handle BeatsaberHttpStatus events not handled by BSEvents
    class SubManager {
        private static SubManager instance = null;
        private static WebSocket OBS_WS = new WebSocket("ws://localhost:9085");

        private SubManager() {
            OBS_WS.Connect(); // TODO: Async and retry loop if closed or never opened
            OBSIntegrations.Log?.Warn("JN: OBS SOCKET CONNECTED " + OBS_WS.ReadyState);
        }

        public static SubManager GetInstance() {
            return instance ?? new SubManager();
        }

        public bool SubscribeTo(OIEventType ev, List<OIBinding> bindList) {
            OBSIntegrations.Log?.Warn("JN: SUBSCRIBE " + ev.ToString());

            // This'll get better, I hope.
            switch (ev) {
                case OIEventType.SongStarted:
                    BSEvents.gameSceneLoaded += () => {
                        OBSIntegrations.Log?.Warn("JN: CASE " + ev.ToString());
                        bindList.Where(bind => bind.bsEvent == ev).ToList().ForEach(bind => { Trigger(bind); });
                    };
                    break;
                case OIEventType.SongFinished:
                    BSEvents.menuSceneLoaded += () => {
                        OBSIntegrations.Log?.Warn("JN: CASE " + ev.ToString());
                        bindList.Where(bind => bind.bsEvent == ev).ToList().ForEach(bind => { Trigger(bind); });
                    };
                    break;
            }
            return true;
        }

        // TODO: Cannot currently unsubscribe as I have no reference to
        //  the handlers assigned to the events.
        public bool UnubscribeFrom(OIEventType ev) {

            return true;
        }

        private void Trigger(OIBinding bind) {
            OBSIntegrations.Log?.Warn("JN: REQUEST " + JsonConvert.SerializeObject(bind.request));
            OBS_WS.Send(JsonConvert.SerializeObject(bind.request));
        }
    }
}

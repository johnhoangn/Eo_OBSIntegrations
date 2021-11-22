using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BS_Utils.Utilities;
using WebSocketSharp;
using Newtonsoft.Json;

using OBSIntegrations.Model;

namespace OBSIntegrations.Controller {
    // TODO: Handle BeatsaberHttpStatus events not handled by BSEvents
    class SubManager {
        private static SubManager instance = null;
        private static WebSocket OBS_WS = new WebSocket("ws://localhost:9085");
        private static Dictionary<OIEventType, Action> subDestructors = new Dictionary<OIEventType, Action>();

        private SubManager() {
            OBS_WS.Connect(); // TODO: Async and retry loop if closed or never opened
            OBSIntegrations.Log?.Warn("JN: OBS SOCKET CONNECTED " + OBS_WS.ReadyState);
        }

        public static SubManager GetInstance() {
            return instance ?? new SubManager();
        }

        public bool SubscribeTo(OIEventType ev, List<OIBinding> bindList) {
            OBSIntegrations.Log?.Warn("JN: SUBSCRIBING TO " + ev.ToString());
            Action callback = MakeDelegate(ev, bindList);
            Action destructor;

            // Would love to learn a better way to store/remove the callback
            switch (ev) {
                case OIEventType.SongStarted:
                    BSEvents.gameSceneLoaded += callback;
                    destructor = () => { BSEvents.gameSceneLoaded -= callback; };
                    break;
                case OIEventType.SongFinished:
                    BSEvents.menuSceneLoaded += callback;
                    destructor = () => { BSEvents.menuSceneLoaded -= callback; };
                    break;
                default:
                    throw new ArgumentException("Invalid OIEventType! " + ev.ToString());
            }


            if (!subDestructors.TryGetValue(ev, out _)) {
                OBSIntegrations.Log?.Warn("JN: SUBSCRIBED TO " + ev.ToString());
                subDestructors.Add(ev, destructor);
            }

            return true;
        }

        public bool UnubscribeFrom(OIEventType ev) {
            if (subDestructors.TryGetValue(ev, out Action destructor)) {
                destructor();
                subDestructors.Remove(ev);
                OBSIntegrations.Log?.Warn("JN: UNSUBSCRIBED FROM " + ev.ToString() + " " + !subDestructors.TryGetValue(ev, out _));
            }

            return true;
        }

        private Action MakeDelegate(OIEventType ev, List<OIBinding> bindList) {
            Action callback = () => {
                OBSIntegrations.Log?.Warn("JN: CASE " + ev.ToString());

                bindList.Where(bind => bind.bsEvent == ev).ToList().ForEach(bind => {
                    OBSIntegrations.Log?.Warn("JN: REQUEST " + JsonConvert.SerializeObject(bind.request));
                    OBS_WS.Send(JsonConvert.SerializeObject(bind.request));
                });
            };

            return callback;
        }
    }
}

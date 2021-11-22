using IPA;
using IPALogger = IPA.Logging.Logger;

namespace OBSIntegrations {
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class OBSIntegrations {
        internal static OBSIntegrations Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        internal static Controller.OIController controller = Controller.OIController.GetInstance();

        [Init]
        public OBSIntegrations(IPALogger logger) {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart() {
            OBSIntegrations.Log.Info("JN: Hi, World.");

            controller.CreateBinding(
                OIEventType.SongStarted,
                OIActionType.SetCurrentScene,
                new JSON.SceneChangeRequest("GameScene")
            );

            controller.CreateBinding(
                OIEventType.SongFinished,
                OIActionType.SetCurrentScene,
                new JSON.SceneChangeRequest("MenuScene")
            );
        }

        [OnExit]
        public void OnApplicationQuit() {

        }
    }
}
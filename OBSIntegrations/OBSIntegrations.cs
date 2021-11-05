using IPA;
using IPALogger = IPA.Logging.Logger;

namespace OBSIntegrations
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class OBSIntegrations
    {
        internal static OBSIntegrations Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public OBSIntegrations(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            OBSIntegrations.Log.Info("OnApplicationStart");
        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }
    }
}

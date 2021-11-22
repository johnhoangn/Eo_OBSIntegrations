using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Newtonsoft.Json;
using OBSIntegrations.JSON;

namespace OBSIntegrations.Controller {
    class Configuration {
        public static readonly string configFile = Path.Combine(Application.dataPath, @"..\UserData\OBSIntegrationsConfig.json");

        public ConfigData data { get; private set; }

        public void ReadConfig() {
            if (!File.Exists(configFile)) {
                ConfigData defaultConfig = new ConfigData();

                data = defaultConfig;
                WriteConfig(JsonConvert.SerializeObject(defaultConfig));

                return;
            }

            data = JsonConvert.DeserializeObject<ConfigData>(File.ReadAllText(configFile));
        }

        public void WriteConfig() {
            WriteConfig(JsonConvert.SerializeObject(data));
        }

        public void WriteConfig(string configData) {
            File.WriteAllText(configFile, configData);
        }
    }
}

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
        public static readonly string configFile = Path.Combine(Application.dataPath, @"..\UserData\Eo\OBSIntegrations\Config.json");

        public ConfigData data { get; private set; }

        public void ReadConfig() {
            OBSIntegrations.Log.Info("JN: Config Path: " + configFile);

            if (!File.Exists(configFile)) {
                ConfigData defaultConfig = new ConfigData();

                data = defaultConfig;
                WriteConfig(JsonConvert.SerializeObject(defaultConfig));

                return;
            }

            string fileContents = File.ReadAllText(configFile);

            OBSIntegrations.Log.Info("JN: Config Contents: " + fileContents);

            data = JsonConvert.DeserializeObject<ConfigData>(fileContents);
        }

        public void WriteConfig() {
            WriteConfig(JsonConvert.SerializeObject(data));
        }

        public void WriteConfig(string configData) {
            File.WriteAllText(configFile, configData);
        }
    }
}

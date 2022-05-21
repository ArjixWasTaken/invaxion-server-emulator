﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using Server.Emulator.EagleTcpPatches;

namespace Server
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Server : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource logger;
        private static bool EnableEmulator = true;
        public static DataBase Database;
        public static List<string> MustImplement = new();

        private void Awake()
        {
            Server.logger = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            
            if (EnableEmulator)
            {
                Database = new DataBase();
                Logger.LogInfo("Local server is enabled!");
                HookManager.Instance.Create();
            }
        }

        private void OnApplicationQuit()
        {
            var output = MustImplement.Aggregate("", (current, line) => current + $"{line}\n");
            File.WriteAllText("must-implement.txt", output);
        }
    }
}
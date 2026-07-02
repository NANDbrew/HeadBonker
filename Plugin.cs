using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;

namespace HeadBonker
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInDependency("com.nandbrew.shipyardexpansion", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.headbonker";
        public const string PLUGIN_NAME = "Head Bonker";
        public const string PLUGIN_VERSION = "0.0.3";

        public static Plugin instance;
        internal static bool SEinstalled;
        //--settings--
        //internal ConfigEntry<bool> someSetting;


        private void Awake()
        {
            SEinstalled = Chainloader.PluginInfos.ContainsKey("com.nandbrew.shipyardexpansion");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            instance = this;
            //someSetting = Config.Bind("Settings", "Some setting", false);
        }
    }
}

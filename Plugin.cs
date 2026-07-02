using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;

namespace HeadBonker
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    //[BepInDependency("com.app24.sailwindmoddinghelper", "2.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.headbonker";
        public const string PLUGIN_NAME = "Head Bonker";
        public const string PLUGIN_VERSION = "0.0.2";

        public static Plugin instance;

        //--settings--
        //internal ConfigEntry<bool> someSetting;


        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            instance = this;
            //someSetting = Config.Bind("Settings", "Some setting", false);
        }
    }
}

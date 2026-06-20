using HarmonyLib;
using UnityEngine;

namespace HeadBonker
{
    [HarmonyPatch(typeof(PlayerCrouching), "Awake")]
    public static class BonkerAdder
    {
        public static void Postfix(PlayerCrouching __instance)
        {
            GameObject foo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            foo.transform.SetParent(__instance.transform, false);
            foo.transform.localScale = Vector3.one * 0.3f;
            foo.transform.localPosition = new Vector3(0, 0.08f, 0);
            foo.AddComponent<HeadBonker>().crouching = __instance;
            foo.GetComponent<Renderer>().enabled = false;

        }
    }
}

using BepInEx;
using System.ComponentModel;

namespace Patching.HarmonyPatching
{
    [BepInPlugin(Stealth.PluginInfo.GUID, Stealth.PluginInfo.Name, Stealth.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }
    }
}

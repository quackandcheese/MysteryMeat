using HarmonyLib;
using Kitchen;
using Kitchen.Components;
using KitchenMysteryMeat.MonoBehaviours;
using KitchenMysteryMeat.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Patches
{
    [HarmonyPatch]
    static class SoundEventView_Patch
    {
        [HarmonyPatch(typeof(SoundEventView), "UpdateData")]
        [HarmonyPrefix]
        static bool UpdateData_Prefix(ref SoundEventView __instance, SoundEventView.ViewData data)
        {
            if (data.Event == Mod.AlertSoundEvent)
            {
                __instance.gameObject.AddComponent<PreferenceVolumeAdjuster>().PreferenceID = Mod.ALERT_VOLUME_ID;

            }
            else if (data.Event == Mod.StabSoundEvent)
            {
                __instance.gameObject.AddComponent<PreferenceVolumeAdjuster>().PreferenceID = Mod.STAB_VOLUME_ID;
            }
            return true;
        }
    }
}

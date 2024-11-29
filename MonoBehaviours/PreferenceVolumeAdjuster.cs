using Kitchen.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.MonoBehaviours
{
    public class PreferenceVolumeAdjuster : MonoBehaviour
    {
        public string PreferenceID = "";

        public void Update()
        {
            if (PreferenceID != "")
            {
                if (base.TryGetComponent<SoundSource>(out var soundSource))
                {
                    soundSource.VolumeMultiplier = (Mod.PrefManager.Get<int>(PreferenceID) / 100.0f);
                }
            }
        }
    }
}

using Kitchen;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat
{
    public static class Helper
    {
        public static GameObject GetPrefab(string name)
        {
            return Mod.Bundle.LoadAsset<GameObject>(name);
        }

        internal static void SetupThinCounterLimitedItem(GameObject counterPrefab, GameObject itemPrefab, bool hasHeldItemPosition)
        {
            Transform holdTransform = GameObjectUtils.GetChildObject(counterPrefab, "GameObject").transform;

            counterPrefab.TryAddComponent<HoldPointContainer>().HoldPoint = holdTransform;

            var sourceView = counterPrefab.TryAddComponent<LimitedItemSourceView>();

            if (hasHeldItemPosition)
            {
                sourceView.HeldItemPosition = holdTransform;
            }

            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                GameObjectUtils.GetChildObject(counterPrefab, $"GameObject/{itemPrefab.name}")
            });
        }
    }
}

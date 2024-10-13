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

        // https://github.com/DepletedNova/IngredientLib/blob/8e6e319e027327a9cd8aa188863614c47dd4c698/Util/Helper.cs#L51C9-L63C10
        internal static void SetupCounterLimitedItem(GameObject counterPrefab, GameObject itemPrefab)
        {
            Transform holdTransform = GameObjectUtils.GetChildObject(counterPrefab, "Block/HoldPoint").transform;

            counterPrefab.TryAddComponent<HoldPointContainer>().HoldPoint = holdTransform;

            var sourceView = counterPrefab.TryAddComponent<LimitedItemSourceView>();
            sourceView.HeldItemPosition = holdTransform;
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                GameObjectUtils.GetChildObject(counterPrefab, $"Block/HoldPoint/{itemPrefab.name}")
            });
        }
    }
}

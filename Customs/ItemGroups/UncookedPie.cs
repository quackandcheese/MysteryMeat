using System.Collections.Generic;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.ItemGroups
{
    public class UncookedPie : CustomItemGroup<UncookedPieItemGroupView>
    {
        public override string UniqueNameID => "UncookedPie";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Uncooked Pie").AssignMaterialsByNames();
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>()
        {
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new List<Item>()
                {
                    (Item)GDOUtils.GetExistingGDO(ItemReferences.PieCrustRaw),
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                Items = new List<Item>()
                {
                    (Item)GDOUtils.GetCustomGameDataObject<MysteryMeat>().GameDataObject,
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 3f,
                Process = (Process)GDOUtils.GetExistingGDO(ProcessReferences.Cook),
                Result = (Item)GDOUtils.GetExistingGDO(ItemReferences.PieMeatCooked)
            }
        };


        public override void OnRegister(ItemGroup gameDataObject)
        {
            Prefab.GetComponent<UncookedPieItemGroupView>()?.Setup(Prefab);
        }
    }

    public class UncookedPieItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab)
        {
            // This tells which sub-object of the prefab corresponds to each component of the ItemGroup
            // All of these sub-objects are hidden unless the item is present
            ComponentGroups = new();
        }
    }
}
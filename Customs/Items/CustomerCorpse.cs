using Kitchen;
using KitchenAmericanBreakfast.Sides;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
/*    public class CustomerCorpseItemView : ObjectsSplittableView
    {
        internal void Setup(GameObject prefab)
        {
            var fObjects = ReflectionUtils.GetField<ObjectsSplittableView>("Objects");
            fObjects.SetValue(this, new List<GameObject>()
            {
                prefab.GetChild("Slice1"),
                prefab.GetChild("Slice2"),
                prefab.GetChild("Slice3")
            });
        }
    }*/

    public class CustomerCorpse : CustomItem
    {
        // UniqueNameID - This is used internally to generate the ID of this GDO. Once you've set it, don't change it.
        public override string UniqueNameID => "CustomerCorpse";

        // Prefab - This is the GameObject used for this Item's visual. AssignMaterialsByNames() is a helper method that assigns materials to the GameObject based on the names of the materials.
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Customer Corpse").AssignMaterialsByNames();
        public override Item SplitSubItem => (Item)GDOUtils.GetCustomGameDataObject<MysteryMeat>().GameDataObject;
        public override int SplitCount => 5;
        public override float SplitSpeed => 1.0f;
        public override List<Item> SplitDepletedItems => new() { (Item)GDOUtils.GetCustomGameDataObject<MysteryMeat>().GameDataObject };
        public override List<IItemProperty> Properties => new()
        {
        };

        /*public override void OnRegister(Item gameDataObject)
        {
            if (!Prefab.HasComponent<CustomerCorpseItemView>())
            {
                var view = Prefab.AddComponent<CustomerCorpseItemView>();
                view.Setup(Prefab);
            }
        }*/
    }
}
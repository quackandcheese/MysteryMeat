using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Appliances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
    public class RottenCustomerCorpse : CustomItem
    {
        // UniqueNameID - This is used internally to generate the ID of this GDO. Once you've set it, don't change it.
        public override string UniqueNameID => "RottenCustomerCorpse";

        // Prefab - This is the GameObject used for this Item's visual. AssignMaterialsByNames() is a helper method that assigns materials to the GameObject based on the names of the materials.
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Rotten Customer Corpse").AssignMaterialsByNames();
        public override Item SplitSubItem => (Item)GDOUtils.GetCustomGameDataObject<RottenMysteryMeat>().GameDataObject;
        public override int SplitCount => 5;
        public override float SplitSpeed => 1.0f;
        public override List<Item> SplitDepletedItems => new() { (Item)GDOUtils.GetCustomGameDataObject<RottenMysteryMeat>().GameDataObject };
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override bool IsIndisposable => true;
        public override List<IItemProperty> Properties => new()
        {
            /*new CProcessCausesSpill()
            {
                Process = -1,
                ID = GDOUtils.GetCustomGameDataObject<BloodSpill1>().ID,
                Rate = 1.0f,
                OverwriteOtherMesses = false
            },*/
            new CIllegalSight()
            {
            },
            new CPreventItemMerge()
            {
                Condition = MergeCondition.OnlyAsFirstSplitElement
            },
            new CPreservedOvernight()
        };

        public override void OnRegister(Item item)
        {
            if (!Prefab.HasComponent<CustomerCorpseItemView>())
            {
                var view = Prefab.AddComponent<CustomerCorpseItemView>();
                view.Setup(Prefab);
            }
        }
    }
}

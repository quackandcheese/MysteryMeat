using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Appliances;
using KitchenMysteryMeat.Customs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
    public class RottenMysteryMeat : CustomItem
    {
        public override string UniqueNameID => "RottenMysteryMeat";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Rotten Mystery Meat").AssignMaterialsByNames().AssignVFXByNames();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;


        /*public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>()
        {
            new Item.ItemProcess()
            {
                Process = (Process)GDOUtils.GetExistingGDO(ProcessReferences.Chop),
                Duration = 1.0f,
                Result = (Item)GDOUtils.GetExistingGDO(ItemReferences.MeatChopped),
            },
            new Item.ItemProcess
            {
                Duration = 2.0f,
                Process = GDOUtils.GetCastedGDO<Process, GrindMeat>(),
                Result = (Item)GDOUtils.GetExistingGDO(ItemReferences.Mince)
            }
        };

        public override List<IItemProperty> Properties => new List<IItemProperty>()
        {
            new CGrindable()
        };*/
    }
}

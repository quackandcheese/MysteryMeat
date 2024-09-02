using System.Collections.Generic;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Appliances;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
    public class MysteryMeat : CustomItem
    {
        public override string UniqueNameID => "MysteryMeat";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Mystery Meat").AssignMaterialsByNames();
        public override Appliance DedicatedProvider => (Appliance)GDOUtils.GetCustomGameDataObject<MeatCleaverProvider>().GameDataObject;


        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>()
        {
            new Item.ItemProcess()
            {
                Process = (Process)GDOUtils.GetExistingGDO(ProcessReferences.Chop),
                Duration = 1.0f,
                Result = (Item)GDOUtils.GetExistingGDO(ItemReferences.MeatChopped),
            }
        };
    }
}
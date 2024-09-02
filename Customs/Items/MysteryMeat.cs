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
    }
}
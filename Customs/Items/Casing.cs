using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Appliances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
    public class Casing : CustomItem
    {
        public override string UniqueNameID => "Casing";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Casing").AssignMaterialsByNames();
        public override Appliance DedicatedProvider => (Appliance)GDOUtils.GetCustomGameDataObject<CasingsProvider>().GameDataObject;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
    }
}

using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
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
    public class TrashBag : CustomItem
    {
        public override string UniqueNameID => "TrashBag";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Trash Bag").AssignMaterialsByNames();
        public override Appliance DedicatedProvider => (Appliance)GDOUtils.GetCustomGameDataObject<TrashBagProvider>().GameDataObject;
    }
}

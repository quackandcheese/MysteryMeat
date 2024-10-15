using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Appliances;
using KitchenMysteryMeat.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Items
{
    public class EmptySpecialSauceBottle : CustomItem
    {
        public override string UniqueNameID => "EmptySpecialSauceBottle";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Special Sauce Bottle").AssignMaterialsByNames();
        public override Appliance DedicatedProvider => (Appliance)GDOUtils.GetCustomGameDataObject<SpecialSauceProvider>().GameDataObject;
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;

        public override List<IItemProperty> Properties => new()
        {
            new CEmptyBottle()
            {
                FullBottleID = GDOUtils.GetCustomGameDataObject<SpecialSauceBottle>().GameDataObject.ID
            }
        };
    }
}

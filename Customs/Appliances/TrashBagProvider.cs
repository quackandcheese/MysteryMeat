using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Appliances
{
    public class TrashBagProvider : CustomAppliance
    {
        public override string UniqueNameID => "TrashBagProvider";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Trash Bag Provider").AssignMaterialsByNames().AssignVFXByNames();
        public override PriceTier PriceTier => PriceTier.VeryExpensive;
        public override RarityTier RarityTier => RarityTier.Uncommon;
        public override bool SellOnlyAsDuplicate => true;//false;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            ( Locale.English, LocalisationUtils.CreateApplianceInfo("Trash Bags", "Large enough to fit some body!", new(), new()) )
        };
        public override List<IApplianceProperty> Properties => new()
        {
            KitchenPropertiesUtils.GetUnlimitedCItemProvider(GDOUtils.GetCustomGameDataObject<TrashBag>().ID)
        };

        public override List<Appliance> RequiresForShop => new()
        {
            (Appliance)GDOUtils.GetCustomGameDataObject<MeatCleaverProvider>().GameDataObject
        };
    }
}

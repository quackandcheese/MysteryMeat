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
    public class CasingsProvider : CustomAppliance
    {
        public override string UniqueNameID => "CasingsProvider";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Casings Provider").AssignMaterialsByNames().AssignVFXByNames();
        public override PriceTier PriceTier => PriceTier.VeryExpensive;
        public override RarityTier RarityTier => RarityTier.Uncommon;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            ( Locale.English, LocalisationUtils.CreateApplianceInfo("Casings", "Provides hotdog casings", new(), new()) )
        };
        public override List<IApplianceProperty> Properties => new()
        {
            KitchenPropertiesUtils.GetUnlimitedCItemProvider(GDOUtils.GetCustomGameDataObject<Casing>().ID)
        };

        public override List<Appliance> RequiresForShop => new()
        {
            (Appliance)GDOUtils.GetCustomGameDataObject<MeatCleaverProvider>().GameDataObject
        };
    }
}

using Kitchen;
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
    public class SpecialSauceProvider : CustomAppliance
    {
        public override string UniqueNameID => "SpecialSauceProvider";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Special Sauce Provider").AssignMaterialsByNames().AssignVFXByNames();
        public override PriceTier PriceTier => PriceTier.VeryExpensive;
        public override RarityTier RarityTier => RarityTier.Uncommon;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            ( Locale.English, LocalisationUtils.CreateApplianceInfo("Special Sauce", "Requires a top-secret ingredient!", new(), new()) )
        };

        public override List<Process> RequiresProcessForShop => new()
        {
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            KitchenPropertiesUtils.GetCItemProvider(GDOUtils.GetCustomGameDataObject<EmptySpecialSauceBottle>().ID, 1, 1, false, false, true, false, false, true, false),
        };

        public override void OnRegister(Appliance gameDataObject)
        {
            Helper.SetupCounterLimitedItem(Prefab, Mod.Bundle.LoadAsset<GameObject>("Special Sauce Bottle").AssignMaterialsByNames().AssignVFXByNames());
        }
    }
}

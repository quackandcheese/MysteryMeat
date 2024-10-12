using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Dishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Customs.Cards
{
    public class CautiousCrowdCard : CustomUnlockCard
    {
        public override string UniqueNameID => "CautiousCrowdCard";
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override CardType CardType => CardType.Default;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Generic;
        public override bool IsUnlockable => true;
        public override List<Unlock> HardcodedRequirements => new()
        {
            (Dish)GDOUtils.GetCustomGameDataObject<MysteryMeatBurgerDish>().GameDataObject
        };
        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect()
            {
                Status = (RestaurantStatus)VariousUtils.GetID("cautiouscrowd")
            }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            ( Locale.English, LocalisationUtils.CreateUnlockInfo("Cautious Crowd", "Customers gain suspicion 50% faster", "They've heard some rumors") )
        };
    }
}

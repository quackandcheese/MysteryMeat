using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Interfaces;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Dishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Customs.Cards
{
    public class MessyMurderCard : CustomUnlockCard, IDontRegister
    {
        public override string UniqueNameID => "MessyMurderCard";
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
                Status = (RestaurantStatus)VariousUtils.GetID("messymurder")
            }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            ( Locale.English, LocalisationUtils.CreateUnlockInfo("Messy Murder", "Customers spill more blood when killed", "Tip: Invest in a mop!") )
        };
    }
}

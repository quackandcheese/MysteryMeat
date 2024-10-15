using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Customs.Dishes
{
    public class SpecialSauceDish : CustomDish
    {
        public override string UniqueNameID => "SpecialSauceDish";
        public override DishType Type => DishType.Extra;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override CardType CardType => CardType.Default;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsSpecificFranchiseTier => false;
        public override bool DestroyAfterModUninstall => false;
        public override bool IsUnlockable => true;
        public override int Difficulty => 1;

        public override List<Unlock> HardcodedRequirements => new()
        {
            (Dish)GDOUtils.GetCustomGameDataObject<MysteryMeatBurgerDish>().GameDataObject
        };

        public override HashSet<Dish.IngredientUnlock> ExtraOrderUnlocks => new HashSet<Dish.IngredientUnlock>
        {
            new Dish.IngredientUnlock
            {
                Ingredient = GDOUtils.GetCastedGDO<Item, SpecialSauceBottle>(),
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemReferences.BurgerPlated)
            },
            new Dish.IngredientUnlock
            {
                Ingredient = GDOUtils.GetCastedGDO<Item, SpecialSauceBottle>(),
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemReferences.HotdogPlated)
            },
            new Dish.IngredientUnlock
            {
                Ingredient = GDOUtils.GetCastedGDO<Item, SpecialSauceBottle>(),
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemReferences.PiePlated)
            },
        };

        public override HashSet<Item> MinimumIngredients => new HashSet<Item>
        {
            GDOUtils.GetCastedGDO<Item, EmptySpecialSauceBottle>(),
        };
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
        };

        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            //{ Locale.English, "Use with plated breakfast to add syrup, then serve." }

            { Locale.English, "Fill bottle with blood and serve when requested. Has 6 uses until a refill is needed" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            //( Locale.English, LocalisationUtils.CreateUnlockInfo("Maple Syrup", "Adds maple syrup as an American Breakfast topping", "Simple yet delicious") )
            ( Locale.English, LocalisationUtils.CreateUnlockInfo("Special Sauce", "Customers can request the 'special sauce' while eating", null) )
        };
    }
}

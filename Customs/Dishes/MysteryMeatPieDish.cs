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
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Dishes
{
    public class MysteryMeatPieDish : CustomDish
    {
        public override string UniqueNameID => "MysteryMeatPieDish";
        public override DishType Type => DishType.Main;
        public override GameObject DisplayPrefab => (GDOUtils.GetExistingGDO(DishReferences.PieBase) as Dish).DisplayPrefab;
        public override GameObject IconPrefab => (GDOUtils.GetExistingGDO(DishReferences.PieBase) as Dish).IconPrefab;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override CardType CardType => CardType.Default;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool DestroyAfterModUninstall => false;
        public override bool IsUnlockable => true;
        public override int Difficulty => 2;

        public override List<Unlock> HardcodedRequirements => new()
        {
            (Dish)GDOUtils.GetCustomGameDataObject<MysteryMeatBurgerDish>().GameDataObject
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new List<Dish.MenuItem>
        {
            new Dish.MenuItem
            {
                Item = (Item)GDOUtils.GetExistingGDO(ItemReferences.PiePlated),
                Phase = MenuPhase.Main,
                Weight = 1,
            }
        };


        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new HashSet<Dish.IngredientUnlock>
        {
            new Dish.IngredientUnlock
            {
                Ingredient = (Item)GDOUtils.GetExistingGDO(ItemReferences.PieMeatCooked),
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemReferences.PiePlated)
            }
        };

        public override HashSet<Item> MinimumIngredients => new HashSet<Item>
        {
            (Item)GDOUtils.GetCustomGameDataObject<MeatCleaver>().GameDataObject,
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Water),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Flour),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Plate),
        };
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            (Process)GDOUtils.GetExistingGDO(ProcessReferences.RequireOven),
            (Process)GDOUtils.GetExistingGDO(ProcessReferences.Knead),
        };

        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Knead flour (or add water) to create dough, then knead into pie crust. Add 'fresh meat' and cook." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, new UnlockInfo
            {
                Name = "Mystery Meat Pies",
                Description = "Adds \"fresh meat\" pies as a main",
                FlavourText = ""
            })
        };
    }
}

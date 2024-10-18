using System.Collections.Generic;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using KitchenMysteryMeat.Customs.Processes;


//using KitchenMysteryMeat.Customs.ItemGroups;
//using KitchenMysteryMeat.Customs.Items;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Dishes
{
    public class MysteryMeatBurgerDish : CustomDish
    {
        public override string UniqueNameID => "MysteryMeatBurgerDish";

        // ExpReward - Determines how much XP this Unlock provides.
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;

        // IsUnlockable - When TRUE this Unlock can appear in the card selector.
        public override bool IsUnlockable => true;

        // UnlockGroup - Determines what type of Unlock this is.
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;

        // CardType - Determines when this Unlock can be selected.
        public override CardType CardType => CardType.Default;

        // CustomerMultiplier - Determines the customer difference this Unlock provides.
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;

        // Type - This is used to decide what phase this Dish should be ordered.
        public override DishType Type => DishType.Base;

        // Difficulty - This is displayed in the lobby. (0 - 5)
        public override int Difficulty => 3;

        // StartingNameSet - The list of names used to decide the default Restaurant name.
        public override List<string> StartingNameSet => new List<string>
        {
            "We Won't Kill You",
            "Fresh Never Frozen"
        };

        // MinimumIngredients - The ingredients required to make this Dish.
        public override HashSet<Item> MinimumIngredients => new HashSet<Item>()
        {
            (Item)GDOUtils.GetCustomGameDataObject<MeatCleaver>().GameDataObject,
            (Item)GDOUtils.GetExistingGDO(ItemReferences.BurgerBun),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Water),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Plate),
        };

        // RequiredProcesses - The processes required to make this Dish.
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            (Process)GDOUtils.GetExistingGDO(ProcessReferences.Cook),
            GDOUtils.GetCastedGDO<Process, GrindMeat>()
        };

        // IconPrefab - This is the Icon displayed in the lobby.
        public override GameObject IconPrefab => Mod.Bundle.LoadAsset<GameObject>("Mystery Meat - Icon").AssignMaterialsByNames();
        public override GameObject DisplayPrefab => (GDOUtils.GetExistingGDO(DishReferences.BurgerBase) as Dish).DisplayPrefab;

        // ResultingMenuItems - What menu Items are available to customers after unlocking this Dish.
        public override List<Dish.MenuItem> ResultingMenuItems => new List<Dish.MenuItem>
        {
            new Dish.MenuItem
            {
                Item = (Item)GDOUtils.GetExistingGDO(ItemReferences.BurgerPlated),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuType = DynamicMenuType.Static,
                DynamicMenuIngredient = null
            }
        };

        // IsAvailableAsLobbyOption - When TRUE this Dish will appear in the lobby.
        public override bool IsAvailableAsLobbyOption => true;
        public override HashSet<Item> BlockProviders => new()
        {
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Meat),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.BurgerPattyRaw),
        };

        // Recipe - This is the recipe displayed when unlocking this Dish.
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Put 'fresh meat' in meat grinder to get minced meat. Knead to form patty. Cook burger patty and add bun." }
        };

        // InfoList - This is used to assign localisation to this Dish.
        public override List<(Locale, UnlockInfo)> InfoList => new List<(Locale, UnlockInfo)>
        {
            (Locale.English, new UnlockInfo
            {
                Name = "Mystery Meat Burgers",
                Description = "Adds \"fresh meat\" burgers as a main",
                FlavourText = ""
            })
        };

        public override List<Dish> AlsoAddRecipes => new()
        {
            (Dish)GDOUtils.GetCustomGameDataObject<MysteryMeatDish>().GameDataObject
        };
    }
}
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using KitchenMysteryMeat.Customs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Dishes
{
    public class MysteryMeatHotdogDish : CustomDish
    {
        public override string UniqueNameID => "MysteryMeatHotdogDish";
        public override DishType Type => DishType.Main;
        public override GameObject DisplayPrefab => (GDOUtils.GetExistingGDO(DishReferences.HotdogBase) as Dish).DisplayPrefab;
        public override GameObject IconPrefab => (GDOUtils.GetExistingGDO(DishReferences.HotdogBase) as Dish).IconPrefab;
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
                Item = (Item)GDOUtils.GetExistingGDO(ItemReferences.HotdogPlated),
                Phase = MenuPhase.Main,
                Weight = 1,

            }
        };

        public override HashSet<Item> MinimumIngredients => new HashSet<Item>
        {
            (Item)GDOUtils.GetCustomGameDataObject<MeatCleaver>().GameDataObject,
            (Item)GDOUtils.GetCustomGameDataObject<Casing>().GameDataObject,
            (Item)GDOUtils.GetExistingGDO(ItemReferences.HotdogBun),
        };
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            (Process)GDOUtils.GetExistingGDO(ProcessReferences.Cook),
            GDOUtils.GetCastedGDO<Process, GrindMeat>()
        };

        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Put 'fresh meat' in meat grinder to get minced meat. Combine with a hot dog casing, cook hot dog, and place in bun." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, new UnlockInfo
            {
                Name = "Mystery Meat Hot Dogs",
                Description = "Adds \"fresh meat\" hot dogs as a main",
                FlavourText = ""
            })
        };
    }
}

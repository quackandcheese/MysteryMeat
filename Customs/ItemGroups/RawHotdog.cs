using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.ItemGroups
{
    public class RawHotdog : CustomItemGroup
    {
        public override string UniqueNameID => "RawHotdogItemGroup";
        public override GameObject Prefab => ((Item)GDOUtils.GetExistingGDO(ItemReferences.HotdogRaw)).Prefab;
        public override bool AutoCollapsing => true;

        public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>()
        {
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new List<Item>()
                {
                    (Item)GDOUtils.GetExistingGDO(ItemReferences.Mince)
                }
            },
            new ItemGroup.ItemSet()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new List<Item>()
                {
                    GDOUtils.GetCastedGDO<Item, Casing>()
                }
            }
        };

        public override List<IItemProperty> Properties => new List<IItemProperty>()
        {
            new CTurnIntoItem()
            {
                NewID = ItemReferences.HotdogRaw
            }
        };
    }
}

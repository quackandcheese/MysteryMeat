using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace KitchenMysteryMeat.Components
{
    public struct CLimitedUseBottle : IModComponent, IItemProperty, IAttachableProperty
    {
        public int Limit;
        public int FillAmount;
        public Entity LastUsedByCustomer;
        public int EmptyBottleID;
    }
}

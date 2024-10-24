using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Components
{
    public struct CPersistPortions : IModComponent, IItemProperty, IAttachableProperty
    {
        public int RemainingCount;
        public int TotalCount;
    }
}

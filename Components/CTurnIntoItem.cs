﻿using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Components
{
    public struct CTurnIntoItem : IModComponent, IItemProperty, IAttachableProperty
    {
        public int NewID;
    }
}

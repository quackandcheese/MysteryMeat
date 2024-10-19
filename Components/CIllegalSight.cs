﻿using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Components
{
    public struct CIllegalSight : IModComponent, IAttachableProperty, IItemProperty, IApplianceProperty
    {
        //public bool PersistBetweenDays;
        public int TurnIntoOnDayStart;
    }
}

using KitchenMods;
using KitchenMysteryMeat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace KitchenMysteryMeat.Components
{
    public struct CSuspicionIndicator: IModComponent
    {
        public SuspicionIndicatorType IndicatorType;
        public bool SeenIllegalThing;
        public float TotalTime;
        public float RemainingTime;
    }
}

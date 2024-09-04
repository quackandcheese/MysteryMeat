using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace KitchenMysteryMeat.Components
{
    public enum SuspicionIndicatorType 
    {
        Suspicious,
        Alert
    }

    public struct CSuspicionIndicator: IModComponent
    {
        public SuspicionIndicatorType IndicatorType;
        public bool SeenIllegalThing;
        public float TotalTime;
        public float RemainingTime;
    }
}

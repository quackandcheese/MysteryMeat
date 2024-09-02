using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMysteryMeat.Components
{
    public struct CProcessCausesSpill : IModComponent, IItemProperty, IAttachableProperty
    {
        public int Process;
        // ID of Spill
        public int ID;
        // Rate of Spilling
        public float Rate;
        // Will this replace other messes
        public bool OverwriteOtherMesses;
    }
}

using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Components
{
    public struct CMeatGrinder : IModComponent, IAttachableProperty, IApplianceProperty
    {
        public Vector3 GrinderInputPosition;
        public Vector3 GrinderOutputPosition;
        public int GrindProcess;
    }
}

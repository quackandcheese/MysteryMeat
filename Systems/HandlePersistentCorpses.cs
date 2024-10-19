using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    internal class HandlePersistentCorpses : StartOfDaySystem, IModSystem
    {
        EntityQuery Illegals;

        protected override void Initialise()
        {
            base.Initialise();

            Illegals = GetEntityQuery(new QueryHelper()
                            .All(typeof(CIllegalSight))
                            .Any(typeof(CItem), typeof(CAppliance)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _illegals = Illegals.ToEntityArray(Allocator.Temp);

            for (int i = 0; i < _illegals.Length; i++)
            {
                Entity illegalEntity = _illegals[i];

                CIllegalSight illegalSight = GetComponent<CIllegalSight>(illegalEntity);

                if (Require<CItem>(out var cItem))
                {
                    // Turn into illegalSight.TurnIntoOnDayStart
                }
                else if (Require<CAppliance>(out var cAppliance))
                {
                    // Turn into illegalSight.TurnIntoOnDayStart
                }
            }
        }
    }
}

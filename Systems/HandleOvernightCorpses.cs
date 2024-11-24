using Kitchen;
using KitchenData;
using KitchenLib.Utils;
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
    public class HandleOvernightCorpses : GameSystemBase, IModSystem
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
            bool statusFound = HasStatus((RestaurantStatus)VariousUtils.GetID("persistentcorpses"));

            using NativeArray<Entity> _illegals = Illegals.ToEntityArray(Allocator.Temp);

            for (int i = _illegals.Length - 1; i >= 0; i--)
            {
                Entity illegalEntity = _illegals[i];
                CIllegalSight illegalSight = GetComponent<CIllegalSight>(illegalEntity);

                if (!GameData.Main.TryGet(illegalSight.TurnIntoOnDayStart, out Appliance _, true) &&
                    !GameData.Main.TryGet(illegalSight.TurnIntoOnDayStart, out Item _, true))
                    continue;

                if (Require<CItem>(illegalEntity, out var _cItem))
                {
                    if (statusFound)
                        Set(illegalEntity, new CPreservedOvernight());
                    else if (Has<CPreservedOvernight>(illegalEntity))
                        EntityManager.RemoveComponent<CPreservedOvernight>(illegalEntity);
                }
                else if (Require<CAppliance>(illegalEntity, out var _cAppliance))
                {
                    if (statusFound)
                        EntityManager.RemoveComponent<CDestroyApplianceAtNight>(illegalEntity);
                    else
                        Set(illegalEntity, new CDestroyApplianceAtNight());
                }
            }
        }
    }
}

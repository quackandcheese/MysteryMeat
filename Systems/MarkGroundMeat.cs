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
    [UpdateInGroup(typeof(ApplianceProcessReactionGroup))]
    public class MarkGroundMeat : GameSystemBase, IModSystem
    {
        private EntityQuery Appliances;
        protected override void Initialise()
        {
            Appliances = GetEntityQuery(new QueryHelper()
                            .All(typeof(CMeatGrinder), typeof(CCompletedProcess), typeof(CItemHolder)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _appliances = Appliances.ToEntityArray(Allocator.TempJob);

            foreach (Entity appliance in _appliances)
            {
                CCompletedProcess cCompletedProcess = EntityManager.GetComponentData<CCompletedProcess>(appliance);
                CMeatGrinder cMeatGrinder = EntityManager.GetComponentData<CMeatGrinder>(appliance);
                CItemHolder cItemHolder = EntityManager.GetComponentData<CItemHolder>(appliance);

                if (cCompletedProcess.Process == cMeatGrinder.GrindProcess)
                {
                    if (Has<CGrindable>(cItemHolder.HeldItem))
                    {
                        EntityManager.RemoveComponent<CGrindable>(cItemHolder.HeldItem);
                    }
                }
            }
        }
    }
}

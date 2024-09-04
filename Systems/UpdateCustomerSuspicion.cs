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
using UnityEngine;
using static Sony.NP.Matching;

namespace KitchenMysteryMeat.Systems
{
    public class UpdateCustomerSuspicion : DaySystem, IModSystem
    {
        EntityQuery SuspicionIndicators;
        protected override void Initialise()
        {
            base.Initialise();
            SuspicionIndicators = GetEntityQuery(new QueryHelper()
                            .All(typeof(CCustomer), typeof(CSuspicionIndicator)));

        }
        protected override void OnUpdate()
        {
            using NativeArray<Entity> _suspicionIndicators = SuspicionIndicators.ToEntityArray(Allocator.Temp);

            foreach (Entity customer in _suspicionIndicators)
            {
                CSuspicionIndicator susIndicator = EntityManager.GetComponentData<CSuspicionIndicator>(customer);

                if (susIndicator.TotalTime <= 0.0f)
                    continue;

                if (!susIndicator.Active)
                {
                    susIndicator.RemainingTime = susIndicator.TotalTime;
                    continue;
                }

                susIndicator.RemainingTime = Mathf.Clamp(susIndicator.RemainingTime - Time.DeltaTime, 0.0f, susIndicator.TotalTime);

                EntityManager.SetComponentData(customer, susIndicator);

                if (susIndicator.RemainingTime <= 0.0f)
                {
                    // Run away and make into alert indicator

                    // Remove customer from group
                    if (Require<CBelongsToGroup>(customer, out CBelongsToGroup cBelongsToGroup) && RequireBuffer<CGroupMember>(cBelongsToGroup.Group, out DynamicBuffer<CGroupMember> groupMembers))
                    {
                        for (int j = groupMembers.Length - 1; j > -1; j--)
                        {
                            if (groupMembers[j].Customer != customer)
                                continue;
                            groupMembers.RemoveAt(j);
                            break;
                        }
                    }

                    // Make leave
                    EntityManager.AddComponent<CCustomerLeaving>(customer);
                    EntityManager.AddComponent<CRunningAway>(customer);

                    susIndicator.Active = false;
                    EntityManager.SetComponentData(customer, susIndicator);
                }
            }
        }
    }
}

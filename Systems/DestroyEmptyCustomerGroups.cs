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
    [UpdateInGroup(typeof(DestructionGroup)), UpdateAfter(typeof(KillCustomers))]
    public class DestroyEmptyCustomerGroups : DaySystem, IModSystem
    {
        EntityQuery CustomerGroups;

        protected override void Initialise()
        {
            base.Initialise();

            CustomerGroups = GetEntityQuery(typeof(CCustomerGroup));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _customerGroups = CustomerGroups.ToEntityArray(Allocator.Temp);

            for (int i = _customerGroups.Length - 1; i > -1; i--)
            {
                Entity customerGroup = _customerGroups[i];

                if (RequireBuffer<CGroupMember>(customerGroup, out DynamicBuffer<CGroupMember> groupMembers))
                {
                    if (groupMembers.Length <= 0)
                    {
                        if (Require<CHasIndicator>(customerGroup, out CHasIndicator cHasIndicator))
                        {
                            EntityManager.DestroyEntity(cHasIndicator.Indicator);
                        }

                        EntityManager.DestroyEntity(customerGroup);
                    }
                }
            }
        }
    }
}

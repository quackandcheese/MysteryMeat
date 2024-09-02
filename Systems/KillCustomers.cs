using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Components;
using Steamworks.Ugc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    [UpdateInGroup(typeof(DestructionGroup))]
    public class KillCustomers : DaySystem, IModSystem
    {
        EntityQuery CustomersToKill;

        protected override void Initialise()
        {
            base.Initialise();
            CustomersToKill = GetEntityQuery(typeof(CCustomer), typeof(CKilled));
        }

        protected override void OnUpdate()
        {
            NativeArray<Entity> customers = CustomersToKill.ToEntityArray(Allocator.Temp);

            for (int i = 0; i < customers.Length; i++)
            {
                Entity customer = customers[i];
                if (!Require(customer, out CBelongsToGroup belongsToGroup) ||
                    !RequireBuffer(belongsToGroup.Group, out DynamicBuffer<CGroupMember> groupMembers))

                    continue;
                for (int j = groupMembers.Length - 1; j > -1; j--)
                {
                    if (groupMembers[j].Customer != customer)
                        continue;
                    groupMembers.RemoveAt(j);
                    break;
                }

                if (groupMembers.Length == 0)
                {
                    EntityManager.DestroyEntity(belongsToGroup.Group);
                }
            }
            EntityManager.DestroyEntity(CustomersToKill);

            customers.Dispose();
        }
    }
}

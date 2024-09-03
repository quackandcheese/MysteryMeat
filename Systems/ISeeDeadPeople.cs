using Kitchen;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.UIElements;
using UnityEngine;
using KitchenMysteryMeat.Components;

namespace KitchenMysteryMeat.Systems
{
    public class ISeeDeadPeople : GenericSystemBase, IModSystem
    {
        EntityQuery CustomerGroups;
        EntityQuery IllegalEntities;

        protected override void Initialise()
        {
            base.Initialise();

            CustomerGroups = GetEntityQuery(new QueryHelper()
                            .All(typeof(CCustomerGroup))
                            .None(
                                typeof(CGroupLeaving)
                            ));
            IllegalEntities = GetEntityQuery(new QueryHelper()
                            .All(typeof(CIllegalSight)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _customerGroups = CustomerGroups.ToEntityArray(Allocator.Temp);
            using NativeArray<Entity> _illegalEntities = IllegalEntities.ToEntityArray(Allocator.Temp);

            for (int i = _customerGroups.Length - 1; i > -1; i--)
            {
                Entity customerGroup = _customerGroups[i];

                DynamicBuffer<CGroupMember> groupMembers = EntityManager.GetBuffer<CGroupMember>(customerGroup);

                for (int j = groupMembers.Length - 1; j > -1; j--)
                {
                    Entity member = groupMembers[j].Customer;


                    if (Has<CRunningAway>(member))
                    {
                        continue;
                    }

                    if (!Require<CPosition>(member, out CPosition memberPosition))
						{
                        continue;
                    }

                    foreach (Entity illegalEntity in _illegalEntities)
                    {
                        CPosition illegalEntityPos;
                        if (!Require<CPosition>(illegalEntity, out illegalEntityPos))
                        {
                            if (Require<CHeldBy>(illegalEntity, out CHeldBy cHeldBy) && !Require<CPosition>(cHeldBy.Holder, out illegalEntityPos))
                                continue;
                        }

                        // This is so they can only see the illegal entity if it is in the same room.
                        if (TileManager.GetRoom(illegalEntityPos) != TileManager.GetRoom(memberPosition))
                            continue;

                        // Checking if illegal entity is in customer's view
                        Vector3 vector = illegalEntityPos.Position - memberPosition.Position;
                        if (vector.sqrMagnitude < 4f)
                        {
                            Vector3 rhs = memberPosition.Forward(1f);
                            if (Vector3.Dot(vector.normalized, rhs) > 1f - Mathf.Cos((float)Math.PI / 5f))
                            {
                                // Run away!
                                groupMembers.RemoveAt(j);
                                EntityManager.AddComponent<CCustomerLeaving>(member);
                                EntityManager.AddComponent<CRunningAway>(member);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}

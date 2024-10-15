﻿using Kitchen;
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

    [UpdateBefore(typeof(GroupReceiveExtra))]
    public class SpecialSauceServing : GameSystemBase, IModSystem
    {
        private EntityQuery GroupQuery;
        public bool has_found_item;
        protected override void Initialise()
        {
            GroupQuery = GetEntityQuery(new QueryHelper()
                            .All(typeof(CWaitingForItem), typeof(CGroupReward), typeof(CPatience), typeof(CGroupMember), typeof(CCustomerSettings), typeof(CItemHolder)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> groups = GroupQuery.ToEntityArray(Allocator.TempJob);

            foreach (Entity group in groups)
            {
                DynamicBuffer<CWaitingForItem> orders = GetBuffer<CWaitingForItem>(group);
                DynamicBuffer<CGroupMember> groupMembers = GetBuffer<CGroupMember>(group);
                CItemHolder citemHolder = GetComponent<CItemHolder>(group);

                for (int i = 0; i < orders.Length; i++)
                {
                    if (orders[i].ExtraSatisfied)
                    {
                        if (citemHolder.HeldItem != Entity.Null && Require<CLimitedUseBottle>(citemHolder.HeldItem, out var limitedUseBottle))
                        {
                            // TODO: fix bug for double orders by same customer
                            if (limitedUseBottle.LastUsedByCustomer == groupMembers[orders[i].MemberIndex].Customer)
                                continue;

                            limitedUseBottle.FillAmount -= 1;
                            limitedUseBottle.LastUsedByCustomer = groupMembers[orders[i].MemberIndex].Customer;

                            EntityManager.SetComponentData(citemHolder.HeldItem, limitedUseBottle);
                        }
                    }
                }
            }
        }
    }
}

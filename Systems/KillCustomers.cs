using ExitGames.Client.Photon.StructWrapping;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Appliances;
using Steamworks.Ugc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UIElements;

namespace KitchenMysteryMeat.Systems
{
    [UpdateInGroup(typeof(DestructionGroup), OrderFirst = true)]
    public class KillCustomers : DaySystem, IModSystem
    {
        EntityQuery CustomersToKill;
        EntityQuery OrderIndicators;

        protected override void Initialise()
        {
            base.Initialise();
            CustomersToKill = GetEntityQuery(typeof(CCustomer), typeof(CKilled));
            OrderIndicators = GetEntityQuery(typeof(CHasItemCollectionIndicator));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _customers = CustomersToKill.ToEntityArray(Allocator.Temp);
            using NativeArray<Entity> _orderIndicators = OrderIndicators.ToEntityArray(Allocator.Temp);
            EntityContext ctx = new EntityContext(EntityManager);

            for (int i = 0; i < _customers.Length; i++)
            {
                Entity customer = _customers[i];

                CPosition customerPosition = EntityManager.GetComponentData<CPosition>(customer);
                CreateCorpse(ctx, customerPosition);

                // Destroy order indicator

                if (!Require(customer, out CBelongsToGroup belongsToGroup) ||
                    !RequireBuffer(belongsToGroup.Group, out DynamicBuffer<CGroupMember> groupMembers))

                    continue;


                for (int j = groupMembers.Length - 1; j > -1; j--)
                {
                    if (groupMembers[j].Customer != customer)
                        continue;
                    groupMembers.RemoveAt(j);

                    if (RequireBuffer<CWaitingForItem>(belongsToGroup.Group, out DynamicBuffer<CWaitingForItem> waitingForItems))
                        waitingForItems.RemoveAt(j);
                    break;
                }

                // In case there are any order indicators that the customer has, destroy them before destroying the customer.
                /*for (int j = _orderIndicators.Length - 1; j > -1; j--)
                {
                    Entity indicatorEntity = _orderIndicators[j];

                    CHasItemCollectionIndicator cIndicator = GetComponent<CHasItemCollectionIndicator>(indicatorEntity);
                    if (!RequireBuffer<CDisplayedItem>(cIndicator.Indicator, out DynamicBuffer<CDisplayedItem> displayedItems))
                        continue;
                    if (!Require<CCustomerTablePlacement>(customer, out CCustomerTablePlacement tablePlacement))
                        continue;
                    for (int k = displayedItems.Length - 1; k > -1; k--)
                    {
                        CDisplayedItem displayedItem = displayedItems[k];

                        if (tablePlacement.SeatPosition == displayedItem.SeatPosition)
                        {
                            // IT WORKS, BUT I THINK WRONG WAITING FOR ITEM IS GETTING DELETED OR SOMETHING
                            displayedItems.RemoveAt(k);
                            break;
                        }

                    }
                }*/
            }

            EntityManager.DestroyEntity(CustomersToKill);
        }

        private void CreateCorpse(EntityContext ctx, CPosition cPosition)
        {
            // Creating corpse
            Entity corpse = ctx.CreateEntity();
            int corpseID = GDOUtils.GetCustomGameDataObject<CustomerFloorCorpse>().ID;
            ctx.Set<CCreateAppliance>(corpse, new CCreateAppliance
            {
                ID = corpseID,
                ForceLayer = OccupancyLayer.Ceiling
            });
            ctx.Set<CPosition>(corpse, new CPosition(cPosition.Position, cPosition.Rotation));

            // Creating blood spills
            int minbloodSpills = 1;
            int maxbloodSpills = 4;
            for (int i = 0; i < UnityEngine.Random.Range(minbloodSpills, maxbloodSpills + 1); i++)
            {
                Entity bloodSpill = ctx.CreateEntity();
                ctx.Set<CMessRequest>(bloodSpill, new CMessRequest
                {
                    ID = GDOUtils.GetCustomGameDataObject<BloodSpill1>().ID,
                    OverwriteOtherMesses = false
                });

                // This is so spills don't spawn out of bounds, becoming an uncleanable illegal sight
                // Doesn't work though since mess request creates the mess appliances
                /*if (!TileManager.IsSuitableEmptyTile(cPosition, allow_oob: false, allow_outside: true))
                    continue;*/

                ctx.Set<CPosition>(bloodSpill, new CPosition(cPosition.Position));
            }
        }
    }
}

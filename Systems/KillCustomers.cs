using ExitGames.Client.Photon.StructWrapping;
using Kitchen;
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
            using NativeArray<Entity> customers = CustomersToKill.ToEntityArray(Allocator.Temp);
            EntityContext ctx = new EntityContext(EntityManager);

            for (int i = 0; i < customers.Length; i++)
            {
                Entity customer = customers[i];

                CPosition customerPosition = EntityManager.GetComponentData<CPosition>(customer);
                CreateCorpse(ctx, customerPosition.Position);

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

                /*if (groupMembers.Length == 0)
                {
                    EntityManager.DestroyEntity(belongsToGroup.Group);
                }*/
            }

            EntityManager.DestroyEntity(CustomersToKill);
        }

        private void CreateCorpse(EntityContext ctx, Vector3 position)
        {
            // Creating corpse
            Entity entity = ctx.CreateEntity();
            int corpseID = GDOUtils.GetCustomGameDataObject<CustomerFloorCorpse>().ID;
            ctx.Set<CCreateAppliance>(entity, new CCreateAppliance
            {
                ID = corpseID
            });
            ctx.Set<CPosition>(entity, new CPosition(position));

            // Creating blood spills
            int minbloodSpills = 2;
            int maxbloodSpills = 6;
            for (int i = 0; i < UnityEngine.Random.Range(minbloodSpills, maxbloodSpills + 1); i++)
            {
                Entity bloodSpill = ctx.CreateEntity();
                ctx.Set<CMessRequest>(bloodSpill, new CMessRequest
                {
                    ID = GDOUtils.GetCustomGameDataObject<BloodSpill1>().ID,
                    OverwriteOtherMesses = false
                });
                ctx.Set<CPosition>(bloodSpill, new CPosition(position));
            }
        }
    }
}

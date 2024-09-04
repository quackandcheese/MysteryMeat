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

        protected override void Initialise()
        {
            base.Initialise();
            CustomersToKill = GetEntityQuery(typeof(CCustomer), typeof(CKilled));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _customers = CustomersToKill.ToEntityArray(Allocator.Temp);
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
                    break;
                }
                /*if (groupMembers.Length == 0)
                {
                    EntityManager.DestroyEntity(belongsToGroup.Group);
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
                if (!TileManager.IsSuitableEmptyTile(cPosition, allow_oob: false, allow_outside: true))
                    continue;

                ctx.Set<CPosition>(bloodSpill, new CPosition(cPosition.Position));
            }
        }
    }
}

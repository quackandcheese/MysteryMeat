﻿using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Appliances;
using Sony.NP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.UIElements;

namespace KitchenMysteryMeat.Systems
{
    internal class HandlePersistentCorpses : StartOfDaySystem, IModSystem
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
            using NativeArray<Entity> _illegals = Illegals.ToEntityArray(Allocator.Temp);

            for (int i = _illegals.Length - 1; i >= 0; i--)
            {
                Entity illegalEntity = _illegals[i];
                CIllegalSight illegalSight = GetComponent<CIllegalSight>(illegalEntity);

                if (!GameData.Main.TryGet(illegalSight.TurnIntoOnDayStart, out Appliance _, true) &&
                    !GameData.Main.TryGet(illegalSight.TurnIntoOnDayStart, out Item _, true))
                    continue;

                if (Require<CItem>(illegalEntity, out var cItem))
                {
                    // Turn into illegalSight.TurnIntoOnDayStart
                    EntityManager.AddComponentData<CChangeItemType>(illegalEntity, new CChangeItemType()
                    {
                        NewID = illegalSight.TurnIntoOnDayStart,
                    });
                }
                else if (Require<CAppliance>(illegalEntity, out var cAppliance))
                {
                    // Turn into illegalSight.TurnIntoOnDayStart
                    if (Require<CPosition>(illegalEntity, out var cPosition))
                    {
                        Entity corpse = EntityManager.CreateEntity();
                        Set<CCreateAppliance>(corpse, new CCreateAppliance
                        {
                            ID = illegalSight.TurnIntoOnDayStart,
                            ForceLayer = OccupancyLayer.Ceiling
                        });
                        Set<CPosition>(corpse, new CPosition(cPosition.Position, cPosition.Rotation));

                        EntityManager.DestroyEntity(illegalEntity);
                    }
                }
            }
        }
    }
}

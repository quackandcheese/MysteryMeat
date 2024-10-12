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

namespace KitchenMysteryMeat.Systems
{
    public class CreateNewProcessSpills : GameSystemBase, IModSystem
    {
        EntityQuery ItemsUndergoingProcess;

        protected override void Initialise()
        {
            base.Initialise();
            ItemsUndergoingProcess = GetEntityQuery(typeof(CItem), typeof(CHeldBy), typeof(CProcessCausesSpill), typeof(CItemUndergoingProcess));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _items = ItemsUndergoingProcess.ToEntityArray(Allocator.Temp);

            foreach (Entity item in _items)
            {
                CProcessCausesSpill cProcessCausesSpill = EntityManager.GetComponentData<CProcessCausesSpill>(item);
                CItemUndergoingProcess cItemUndergoingProcess = EntityManager.GetComponentData<CItemUndergoingProcess>(item);
                CHeldBy cHeldBy = EntityManager.GetComponentData<CHeldBy>(item);
                CPosition cPosition;

                if (!Require<CPosition>(cHeldBy.Holder, out cPosition))
                    continue;

                if (cItemUndergoingProcess.Process != cProcessCausesSpill.Process)
                    continue;

                if (cItemUndergoingProcess.Progress >= 0.9)
                    continue;

                if (UnityEngine.Random.value < cProcessCausesSpill.Rate * Time.DeltaTime)
                {
                    Entity spill = EntityManager.CreateEntity();
                    EntityManager.AddComponentData<CMessRequest>(spill, new CMessRequest
                    {
                        ID = cProcessCausesSpill.ID,
                        OverwriteOtherMesses = cProcessCausesSpill.OverwriteOtherMesses
                    });
                    EntityManager.AddComponentData<CPosition>(spill, cPosition);
                }
            }
        }
    }
}

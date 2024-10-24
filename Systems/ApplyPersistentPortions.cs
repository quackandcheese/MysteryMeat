using Kitchen;
using KitchenData;
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
    //[UpdateAfter(typeof(ObjectsSplittableView.UpdateView))]
    public class ApplyPersistentPortions : DaySystem, IModSystem
    {
        EntityQuery Query;

        protected override void Initialise()
        {
            base.Initialise();

            Query = GetEntityQuery(new QueryHelper()
                            .All(typeof(CPersistPortions), typeof(CItem), typeof(CSplittableItem), typeof(CLinkedView)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _splittableItems = Query.ToEntityArray(Allocator.Temp);

            for (int i = _splittableItems.Length - 1; i >= 0; i--)
            {
                Entity splittableItem = _splittableItems[i];

                CPersistPortions cPersistPortions = GetComponent<CPersistPortions>(splittableItem);
                CSplittableItem cSplittableItem = GetComponent<CSplittableItem>(splittableItem);
                cSplittableItem.TotalCount = cPersistPortions.TotalCount;
                cSplittableItem.RemainingCount = cPersistPortions.RemainingCount;

                Set<CSplittableItem>(splittableItem, cSplittableItem);

                EntityManager.RemoveComponent<CPersistPortions>(splittableItem);
            }
        }
    }
}

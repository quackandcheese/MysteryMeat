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
    public class ReplaceEmptySpecialSauce : GenericSystemBase, IModSystem
    {
        EntityQuery Items;

        protected override void Initialise()
        {
            base.Initialise();

            Items = GetEntityQuery(typeof(CItem), typeof(CLimitedUseBottle));

        }
        protected override void OnUpdate()
        {
            using NativeArray<Entity> _items = Items.ToEntityArray(Allocator.Temp);

            foreach (Entity item in _items)
            {
                CLimitedUseBottle cLimitedUseBottle = GetComponent<CLimitedUseBottle>(item);

                if (cLimitedUseBottle.FillAmount <= 0)
                {
                    EntityManager.AddComponentData<CChangeItemType>(item, new CChangeItemType()
                    {
                        NewID = cLimitedUseBottle.EmptyBottleID,
                        CollapseComponents = true
                    });

                    EntityManager.RemoveComponent<CLimitedUseBottle>(item);
                }
            }
        }
    }
}

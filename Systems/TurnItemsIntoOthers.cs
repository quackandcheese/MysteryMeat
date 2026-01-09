using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Components;
using Unity.Collections;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    public class TurnItemsIntoOthers : GenericSystemBase, IModSystem
    {
        EntityQuery Items;

        protected override void Initialise()
        {
            base.Initialise();

            Items = GetEntityQuery(typeof(CItem), typeof(CTurnIntoItem));

        }
        protected override void OnUpdate()
        {
            using NativeArray<Entity> _items = Items.ToEntityArray(Allocator.Temp);

            foreach (Entity item in _items)
            {
                CTurnIntoItem cTurnIntoItem = GetComponent<CTurnIntoItem>(item);

                if (cTurnIntoItem.NewID == 0)
                {
                    return;
                }
                EntityManager.AddComponentData<CChangeItemType>(item, new CChangeItemType()
                {
                    NewID = cTurnIntoItem.NewID,
                });
                EntityManager.RemoveComponent<CTurnIntoItem>(item);
            }
        }
    }
}

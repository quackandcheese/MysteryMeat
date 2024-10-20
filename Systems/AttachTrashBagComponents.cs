using Kitchen;
using KitchenData;
using KitchenLib.Utils;
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
    public class AttachTrashBagComponents : GameSystemBase, IModSystem
    {
        private EntityQuery TrashBags;
        protected override void Initialise()
        {
            TrashBags = GetEntityQuery(new QueryHelper()
                            .All(typeof(CTrashBag), typeof(CItem))
                            .None(typeof(CItemStorage)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> _trashBags = TrashBags.ToEntityArray(Allocator.Temp);

            foreach (Entity trashBag in _trashBags)
            {
                Set<CItemStorage>(trashBag, new CItemStorage()
                {
                    Capacity = 1
                });
                EntityManager.AddBuffer<CItemStored>(trashBag);
            }
        }
    }
}

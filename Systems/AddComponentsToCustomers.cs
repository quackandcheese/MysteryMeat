using Kitchen;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    public class AddComponentsToCustomers : GameSystemBase, IModSystem
    {
        private EntityQuery _customers;
        protected override void Initialise()
        {
            _customers = GetEntityQuery(new QueryHelper()
                            .All(typeof(CCustomer))
                            .None(
                                typeof(CIsInteractive)
                            ));
        }

        protected override void OnUpdate()
        {
            using (NativeArray<Entity> customers = _customers.ToEntityArray(Allocator.TempJob))
            {
                foreach (Entity customer in customers)
                {
                    EntityManager.AddComponent<CIsInteractive>(customer);
                }
            }
        }
    }
}

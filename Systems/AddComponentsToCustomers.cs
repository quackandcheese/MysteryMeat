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
    public class AddComponentsToCustomers : GameSystemBase, IModSystem
    {
        private EntityQuery CustomersWithoutInteractive;
        private EntityQuery CustomersWithoutSuspicionIndicator;
        protected override void Initialise()
        {
            CustomersWithoutInteractive = GetEntityQuery(new QueryHelper()
                            .All(typeof(CCustomer))
                            .None(
                                typeof(CIsInteractive)
                            ));

            CustomersWithoutSuspicionIndicator = GetEntityQuery(new QueryHelper()
                            .All(typeof(CCustomer))
                            .None(
                                typeof(CSuspicionIndicator)
                            ));
        }

        protected override void OnUpdate()
        {
            EntityManager.AddComponent<CIsInteractive>(CustomersWithoutInteractive);

            using NativeArray<Entity> _customersWithoutSuspicionIndicator = CustomersWithoutSuspicionIndicator.ToEntityArray(Allocator.TempJob);

            float totalTime = HasStatus((RestaurantStatus)VariousUtils.GetID("cautiouscrowd")) ? 1.0f : 2.0f;

            foreach (Entity customer in _customersWithoutSuspicionIndicator)
            {
                EntityManager.AddComponentData(customer, new CSuspicionIndicator()
                {
                    IndicatorType = Enums.SuspicionIndicatorType.Suspicious,
                    TotalTime = totalTime,
                    RemainingTime = totalTime,
                });
            }
        }
    }
}

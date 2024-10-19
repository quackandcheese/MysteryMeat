using Kitchen;
using KitchenLib.Utils;
using KitchenLib.Views;
using KitchenMods;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace KitchenMysteryMeat.Systems
{
    //https://github.com/ZekNikZ/PlateUpCompetitiveMode/blob/main/Systems/RestaurantInitSystem.cs
    [UpdateInGroup(typeof(ChangeModeGroup))]
    public class RestaurantInitSystem : GenericSystemBase, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();
            RequireSingletonForUpdate<SCreateScene>();
        }

        protected override void OnUpdate()
        {
            if (GetSingleton<SCreateScene>().Type != SceneType.Kitchen)
            {
                return;
            }
            Mod.Logger.LogInfo("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            if (!Mod.WantedDisplayPrefab)
            {
                var prefab = Mod.VanillaAssetDirectory.ViewPrefabs[ViewType.MoneyDisplay];

                var parent = GameObject.Find("ViewPrefabs");
                if (parent == null)
                {
                    parent = new GameObject("ViewPrefabs");
                    parent.transform.localPosition = Vector3.positiveInfinity;
                    parent.SetActive(false);
                }

                Mod.WantedDisplayPrefab = UnityEngine.Object.Instantiate(prefab, parent.transform);
                Mod.WantedDisplayPrefab.name = $"View Wanted Display";

                UnityEngine.Object.Destroy(Mod.WantedDisplayPrefab.GetComponent<MoneyDisplayView>());
                UnityEngine.Object.Destroy(Mod.WantedDisplayPrefab.transform.Find("Value"));
                UnityEngine.Object.Destroy(Mod.WantedDisplayPrefab.transform.Find("Unit"));

                var view = Mod.WantedDisplayPrefab.AddComponent<WantedDisplayView>();
                view.IconParent = UnityEngine.Object.Instantiate(Mod.Bundle.LoadAsset<GameObject>("Wanted Display"));
                view.IconParent.transform.SetParent(Mod.WantedDisplayPrefab.transform.GetChild(0));

                var pos = prefab.transform.GetChild(0).localPosition;
                pos.x *= -1;
                Mod.WantedDisplayPrefab.transform.GetChild(0).localPosition = pos;
            }

            AddNewView((ViewType)VariousUtils.GetID("WantedDisplay"), new Vector3(1, 1, 0), new CWantedDisplay
            {
                Level = 0
            });
        }

        private Entity AddNewView<T>(ViewType view, Vector3 pos) where T : IComponentData
        {
            EntityManager entityManager = base.EntityManager;
            Entity entity = entityManager.CreateEntity();
            entityManager.AddComponent<T>(entity);
            entityManager.AddComponentData(entity, (CPosition)pos);
            entityManager.AddComponentData(entity, new CRequiresView
            {
                Type = view,
                ViewMode = ViewMode.Screen
            });
            return entity;
        }

        private Entity AddNewView<T>(ViewType view, Vector3 pos, T data) where T : struct, IComponentData
        {
            EntityManager entityManager = base.EntityManager;
            Entity entity = entityManager.CreateEntity();
            entityManager.AddComponentData(entity, data);
            entityManager.AddComponentData(entity, (CPosition)pos);
            entityManager.AddComponentData(entity, new CRequiresView
            {
                Type = view,
                ViewMode = ViewMode.Screen
            });
            return entity;
        }
    }
}

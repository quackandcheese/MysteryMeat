using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Components;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace KitchenMysteryMeat.Views
{
    public class WantedDisplayView : UpdatableObjectView<WantedDisplayView.ViewData>
    {
        [MessagePackObject]
        public struct ViewData : IViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)]
            public int Level;

            //public IUpdatableObject GetRelevantSubview(IObjectView view)
            //{
            //    return view.GetSubView<TeamMoneyDisplayView>();
            //}

            public bool IsChangedFrom(ViewData check)
            {
                return Level != check.Level;
            }
        }

        public GameObject IconParent;

        public ViewData Data;

        public override void Initialise()
        {
            base.Initialise();
        }

        protected override void UpdateData(ViewData data)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Find("Icon").gameObject.SetActive(i < data.Level);
            }

            Data = data;
        }
    }

    public class UpdateTeamMoneyView : IncrementalViewSystemBase<WantedDisplayView.ViewData>, IModSystem
    {
        private EntityQuery Views;

        protected override void Initialise()
        {
            base.Initialise();
            Views = GetEntityQuery(new QueryHelper()
                .All(typeof(CLinkedView), typeof(CWantedDisplay))
            );
            RequireSingletonForUpdate<SKitchenStatus>();
        }

        protected override void OnUpdate()
        {
            using var views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
            using var components = Views.ToComponentDataArray<CWantedDisplay>(Allocator.Temp);

            for (int i = 0; i < views.Length; i++)
            {
                var view = views[i];
                var data = components[i];

                SendUpdate(view, new WantedDisplayView.ViewData
                {
                    Level = data.Level,
                });
            }
        }
    }
}

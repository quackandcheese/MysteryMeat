using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Enums;
using KitchenMysteryMeat.Components;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Kitchen.Components;


namespace KitchenMysteryMeat.Views
{
    public class MeatGrinderView : UpdatableObjectView<MeatGrinderView.ViewData>
    {
        public GameObject HoldPoint;

        private void Awake()
        {
            HoldPoint = transform.Find("GameObject").gameObject;
        }

        protected override void UpdateData(MeatGrinderView.ViewData data)
        {
            if (data.ConveyProgress > 0.0f)
            {
                HoldPoint.transform.localPosition = new Vector3(0, 0.666f, -0.128f);
            }
            else
            {
                HoldPoint.transform.localPosition = new Vector3(0, 1.147f, -0.032f);
            }
        }

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CMeatGrinder), typeof(CConveyPushItems)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var meatGrinders = query.ToComponentDataArray<CMeatGrinder>(Allocator.Temp);
                using var conveyPushItemsComponents = query.ToComponentDataArray<CConveyPushItems>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var meatGrinder = meatGrinders[i];
                    var conveyPushItems = conveyPushItemsComponents[i];

                    SendUpdate(view, new ViewData
                    {
                        ConveyProgress = conveyPushItems.Progress,
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public float ConveyProgress;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<MeatGrinderView>();

            public bool IsChangedFrom(ViewData check) => check.ConveyProgress != ConveyProgress;
        }
    }
}

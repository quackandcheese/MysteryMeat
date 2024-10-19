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
            if (data.HasGrindableItem)
            {
                HoldPoint.transform.localPosition = data.GrinderInputPosition;
            }
            else
            {
                HoldPoint.transform.localPosition = data.GrinderOutputPosition;
            }
            float inverseProgress = 1 - data.ProcessProgress;
            HoldPoint.transform.localScale = new Vector3(inverseProgress, inverseProgress, inverseProgress);
        }

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CMeatGrinder), typeof(CApplyingProcess), typeof(CItemHolder)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var meatGrinders = query.ToComponentDataArray<CMeatGrinder>(Allocator.Temp);
                using var applyingProcessComponents = query.ToComponentDataArray<CApplyingProcess>(Allocator.Temp);
                using var itemHolders = query.ToComponentDataArray<CItemHolder>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var meatGrinder = meatGrinders[i];
                    var applyingProcess = applyingProcessComponents[i];
                    var hasGrindable = Has<CGrindable>(itemHolders[i].HeldItem);

                    SendUpdate(view, new ViewData
                    {
                        HasGrindableItem = hasGrindable,
                        ProcessProgress = applyingProcess.Progress,
                        GrinderInputPosition = meatGrinder.GrinderInputPosition,
                        GrinderOutputPosition = meatGrinder.GrinderOutputPosition,
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public bool HasGrindableItem;
            [Key(1)] public float ProcessProgress;
            [Key(2)] public Vector3 GrinderInputPosition;
            [Key(3)] public Vector3 GrinderOutputPosition;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<MeatGrinderView>();

            public bool IsChangedFrom(ViewData check) => check.HasGrindableItem != HasGrindableItem || check.ProcessProgress != ProcessProgress;
        }
    }
}

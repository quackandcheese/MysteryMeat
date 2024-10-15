using Kitchen;
using KitchenMods;
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

namespace KitchenMysteryMeat.Views
{
    public class LimitedUseBottleView : UpdatableObjectView<LimitedUseBottleView.ViewData>
    {
        public GameObject Mesh;
        public Material BottleMaterial;
        public Material LiquidMaterial;

        private void Awake()
        {
            Mesh = transform.Find("LimitedUseBottle").gameObject;
        }

        protected override void UpdateData(LimitedUseBottleView.ViewData data)
        {
            if (!Mesh || !BottleMaterial || !LiquidMaterial || data.Equals(default(ViewData)))
                return;

            MeshRenderer renderer = Mesh.GetComponent<MeshRenderer>();
            Material[] newMats = new Material[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                Material desiredMaterial = BottleMaterial;
                if (i < data.FillAmount)
                {
                    desiredMaterial = LiquidMaterial;
                }
                newMats[i] = desiredMaterial;
            }
            Mesh.GetComponent<MeshRenderer>().materials = newMats;
        }

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CLimitedUseBottle)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var limitedUseBottleComponents = query.ToComponentDataArray<CLimitedUseBottle>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var limitedUseBottle = limitedUseBottleComponents[i];

                    SendUpdate(view, new ViewData
                    {
                        Limit = limitedUseBottle.Limit,
                        FillAmount = limitedUseBottle.FillAmount
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int Limit;
            [Key(1)] public int FillAmount;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<LimitedUseBottleView>();

            public bool IsChangedFrom(ViewData check) => check.Limit != Limit || check.FillAmount != FillAmount;
        }
    }
}

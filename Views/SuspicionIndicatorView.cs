﻿using Kitchen;
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

namespace KitchenMysteryMeat.Views
{
    public class SuspicionIndicatorView : UpdatableObjectView<SuspicionIndicatorView.ViewData>
    {
        public GameObject Canvas;
        public Image SuspicionIconFill;

        public GameObject SuspicionIconParent;
        public GameObject AlertIconParent;

        private void Awake()
        {
            Canvas = transform.Find("Canvas").gameObject;
            SuspicionIconParent = Canvas.transform.Find("Suspicion").gameObject;
            AlertIconParent = Canvas.transform.Find("Alert").gameObject;
            SuspicionIconFill = SuspicionIconParent.transform.Find("Icon").GetComponent<Image>();
        }

        protected override void UpdateData(SuspicionIndicatorView.ViewData data)
        {
            if (Canvas == null || SuspicionIconFill == null)
                return;
            
            bool shouldShowIndicator = data.RemainingTime < data.TotalTime || data.IndicatorType == SuspicionIndicatorType.Alert;
            Canvas.SetActive(shouldShowIndicator);

            if (data.IndicatorType == SuspicionIndicatorType.Alert) 
            {
                // Show Alert Indicator
                AlertIconParent.SetActive(true);
                SuspicionIconParent.SetActive(false);
            }
            else if (data.IndicatorType == SuspicionIndicatorType.Suspicious) 
            {
                // Show Sus Indicator
                SuspicionIconParent.SetActive(true);
                AlertIconParent.SetActive(false);

                if (data.RemainingTime > 0.0f)
                {
                    // Fill amount starts from 0, then goes up
                    SuspicionIconFill.fillAmount = 1 - (data.RemainingTime / data.TotalTime);
                }
            }            
        }

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CSuspicionIndicator)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var suspicionIndicators = query.ToComponentDataArray<CSuspicionIndicator>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var suspicionIndicator = suspicionIndicators[i];

                    SendUpdate(view, new ViewData
                    {
                        IndicatorType = suspicionIndicator.IndicatorType,
                        TotalTime = suspicionIndicator.TotalTime,
                        RemainingTime = suspicionIndicator.RemainingTime,
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public SuspicionIndicatorType IndicatorType;
            [Key(1)] public float TotalTime;
            [Key(2)] public float RemainingTime;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<SuspicionIndicatorView>();

            public bool IsChangedFrom(ViewData check) => check.IndicatorType != IndicatorType || check.RemainingTime != RemainingTime || check.TotalTime != TotalTime;
        }
    }
}
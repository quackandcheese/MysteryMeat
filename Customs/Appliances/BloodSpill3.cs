using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Customs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Appliances
{
    public class BloodSpill3 : CustomAppliance
    {
        public override string UniqueNameID => "BloodSpill3";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Mess - Blood Spill 3").AssignMaterialsByNames();
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CSlowPlayer()
            {
                Radius = 0.3f,
                Factor = 0.9f
            },
            new CTakesDuration()
            {
                Total = 6,
                Manual = true,
                ManualNeedsEmptyHands = false,
                RelevantTool = DurationToolType.Clean,
                Mode = InteractionMode.Items
            },
            new CDestroyAfterDuration(),
            new CDestroyApplianceAtNight(),
            new CDisplayDuration()
            {
                IsBad = false,
                Process = ProcessReferences.Clean,
                ShowWhenEmpty = false
            }
        };
    }
}

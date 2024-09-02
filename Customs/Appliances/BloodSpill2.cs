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
    public class BloodSpill2 : CustomAppliance
    {
        public override string UniqueNameID => "BloodSpill2";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Mess - Blood Spill 2").AssignMaterialsByNames();
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CSlowPlayer()
            {
                Radius = 0.25f,
                Factor = 1.0f
            },
            new CTakesDuration()
            {
                Total = 3,
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
            },
            new CStackableMess()
            {
                BaseMess = GDOUtils.GetCustomGameDataObject<BloodSpill1>().ID,
                NextMess = GDOUtils.GetCustomGameDataObject<BloodSpill3>().ID
            }
        };
    }
}

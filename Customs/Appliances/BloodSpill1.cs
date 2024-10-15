using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMysteryMeat.Components;
using KitchenMysteryMeat.Customs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenMysteryMeat.Customs.Appliances
{
    public class BloodSpill1 : CustomAppliance
    {
        public override string UniqueNameID => "BloodSpill1";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Mess - Blood Spill 1").AssignMaterialsByNames();
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CSlowPlayer()
            {
                Radius = 0.2f,
                Factor = 1.1f
            },
            new CTakesDuration()
            {
                Total = 1,
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
                BaseMess = ID,
                NextMess = GDOUtils.GetCustomGameDataObject<BloodSpill2>().ID
            },
            new CIllegalSight(),
            new CFillsBottle()
            {
                BottleID = GDOUtils.GetCustomGameDataObject<SpecialSauceBottle>().ID
            }
        };
    }
}

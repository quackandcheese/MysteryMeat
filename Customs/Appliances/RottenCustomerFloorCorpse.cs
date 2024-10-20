using Kitchen;
using KitchenData;
using KitchenLib.Customs;
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
    public class RottenCustomerFloorCorpse : CustomAppliance
    {
        public override string UniqueNameID => "RottenCustomerFloorCorpse";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Rotten Customer Floor Corpse").AssignMaterialsByNames().AssignVFXByNames();
        public override OccupancyLayer Layer => OccupancyLayer.Floor;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            //new CDestroyApplianceAtNight(),
            KitchenPropertiesUtils.GetCItemProvider(GDOUtils.GetCustomGameDataObject<RottenCustomerCorpse>().ID, 1, 1, false, false, false, true, false, false, false),
            new CIllegalSight()
            {
            },
            new CImmovable(),
/*
            new CTakesDuration()
            {
                Total = 0.5f,
                Active = false,
                Manual = false,
                
            },
            new CCausesSpills()
            {
                ID = GDOUtils.GetCustomGameDataObject<BloodSpill1>().ID,
                Rate = 2f,
                OverwriteOtherMesses = false
            }*/
        };
    }
}

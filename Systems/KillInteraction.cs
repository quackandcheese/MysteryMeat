using Kitchen;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMods;
using KitchenMysteryMeat.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    [UpdateInGroup(typeof(LowPriorityInteractionGroup), OrderFirst = true)]
    public class KillInteraction : ItemInteractionSystem, IModSystem
    {
        protected override bool RequireHold => true;

        protected override bool RequirePress => false;

        protected override bool IsPossible(ref InteractionData data)
        {
            CToolUser ctoolUser;

            return Has<CCustomer>(data.Target) && Require<CToolUser>(data.Interactor, out ctoolUser) && Has<CKillsCustomer>(ctoolUser.CurrentTool) && !Has<CKilled>(data.Target);
        }

        protected override void Perform(ref InteractionData data)
        {
            EntityManager.AddComponent<CKilled>(data.Target);
        }
    }
}

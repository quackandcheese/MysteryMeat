using Kitchen;
using KitchenMods;
using KitchenMysteryMeat.Components;
using System;
using Unity.Entities;

namespace KitchenMysteryMeat.Systems
{
    public class PoisonInteraction : ItemInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            CItemHolder playerHeldItem;
            CItemHolder applianceHeldItem;

            // Check if the appliance holds an item and the player holds a poison bottle, or vice versa
            return (Require<CItemHolder>(data.Target, out applianceHeldItem)
                    && Has<CItem>(applianceHeldItem.HeldItem)
                    && !Has<CPoisoned>(applianceHeldItem.HeldItem)
                    && Require<CItemHolder>(data.Interactor, out playerHeldItem)
                    && Has<CPoisonBottle>(playerHeldItem.HeldItem))
                   ||
                   (Require<CItemHolder>(data.Interactor, out playerHeldItem)
                    && Has<CItem>(playerHeldItem.HeldItem)
                    && !Has<CPoisoned>(playerHeldItem.HeldItem)
                    && Require<CItemHolder>(data.Target, out applianceHeldItem)
                    && Has<CPoisonBottle>(applianceHeldItem.HeldItem));
        }

        protected override void Perform(ref InteractionData data)
        {
            CItemHolder playerHeldItem;
            CItemHolder applianceHeldItem;

            // Apply poison to the appliance-held item if the player holds the poison bottle
            if (Require<CItemHolder>(data.Target, out applianceHeldItem)
                && Require<CItemHolder>(data.Interactor, out playerHeldItem)
                && Has<CPoisonBottle>(playerHeldItem.HeldItem))
            {
                EntityManager.AddComponent<CPoisoned>(applianceHeldItem.HeldItem);
            }
            // Apply poison to the player-held item if the appliance holds the poison bottle
            else if (Require<CItemHolder>(data.Interactor, out playerHeldItem)
                     && Require<CItemHolder>(data.Target, out applianceHeldItem)
                     && Has<CPoisonBottle>(applianceHeldItem.HeldItem))
            {
                EntityManager.AddComponent<CPoisoned>(playerHeldItem.HeldItem);
            }
        }
    }
}

﻿using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System.Linq;
using System;
using UnityEngine;


namespace ShouMod.StatusEffects
{
    public sealed class ShouWondersofNatureSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.IsStackable = false;
            config.HasLevel = false;
            return config;
        }

    }

    [EntityLogic(typeof(ShouWondersofNatureSeDef))]
    public sealed class ShouWondersofNatureSe : StatusEffect
    {


        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new EventSequencedReactor<CardsAddingToDrawZoneEventArgs>(this.OnCardsAddedToDrawZone));
        }

        private IEnumerable<BattleAction> OnAddCard(CardsEventArgs args)
        {
            //At the start of the Player's turn, gain Spirit.
            foreach (Card card in args.Cards) {
                if (card is ShouGemstoneCard)
                {
                    if (card.CanUpgradeAndPositive)
                    {
                        yield return new UpgradeCardAction(card);
                    }
                }
            }
            yield break;
        }

        private IEnumerable<BattleAction> OnCardsAddedToDrawZone(CardsAddingToDrawZoneEventArgs args)
        {
            foreach (Card card in args.Cards)
            {
                if (card is ShouGemstoneCard)
                {
                    if (card.CanUpgradeAndPositive)
                    {
                        yield return new UpgradeCardAction(card);
                    }
                }
            }
            yield break;
        }
    }
}
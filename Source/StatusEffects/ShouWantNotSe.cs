using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Base.Extensions;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System;
using System.Linq;
using LBoL.Core.Battle.Interactions;

namespace ShouMod.StatusEffects
{
    public sealed class ShouWantNotSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(ShouWantNotSeDef))]
    public sealed class ShouWantNotSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnding));

        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnOwnerTurnEnding(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            List<Card> list = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).ToList<Card>();
            SelectCardInteraction interaction = new SelectCardInteraction(0, 3, list, SelectedCardHandling.DoNothing)
            {
                Source = this
            };
            yield return new InteractionAction(interaction, true);
            IReadOnlyList<Card> selectedCards = ((SelectCardInteraction)interaction).SelectedCards;
            if (selectedCards != null)
            {
                foreach(Card card in selectedCards)
                {
                    yield return new CastBlockShieldAction(base.Battle.Player, 0, base.Level, BlockShieldType.Direct, false);
                    yield return new ExileCardAction(card);
                }
            }
            yield break;
        }
    }
}
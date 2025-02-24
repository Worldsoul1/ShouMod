using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System;

namespace ShouMod.StatusEffects
{
    public sealed class ShouPrimalPowerSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.RelativeEffects = new List<string>() { nameof(ShouResonanceSe) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouPrimalPowerSeDef))]
    public sealed class ShouPrimalPowerSe : StatusEffect
    {
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Single(ManaColor.Philosophy);
            }
        }
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnding));
            
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.CardType is CardType.Attack)
            {
                yield return new GainManaAction(Mana);
                yield return new DrawCardAction();
                yield return new ApplyStatusEffectAction<ShouResonanceSe>(base.Battle.Player, 3, 0, 0, 0, 0.2f);
                Level--;
                if(base.Level == 0)
                {
                    yield return new RemoveStatusEffectAction(this, true);
                }
            }
            yield break;
        }
        private IEnumerable<BattleAction> OnOwnerTurnEnding(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this, true);
        }
    }
}

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
    public sealed class ShouCrimsonTearsSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(ShouCrimsonTearsSeDef))]
    public sealed class ShouCrimsonTearsSe : StatusEffect
    {
        int count = 0;
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Owner.DamageReceived, new EventSequencedReactor<DamageEventArgs>(this.OnDamageReceived));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnDamageReceived(DamageEventArgs args)
        {
            if (args.Source != base.Owner && args.Source.IsAlive && args.DamageInfo.DamageType == DamageType.Attack && args.DamageInfo.Amount > 0f)
            {
                base.NotifyActivating();
                if (count < base.Level)
                {
                    yield return new AddCardsToDiscardAction(Library.CreateCards<ShouRuby>(1, false), AddCardsType.Normal);
                    count = count + 1;
                }
            }
            yield break;
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            count = 0;
            yield break;
        }
    }
}
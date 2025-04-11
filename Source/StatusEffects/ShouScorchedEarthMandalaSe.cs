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
using LBoL.Base.Extensions;

namespace ShouMod.StatusEffects
{
    public sealed class ShouScorchedEarthMandalaSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(ShouScorchedEarthMandalaSeDef))]
    public sealed class ShouScorchedEarthMandalaSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.CardType == CardType.Attack)
            {
                yield return new ApplyStatusEffectAction<ShouResonanceSe>(base.Battle.Player, base.Level, 0, 0, 0, 0.2f);
            }
            else if((args.Card.CardType == CardType.Defense) && base.Battle.Player.HasStatusEffect<ShouVigorSe>())
            {
                ShouVigorSe Vigor = base.Battle.Player.GetStatusEffect<ShouVigorSe>();
                yield return new RemoveStatusEffectAction(Vigor);
            }
            yield break;
        }
    }
}

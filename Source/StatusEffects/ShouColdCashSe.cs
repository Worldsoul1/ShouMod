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
using LBoL.EntityLib.StatusEffects.Cirno;

namespace ShouMod.StatusEffects
{
    public sealed class ShouColdCashSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.RelativeEffects = new List<string>() { nameof(Cold) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouColdCashSeDef))]
    public sealed class ShouColdCashSe : StatusEffect
    {
        public List<Type> Gemstones = new List<Type>
        {
            typeof(ShouAmber),
            typeof(ShouDiamond),
            typeof(ShouEmerald),
            typeof(ShouOnyx),
            typeof(ShouOpal),
            typeof(ShouPearl),
            typeof(ShouRuby),
            typeof(ShouSapphire),
        };
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnding));
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card is ShouGemstoneCard)
            {
                yield return new ApplyStatusEffectAction<Cold>(base.Battle.RandomAliveEnemy, 0, 0, 0, 0, 0.2f);
            }
            yield break;
        }
        private IEnumerable<BattleAction> OnOwnerTurnEnding(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this);
        }
    }
}

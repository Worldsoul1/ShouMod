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
    public sealed class ShouCycleofWealthSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }
    }

    [EntityLogic(typeof(ShouCycleofWealthSeDef))]
    public sealed class ShouCycleofWealthSe : StatusEffect
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
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card is ShouGemstoneCard)
            {
                this.Gemstones.Shuffle(base.GameRun.BattleCardRng);
                yield return new ExileCardAction(args.Card);
                yield return new AddCardsToDiscardAction(Library.CreateCard(this.Gemstones[0]));
            }
            yield break;
        }
    }
}

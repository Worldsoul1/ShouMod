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
    public sealed class ShouRefractingLightSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.RelativeEffects = new List<string>() { nameof(TempFirepower), nameof(TempSpirit) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouRefractingLightSeDef))]
    public sealed class ShouRefractingLightSe : StatusEffect
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
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardPlayed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardPlayed));
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x000036D2 File Offset: 0x000018D2
        private IEnumerable<BattleAction> OnCardPlayed(CardUsingEventArgs args)
        {
            if (args.Card is ShouGemstoneCard)
            {
                yield return new ApplyStatusEffectAction<TempFirepower>(base.Battle.Player, base.Level, 0, 0, 0, 0.2f);
                yield return new ApplyStatusEffectAction<TempSpirit>(base.Battle.Player, base.Level, 0, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}
using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.Base.Extensions;
using System;
using ShouMod.Cards;

namespace ShouMod.StatusEffects
{
    public sealed class ShouInstinctiveCharitySeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(ShouInstinctiveCharitySeDef))]
    public sealed class ShouInstinctiveCharitySe : StatusEffect
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
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            List<Card> list = new List<Card>();
            this.Gemstones.Shuffle(base.GameRun.BattleCardRng);
            for (int i = 0; i < base.Level; i++)
            {
                list.Add(Library.CreateCard(this.Gemstones[i]));
            }
            yield return new AddCardsToDrawZoneAction(list, DrawZoneTarget.Random, AddCardsType.Normal);

            yield break;
        }
    }
}
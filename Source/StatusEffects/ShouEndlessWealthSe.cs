using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System;

namespace ShouMod.StatusEffects
{
    public sealed class ShouEndlessWealthSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.IsStackable = false;
            config.LevelStackType = StackType.Keep;
            config.HasCount = true;
            return config;
        }

    }

    [EntityLogic(typeof(ShouEndlessWealthSeDef))]
    public sealed class ShouEndlessWealthSe : StatusEffect
    {
        public string NextGem
        {
            get
            {
                string[] l = base.Brief.Split(" ");
                return l[(Count + 1) % 6];
            }
        }
        public List<Type> Gemstones = new List<Type>
        {
            typeof(ShouRuby),
            typeof(ShouOnyx),
            typeof(ShouSapphire),
            typeof(ShouPearl),
            typeof(ShouEmerald),
            typeof(ShouOpal),
        };


        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            Count = (Count + 1) % 6;
            yield return new AddCardsToHandAction(new LBoL.Core.Cards.Card[] { Library.CreateCard(this.Gemstones[Count]) });
            //At the start of the Player's turn, gain Spirit.
            
            yield break;
        }
    }
}
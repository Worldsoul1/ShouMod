﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using ShouMod.StatusEffects;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;

namespace ShouMod.Cards
{
    public sealed class ShouProtectiveMoneyDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 2, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Shield = 15;
            config.UpgradedShield = 20;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;
            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.
            config.RelativeKeyword = Keyword.Shield;
            config.UpgradedRelativeKeyword = Keyword.Shield;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouProtectiveMoneyDef))]
    public sealed class ShouProtectiveMoney : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int ExtraMoney = 0;
            yield return DefenseAction(true);
            ExtraMoney = base.Value1 * base.Battle.HandZone.Count;
            yield return new GainMoneyAction(ExtraMoney, SpecialSourceType.None);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
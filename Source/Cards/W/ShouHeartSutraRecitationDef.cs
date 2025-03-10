﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using ShouMod.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouHeartSutraRecitationDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 4;
            config.UpgradedValue1 = 6;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouHeartSutraRecitationSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouHeartSutraRecitationSe) };

            config.Illustrator = "黄瓜";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouHeartSutraRecitationDef))]
    public sealed class ShouHeartSutraRecitation : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouHeartSutraRecitationSe>(base.Value1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
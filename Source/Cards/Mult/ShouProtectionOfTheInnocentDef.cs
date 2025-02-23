using LBoL.Base;
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
    public sealed class ShouProtectionOfTheInnocentDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Shield = 20;
            config.UpgradedShield = 23;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.
            config.RelativeKeyword = Keyword.Shield;
            config.UpgradedRelativeKeyword = Keyword.Shield;

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouProtectionOfTheInnocentDef))]
    public sealed class ShouProtectionOfTheInnocent : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return DefenseAction(true);
            yield return new ApplyStatusEffectAction<ShouVigorSe>(base.Battle.Player, 0, 2, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
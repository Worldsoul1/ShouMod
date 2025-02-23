using LBoL.Base;
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
    public sealed class ShouLimitlessBenevolenceDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;
            config.Value2 = 2;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouLimitlessBenevolenceSe), nameof(ShouVigorSe), nameof(LockedOn) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouLimitlessBenevolenceSe), nameof(ShouVigorSe), nameof(LockedOn) };

            config.Illustrator = "メイマ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouLimitlessBenevolenceDef))]
    public sealed class ShouLimitlessBenevolence : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouLimitlessBenevolenceSe>(base.Value1, 0, 0, 0, 0.2f);
            yield return BuffAction<ShouVigorSe>(0, base.Value2, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
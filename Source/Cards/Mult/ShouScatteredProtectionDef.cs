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
    public sealed class ShouScatteredProtectionDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;
            config.Value2 = 2;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouScatteredProtectionSe), nameof(ShouHardenSe), nameof(TempFirepowerNegative) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouScatteredProtectionSe), nameof(ShouHardenSe), nameof(TempFirepowerNegative) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouScatteredProtectionDef))]
    public sealed class ShouScatteredProtection : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouScatteredProtectionSe>(base.Value1, 0, 0, 0, 0.2f);
            yield return BuffAction<ShouHardenSe>(0, base.Value2, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
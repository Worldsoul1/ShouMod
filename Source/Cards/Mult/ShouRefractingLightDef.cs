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
    public sealed class ShouRefractingLightDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;
            config.Value2 = 1;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouRefractingLightSe), nameof(TempFirepower), nameof(TempSpirit) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouRefractingLightSe), nameof(TempFirepower), nameof(TempSpirit) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouRefractingLightDef))]
    public sealed class ShouRefractingLight : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouRefractingLightSe>(base.Value1, 0, 0, 0, 0.2f);
            if(this.IsUpgraded)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<ShouPearl>(base.Value2, false), AddCardsType.Normal);
            }
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
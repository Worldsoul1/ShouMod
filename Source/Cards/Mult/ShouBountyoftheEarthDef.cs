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

namespace ShouMod.Cards
{
    public sealed class ShouBountyoftheEarthDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeCards = new List<string> { nameof(ShouEmerald) };
            config.UpgradedRelativeCards = new List<string> { nameof(ShouEmerald) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouBountyoftheEarthDef))]
    public sealed class ShouBountyoftheEarth : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            if (base.IsUpgraded)
            {
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouEmerald>(base.Value1, true), AddCardsType.Normal);
            }
            else
            {
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouEmerald>(base.Value1, false), AddCardsType.Normal);
            }
            yield break;
        }
    }
}


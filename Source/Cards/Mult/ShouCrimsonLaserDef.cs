using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.Cards;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;

namespace ShouMod.Cards
{
    public sealed class ShouCrimsonLaserDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            //Hybrid colors:
            //0 = W/U
            //1 = W/B
            //2 = W/R
            //3 = W/G
            //4 = U/B
            //5 = U/R
            //6 = U/G
            //7 = B/R
            //8 = B/G
            //9 = R/G
            //As of 1.5.1: Colorless hybrid are not supported.    
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 12;
            config.UpgradedDamage = 12;

            config.RelativeCards = new List<string> { nameof(ShouRuby), nameof(ShouPearl) };
            config.UpgradedRelativeCards = new List<string> { nameof(ShouRuby), nameof(ShouPearl) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouCrimsonLaserDef))]
    public sealed class ShouCrimsonLaser : SampleCharacterCard
    {
        public override Interaction Precondition()
        {
            List<Card> cards = new List<Card>
            {
                Library.CreateCard<ShouRuby>(),
                Library.CreateCard<ShouPearl>()
            };
            return new SelectCardInteraction(1, 1, cards, SelectedCardHandling.DoNothing);
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            if (precondition == null)
            {
                yield break;
            }
            SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
            List<Card> cards = new List<Card>
            {
                selectCardInteraction.SelectedCards[0],
            };
            yield return new AddCardsToDiscardAction(cards, AddCardsType.Normal);
            yield break;
        }
    }
}
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
    public sealed class ShouGemWallDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
        
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 12;
            config.UpgradedBlock = 16;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.
            config.RelativeKeyword = Keyword.Block;
            config.UpgradedRelativeKeyword = Keyword.Block;

            config.RelativeCards = new List<string> { nameof(ShouRuby), nameof(ShouOnyx) };
            config.UpgradedRelativeCards = new List<string> { nameof(ShouRuby), nameof(ShouOnyx) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(ShouGemWallDef))]
    public sealed class ShouGemWall : SampleCharacterCard
    {
        public override Interaction Precondition()
        {
            List<Card> cards = new List<Card>
            {
                Library.CreateCard<ShouRuby>(),
                Library.CreateCard<ShouOnyx>()
            };
            return new SelectCardInteraction(1, 1, cards, SelectedCardHandling.DoNothing);
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return DefenseAction(true);

            if (base.IsUpgraded)
            {
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouOpal>(base.Value1, false), AddCardsType.Normal);
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouOnyx>(base.Value1, false), AddCardsType.Normal); 
            }
            else
            {
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
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}



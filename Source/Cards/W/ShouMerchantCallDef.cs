using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using ShouMod.StatusEffects;
using LBoL.Core.Cards;
using LBoL.Core.Battle.Interactions;
using System.Linq;
using LBoL.Core.Randoms;

namespace ShouMod.Cards
{
    public sealed class ShouMerchantCallDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.Value2 = 3;
            config.UpgradedValue2 = 3;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.Illustrator = "Jintouhou";


            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouMerchantCallDef))]
    public sealed class ShouMerchantCall : ShouGemstoneReferenceCard
    {
        public override Interaction Precondition()
        {

            List<Card> list = (from card in base.Battle.HandZone
                               where card != this
                               select card).ToList<Card>();
            if (!list.Empty<Card>())
            {
                return new SelectHandInteraction(0, base.Value1, list);
            }
            return null;

        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition != null)
            {
                //Add a token card to the hand.
                SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
                Card card = (selectCardInteraction != null) ? selectCardInteraction.SelectedCards.FirstOrDefault<Card>() : null;
                if (card != null)
                {

                    if (card is ShouGemstoneCard)
                    {
                        Card[] array = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.NoneRare, OwnerWeightTable.Valid, CardTypeWeightTable.OnlyTool, false), base.Value2, null);
                        if (array.Length != 0)
                        {
                            foreach (Card toolcard in array)
                            {
                                toolcard.DeckCounter = new int?(1);
                            }
                            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(array, false, false, false)
                            {
                                Source = this
                            };
                            yield return new InteractionAction(interaction, false);
                            yield return new AddCardsToHandAction(new Card[]
                                  {
                    interaction.SelectedCard
                                  });
                            interaction = null;
                        }
                    }
                }
                yield return new ExileCardAction(card);
                yield break;
            }
        }
    }
}
    
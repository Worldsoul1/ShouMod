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

namespace ShouMod.Cards
{
    public sealed class ShouOneMansTrashDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1,  White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 3;
            config.UpgradedValue1 = 3;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.Illustrator = "核能松饼";


            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouOneMansTrashDef))]
    public sealed class ShouOneMansTrash : ShouGemstoneReferenceCard
    {
        public override Interaction Precondition()
        {
            if (this.IsUpgraded)
            {
                List<Card> list = (from card in base.Battle.HandZone.Concat(base.Battle.DrawZoneToShow).Concat(base.Battle.DiscardZone)
                                   where card != this
                                   select card).ToList<Card>();
                if (!list.Empty<Card>())
                {
                    return new SelectCardInteraction(0, base.Value1, list, SelectedCardHandling.DoNothing);
                }
                return null;
            }
            else
            {
                List<Card> list2 = (from card in base.Battle.HandZone
                                    where card != this
                                    select card).ToList<Card>();
                if (!list2.Empty<Card>())
                {
                    return new SelectHandInteraction(0, base.Value1, list2);
                }
                return null;
            }
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            if (precondition != null)
            {
                List<Card> listGems = new List<Card>();
                IReadOnlyList<Card> readOnlyList = this.IsUpgraded ? ((SelectCardInteraction)precondition).SelectedCards : ((SelectHandInteraction)precondition).SelectedCards;
                if (readOnlyList.Count > 0)
                {
                    foreach (Card card in readOnlyList) 
                    { 
                        if (card.CardType == CardType.Attack) { listGems.Add(Library.CreateCard<ShouRuby>()); }
                        if (card.CardType == CardType.Defense) { listGems.Add(Library.CreateCard<ShouOnyx>()); }
                        if (card.CardType == CardType.Skill) { listGems.Add(Library.CreateCard<ShouOpal>()); }
                        if (card.CardType == CardType.Ability) { listGems.Add(Library.CreateCard<ShouSapphire>()); }
                        if (card.CardType == CardType.Friend) { listGems.Add(Library.CreateCard<ShouPearl>()); }
                        if (card.CardType == CardType.Status) { listGems.Add(Library.CreateCard<ShouEmerald>()); }
                        if (card.CardType == CardType.Tool) { listGems.Add(Library.CreateCard<ShouAmber>()); }
                        if (card.CardType == CardType.Misfortune) { listGems.Add(Library.CreateCard<ShouDiamond>()); }
                        yield return new ExileCardAction(card);
                    }
                    yield return new AddCardsToHandAction(listGems, AddCardsType.Normal);
                }
                yield break;
            }
            yield break;
        }
    }
}
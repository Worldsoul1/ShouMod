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
using System.Linq;
using LBoL.Core.Battle.Interactions;

namespace ShouMod.Cards
{
    public sealed class ShouGlintDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 2 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.Value1 = 8;
            config.UpgradedValue1 = 8;

            config.Value2 = 4;
            config.UpgradedValue2 = 6;

            config.Illustrator = "Radal";




            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouGlintDef))]
    public sealed class ShouGlint : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            List<Card> list = new List<Card>();
            for (int i = 0; i < base.Value1; i++)
            {
                list.Add(Library.CreateCard(this.Gemstones[i]));
            }
            yield return new AddCardsToDrawZoneAction(list, DrawZoneTarget.Random, AddCardsType.Normal);

            List<Card> pullGemstone = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DrawZoneToShow.Where(c => c is ShouGemstoneCard)).ToList<Card>();
            if (!pullGemstone.Empty<Card>())
            {
                SelectCardInteraction interaction = new SelectCardInteraction(0, base.Value2, pullGemstone);
                yield return new InteractionAction(interaction, false);
                List<Card> selected = interaction.SelectedCards.ToList<Card>();
                if (selected.Count > 0)
                {
                    foreach (Card card in selected)
                    {
                        yield return new MoveCardAction(card, CardZone.Hand);
                    }
                }
                yield break;
            }
        }
    }
}

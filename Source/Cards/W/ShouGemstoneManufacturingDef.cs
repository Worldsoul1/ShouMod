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

namespace ShouMod.Cards
{
    public sealed class ShouGemstoneManufacturingDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.Value2 = 3;
            config.UpgradedValue2 = 5;

            config.Illustrator = "";


            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouGemstoneManufacturingDef))]
    public sealed class ShouGemstoneManufacturing : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            List<Card> list = new List<Card>();
            this.Gemstones.Shuffle(base.GameRun.BattleCardRng);
            for (int i = 0; i < base.Value2; i++)
            {
                list.Add(Library.CreateCard(this.Gemstones[i]));
            }
            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false)
            {
                Source = this
            };
            yield return new InteractionAction(interaction, false);
            Card selectedCard = interaction.SelectedCard;
            if (selectedCard != null)
            {
               yield return new AddCardsToHandAction(new Card[] { selectedCard });
            }
            yield break;
        }
    }
}

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
    public sealed class ShouScatteredTreasureDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Value2 = 3;
            config.UpgradedValue2 = 5;

            config.RelativeCards = new List<string>() { nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };
            config.UpgradedRelativeCards = new List<string>() { nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };


            config.Illustrator = "Radal";

           
            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(ShouScatteredTreasureDef))]
    public sealed class ShouScatteredTreasure : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            List<Card> list = new List<Card>();
            this.CommonGemstones.Shuffle(base.GameRun.BattleCardRng);
            for (int i = 0; i < base.Value2; i++)
            {
                list.Add(Library.CreateCard(this.CommonGemstones[i]));
            }
            SelectCardInteraction interaction = new SelectCardInteraction(0, base.Value1, list, SelectedCardHandling.DoNothing)
            {
                Source = this
            };
            yield return new InteractionAction(interaction, false);
            IReadOnlyList<Card> selectedCards = interaction.SelectedCards;
            foreach (Card card in selectedCards) 
            {
                yield return new AddCardsToHandAction(new Card[] { card });
            }
            yield return new AddCardsToDrawZoneAction(list.Except(selectedCards), DrawZoneTarget.Random, AddCardsType.Normal);
            yield break;
        }
    }
}



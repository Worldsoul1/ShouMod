using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using ShouMod.StatusEffects;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Cirno;
using System.Linq;
using LBoL.Base.Extensions;

namespace ShouMod.Cards
{
    public sealed class ShouDiamondDustDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 2, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 15;
            config.UpgradedBlock = 20;

            config.Value1 = 6;
            config.UpgradedValue1 = 8;

            config.Value2 = 2;
            config.UpgradedValue2 = 3;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;
            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.
            config.RelativeKeyword = Keyword.Block;
            config.UpgradedRelativeKeyword = Keyword.Block;

            config.RelativeEffects = new List<string>() { nameof(FrostArmor) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(FrostArmor) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouDiamondDustDef))]
    public sealed class ShouDiamondDust : ShouGemstoneReferenceCard
    {

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int FrostCount = 0;
            List<Card> pullGemstone = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DrawZoneToShow.Where(c => c is ShouGemstoneCard)).ToList<Card>();
            if (!pullGemstone.Empty<Card>())
            {
                SelectCardInteraction interaction = new SelectCardInteraction(0, base.Value1, pullGemstone);
                yield return new InteractionAction(interaction, false);
                List<Card> selected = interaction.SelectedCards.ToList<Card>();
                if (selected.Count > 0)
                {
                    foreach (Card card in selected)
                    {
                        FrostCount = FrostCount + base.Value1;
                        yield return new ExileCardAction(card);
                    }
                }
                yield return new ApplyStatusEffectAction<FrostArmor>(base.Battle.Player, FrostCount, 0, 0, 0, 0.2f);
            }
            yield return DefenseAction(true);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
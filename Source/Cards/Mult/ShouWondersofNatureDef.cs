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
using System.Linq;

namespace ShouMod.Cards
{
    public sealed class ShouWondersofNatureDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Green };
            config.Cost = new ManaGroup() { Any = 1, White = 1, Green = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 3};
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouWondersofNatureSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouWondersofNatureSe) };

            config.Illustrator = "茶餅";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouWondersofNatureDef))]
    public sealed class ShouWondersofNature : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            List<Card> Gemstones = base.Battle.HandZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DrawZoneToShow.Where(c => c is ShouGemstoneCard))).ToList<Card>();
            yield return BuffAction<ShouWondersofNatureSe>(base.Value1, 0, 0, 0, 0.2f);
            List<Card> cards = new List<Card>();
            cards.Add(Library.CreateCard<ShouAmber>(false));
            cards.Add(Library.CreateCard<ShouPearl>(false));
            yield return new AddCardsToHandAction(cards, AddCardsType.Normal);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
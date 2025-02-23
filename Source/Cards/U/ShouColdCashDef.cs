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
using LBoL.EntityLib.StatusEffects.Cirno;

namespace ShouMod.Cards
{
    public sealed class ShouColdCashDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 1, Blue = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.
            config.Keywords = Keyword.Exile;

            config.RelativeEffects = new List<string>() { nameof(ShouColdCashSe), nameof(Cold) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouColdCashSe), nameof(Cold) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouColdCashDef))]
    public sealed class ShouColdCash : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouColdCashSe>(base.Value1, 0, 0, 0, 0.2f);
            List<Card> cards = new List<Card>();
            cards.Add(Library.CreateCard<ShouOpal>(false));
            cards.Add(Library.CreateCard<ShouSapphire>(false));
            yield return new AddCardsToHandAction(cards, AddCardsType.Normal);

            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
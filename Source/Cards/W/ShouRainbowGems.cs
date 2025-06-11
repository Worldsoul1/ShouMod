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
    public sealed class ShouRainbowGemsDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 23;
            config.UpgradedDamage = 29;

            config.Value1 = 8;
            config.UpgradedValue1 = 8;

            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouRainbowGemsDef))]
    public sealed class ShouRainbowGems : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            //Add a token card to the hand.
            List<Card> list = new List<Card>();
            for (int i = 0; i < base.Value1; i++)
            {
                list.Add(Library.CreateCard(this.Gemstones[i]));
            }
            yield return new AddCardsToDrawZoneAction(list, DrawZoneTarget.Random, AddCardsType.Normal);
        }
    }
}
using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.GunName;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Units;

namespace ShouMod.Cards
{
    public sealed class ShouTreasureBlastDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 16;
            config.UpgradedDamage = 18;

            config.Value1 = 3;
            config.UpgradedValue1 = 4;

            config.Value2 = 8;

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "なゝ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouTreasureBlastDef))]
    public sealed class ShouTreasureBlast : ShouGemstoneReferenceCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.
        public override Interaction Precondition()
        {
            IReadOnlyList<Card> Gemstones = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).ToList<Card>();
            if (Gemstones.Count <= 0)
            {
                return null;
            }
            return new SelectCardInteraction(0, base.Value1, Gemstones, SelectedCardHandling.DoNothing);

        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int IncreasedDamage = 0;
            Unit selectedEnemy = selector.SelectedEnemy;
            SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
            IReadOnlyList<Card> readOnlyList = (selectCardInteraction != null) ? selectCardInteraction.SelectedCards : null;
            if (readOnlyList != null && readOnlyList.Count > 0)
            {
                IncreasedDamage = base.Value2 * readOnlyList.Count;
                foreach (Card card in readOnlyList)
                {
                    yield return new ExileCardAction(card);
                }
                IEnumerator<Card> enumerator = null;
            }
            yield return new DamageAction(Battle.Player, selectedEnemy, DamageInfo.Attack(base.Damage.Damage+IncreasedDamage));
            yield break;
        }
    }
}
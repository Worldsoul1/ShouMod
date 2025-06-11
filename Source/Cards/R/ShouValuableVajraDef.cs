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
using LBoL.Base.Extensions;
using ShouMod.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouValuableVajraDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 15;
            config.UpgradedDamage = 18;

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe) };

            config.Value1 = 1;
            config.UpgradedValue1 = 1;
            config.Value2 = 2;

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "紅葉狩";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouValuableVajraDef))]
    public sealed class ShouValuableVajra : ShouGemstoneReferenceCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            else if (base.Battle.DiscardZone.Count > 0)
            {
                List<Card> list = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).SampleManyOrAll(base.Value1, base.GameRun.BattleRng).ToList<Card>();
                int count = list.Count;
                if (count > 0)
                {
                    foreach (Card card in list)
                    {
                        yield return new ExileCardAction(card);
                        yield return new ApplyStatusEffectAction<ShouVigorSe>(base.Battle.Player, 0, base.Value2, 0, 0, 0.2f);
                    }
                }

            }
            yield break;
        }
    }
}
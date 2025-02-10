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

namespace ShouMod.Cards
{
    public sealed class ShouHiddenSwipeDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 6;
            config.UpgradedDamage = 9;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouHiddenSwipeDef))]
    public sealed class ShouHiddenSwipe : ShouGemstoneReferenceCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<CardEventArgs>(base.Battle.CardExiled, new EventSequencedReactor<CardEventArgs>(this.OnCardExiled));
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

            yield return base.AttackAction(selector, base.GunName);
            yield break;
        }

        private IEnumerable<BattleAction> OnCardExiled(CardEventArgs args)
        {
            if(args.Cause != ActionCause.AutoExile && args.Card is ShouGemstoneCard && base.Zone == CardZone.Discard)
            {
                yield return new MoveCardAction(this, CardZone.Hand);
            }
        }
    }
}
using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.Cards;
using LBoL.EntityLib.StatusEffects.ExtraTurn;
using ShouMod.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouTimeisMoneyDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 3, White = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            //Mana config for the Time Limit
            config.Mana = new ManaGroup() { Any = 1 };

            config.RelativeEffects = new List<string>() { nameof(TimeIsLimited) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(TimeIsLimited) };

            config.Value1 = 5;
            config.Value2 = 8;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouTimeisMoneyDef))]
    //TimeStopCards inhehit from LimitedStopTimeCard instead of Card
    public sealed class ShouTimeisMoney : LimitedStopTimeCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<ShouTimeisMoneySe>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return base.BuffAction<ExtraTurn>(1, 0, 0, 0, 0.2f);
            yield return new RequestEndPlayerTurnAction();
            if (base.Limited)
            {
                yield return base.DebuffAction<TimeIsLimited>(base.Battle.Player, 1, 0, 0, 0, true, 0.2f);
            }
            yield break;
        }
    }
}


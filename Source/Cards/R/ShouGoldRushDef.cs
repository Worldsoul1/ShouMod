using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using ShouMod.StatusEffects;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;

namespace ShouMod.Cards
{
    public sealed class ShouGoldRushDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            //Hybrid colors:
            //0 = W/U
            //1 = W/B
            //2 = W/R
            //3 = W/G
            //4 = U/B
            //5 = U/R
            //6 = U/G
            //7 = B/R
            //8 = B/G
            //9 = R/G
            //As of 1.5.1: Colorless hybrid are not supported.    
            config.Cost = new ManaGroup() { Any = 1, Red =1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 20;
            config.UpgradedDamage = 27;

            config.Value1 = 5;
            

            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouVigorousSe) };

            config.Illustrator = "ウミサソリ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouGoldRushDef))]
    public sealed class ShouGoldRush : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            base.GameRun.LoseMoney(base.Value1);

            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>())
            {
                base.GameRun.GainMoney(base.Value1);
            }
            yield break;
        }
    }
}

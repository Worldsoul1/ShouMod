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
    public sealed class ShouTrackingLaserDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
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
            config.Cost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2};
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 20;
            config.UpgradedDamage = 27;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            config.RelativeEffects = new List<string>() { nameof(Weak), nameof(ShouVigorSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Weak), nameof(ShouVigorSe), nameof(ShouVigorousSe) };

            config.Illustrator = "cato";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(ShouTrackingLaserDef))]
    public sealed class ShouTrackingLaser : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>() && selectedEnemy.IsAlive)
            {
                yield return new ApplyStatusEffectAction<Weak>(selectedEnemy, 0, base.Value1, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}



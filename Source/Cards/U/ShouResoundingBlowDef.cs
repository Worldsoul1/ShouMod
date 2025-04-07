using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.Cards;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Units;

namespace ShouMod.Cards
{
    public sealed class ShouResoundingBlowDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Blue };
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
            config.Cost = new ManaGroup() { Any = 1, Blue = 1 };
            config.UpgradedCost = new ManaGroup() { Blue = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 10;
            config.UpgradedDamage = 10;

            config.Mana = new ManaGroup() { Philosophy = 1 };
            config.UpgradedMana = new ManaGroup() { Philosophy = 1 };

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouHardenedSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouHardenedSe), nameof(ShouVigorousSe) };

            config.Illustrator = "ぽろねぎ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouResoundingBlowDef))]
    public sealed class ShouResoundingBlow : SampleCharacterCard
    {
        public int ResonanceDamage
        {
            get
            {
                if (base.Battle != null && base.Battle.Player.HasStatusEffect<ShouResonanceSe>())
                {
                    return 2 * base.Battle.Player.GetStatusEffect<ShouResonanceSe>().Count;
                }
                return 0;
            }
        }

        // Token: 0x170001A8 RID: 424
        // (get) Token: 0x06000D54 RID: 3412 RVA: 0x00018C14 File Offset: 0x00016E14
        protected override int AdditionalDamage
        {
            get
            {
                return this.ResonanceDamage;
            }
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            bool VigorCheck = false;
            bool HardenCheck = false;
            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>()) { VigorCheck = true; }
            if (base.Battle.Player.HasStatusEffect<ShouHardenSe>()) { HardenCheck = true; }
            if (HardenCheck) { yield return new DamageAction(Battle.Player, selectedEnemy, DamageInfo.Attack(base.Value1, true), base.GunName); }
            if (VigorCheck) { yield return new GainManaAction(Mana); }
            yield break;
        }
    }
}
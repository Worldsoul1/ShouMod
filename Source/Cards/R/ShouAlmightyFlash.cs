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
    public sealed class ShouAlmightyFlashDef : SampleCharacterCardTemplate
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
            config.Cost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 11;
            config.UpgradedDamage = 15;

            config.Value1 = 6;
            config.UpgradedValue1 = 8;

            config.Block = 6;
            config.UpgradedBlock = 8;

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouHardenedSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouHardenedSe), nameof(ShouVigorousSe) };

            config.Illustrator = "エンリ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouAlmightyFlashDef))]
    public sealed class ShouAlmightyFlash : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            bool VigorCheck = false;
            bool HardenCheck = false;
            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>()) { VigorCheck = true; }
            if (base.Battle.Player.HasStatusEffect<ShouHardenSe>()) { HardenCheck = true; }
            if(VigorCheck) { yield return new DamageAction(Battle.Player, selectedEnemy, DamageInfo.Attack(base.Value1, true), base.GunName); }
            if (HardenCheck) { yield return new CastBlockShieldAction(base.Battle.Player, base.Block.Block, 0, BlockShieldType.Normal, true); }
            yield break;
        }
    }
}
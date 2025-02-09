using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Core.Battle.BattleActions;
using JetBrains.Annotations;

namespace ShouMod.Cards
{
    public sealed class ShouTreasureImmaterialDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 3;
            config.UpgradedValue1 = 4;

            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.Mana = new ManaGroup() { Philosophy = 1 };


            //Add Weak, Vulnerable, Firepower Down descrption when hovering over the card.
            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouVigorousSe), nameof(ShouHardenedSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe), nameof(ShouVigorousSe), nameof(ShouHardenedSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouTreasureImmaterialDef))]
    public sealed class ShouTreasureImmaterial : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            //Buff and debuff have either a level or a duration.
            //Duration: Effects that last a certain amount of turns then disappear.
            //Level: Effects that have a fixed duration but that can vary in intensity. 
            //Weak and Vulnerable have a duration, FirepowerNegative has a level.  
            //DebuffAction's 2nd field is the level, the 3rd one is the duration.
            yield return new DrawManyCardAction(base.Value1);

            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>()) 
            {
                yield return new GainManaAction(base.Mana);
            }
            if (base.Battle.Player.HasStatusEffect<ShouHardenSe>())
            {
                yield return base.UpgradeRandomHandAction(base.Value1, CardType.Unknown);
            }

            yield break;
        }
    }
}
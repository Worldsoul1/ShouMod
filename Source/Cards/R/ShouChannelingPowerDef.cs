using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;

namespace ShouMod.Cards
{
    public sealed class ShouChannelingPowerDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            //Add Weak, Vulnerable, Firepower Down descrption when hovering over the card.
            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouChannelingPowerDef))]
    public sealed class ShouChannelingPower : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Buff and debuff have either a level or a duration.
            //Duration: Effects that last a certain amount of turns then disappear.
            //Level: Effects that have a fixed duration but that can vary in intensity. 
            //Weak and Vulnerable have a duration, FirepowerNegative has a level.  
            //DebuffAction's 2nd field is the level, the 3rd one is the duration.
            yield return base.BuffAction<ShouHardenSe>(0, base.Value1, 0, 0, 0.2f);
            yield return base.BuffAction<ShouVigorSe>(0, base.Value1, 0, 0, 0.2f);

            yield break;
        }
    }
}
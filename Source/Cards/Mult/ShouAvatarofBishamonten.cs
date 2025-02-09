using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Enemy;

namespace ShouMod.Cards
{
    public sealed class ShouAvatarofBishamontenDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { White = 2, Red = 2 };
            config.UpgradedCost = new ManaGroup() { White = 1, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Shield = 15;

            config.Value1 = 4;
            config.UpgradedValue1 = 4;

            config.Value2 = 2;

            //Add Weak, Vulnerable, Firepower Down descrption when hovering over the card.
            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouHardenSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouAvatarofBishamontenDef))]
    public sealed class ShouAvatarofBishamonten : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Buff and debuff have either a level or a duration.
            //Duration: Effects that last a certain amount of turns then disappear.
            //Level: Effects that have a fixed duration but that can vary in intensity. 
            //Weak and Vulnerable have a duration, FirepowerNegative has a level.  
            //DebuffAction's 2nd field is the level, the 3rd one is the duration.
            yield return DefenseAction(true);
            yield return base.BuffAction<ShouVigorSe>(0, base.Value1, 0, 0, 0.2f);
            yield return base.BuffAction<ShouHardenSe>(0, base.Value1, 0, 0, 0.2f);
            yield return new AddCardsToDiscardAction(Library.CreateCards<Riguang>(base.Value2, false), AddCardsType.Normal);

            yield break;
        }
    }
}
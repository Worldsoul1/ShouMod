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
    public sealed class ShouAuraofJusticeDef : SampleCharacterCardTemplate
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
            config.UpgradedCost = new ManaGroup() { Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.AllEnemies;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.RelativeEffects = new List<string>() { nameof(Weak), nameof(ShouHardenSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Weak), nameof(ShouHardenSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouAuraofJusticeDef))]
    public sealed class ShouAuraofJustice : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            foreach (BattleAction battleAction in base.DebuffAction<Weak>(base.Battle.AllAliveEnemies, 0, base.Value1, 0, 0, true, 0.03f))
            {
                yield return battleAction;
            }
            yield return base.BuffAction<ShouHardenSe>(0, base.Value1, 0, 0, 0.2f);
            yield break;
        }
    }
}
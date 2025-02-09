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
    public sealed class ShouGleamofHeavenDef : SampleCharacterCardTemplate
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
            config.Cost = new ManaGroup() { Any = 1, Red = 1};
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Value2 = 2;
            config.UpgradedValue2 = 3;

            config.RelativeEffects = new List<string>() { nameof(Graze), nameof(ShouVigorSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Graze), nameof(ShouVigorSe), nameof(ShouVigorousSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouGleamofHeavenDef))]
    public sealed class ShouGleamofHeaven : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return new ApplyStatusEffectAction<Graze>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>())
            {
                yield return new DrawManyCardAction(base.Value2);
            }
            yield break;
        }
    }
}
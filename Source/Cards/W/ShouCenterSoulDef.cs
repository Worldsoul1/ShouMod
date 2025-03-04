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
    public sealed class ShouCenterSoulDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
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
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.UpgradedValue1 = 4;
            config.RelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(ShouVigorSe), nameof(ShouResonanceSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(ShouVigorSe), nameof(ShouResonanceSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouCenterSoulDef))]
    public sealed class ShouCenterSoul : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (base.Battle.Player.HasStatusEffect<ShouVigorSe>() || base.Battle.Player.HasStatusEffect<ShouHardenSe>())
            {
                ShouVigorSe vigor = base.Battle.Player.GetStatusEffect<ShouVigorSe>();
                ShouHardenSe harden = base.Battle.Player.GetStatusEffect<ShouHardenSe>();
                int bonus = base.Value1 * (vigor.Duration + harden.Duration);
                yield return new ApplyStatusEffectAction<ShouResonanceSe>(base.Battle.Player, bonus, 0, 0, 0, 0.2f);
                yield break;
            }
        }
    }
}

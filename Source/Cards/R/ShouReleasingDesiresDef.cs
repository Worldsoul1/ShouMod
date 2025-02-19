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
    public sealed class ShouReleasingDesiresDef : SampleCharacterCardTemplate
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
            config.Cost = new ManaGroup() { Any = 1, Red = 2};
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 11;
            config.UpgradedDamage = 15;

            config.Value1 = 8;
            config.UpgradedValue1 = 6;

            config.RelativeEffects = new List<string>() { nameof(ShouResonanceSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouResonanceSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouReleasingDesiresDef))]
    public sealed class ShouReleasingDesires : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;

            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.Player.HasStatusEffect<ShouResonanceSe>())
            {
                ShouResonanceSe Resonance = base.Battle.Player.GetStatusEffect<ShouResonanceSe>();
                int count = base.Battle.Player.GetStatusEffect<ShouResonanceSe>().Level / base.Value1;
                for (int i = 0; i < count; i++)
                {
                    yield return base.AttackAction(selector, base.GunName);
                }
                yield return new RemoveStatusEffectAction(Resonance, true, 0.1f);
            }
            yield break;
        }
    }
}
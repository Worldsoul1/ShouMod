using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.GunName;
using LBoL.Core.StatusEffects;
using ShouMod.StatusEffects;

namespace ShouMod.SampleCharacterUlt
{
    public sealed class ShouVajraBuddhistDef : SampleCharacterUltTemplate
    {
        public override UltimateSkillConfig MakeConfig()
        {
            UltimateSkillConfig config = GetDefaulUltConfig();
            config.Damage = 23;
            config.Value1 = 3;
            config.Value2 = 1;

            // Add the relative status effects in the description box.   
            config.RelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(Firepower) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouVajraBuddhistDef))]
    public sealed class ShouVajraBuddhist : UltimateSkill
    {
        public ShouVajraBuddhist()
        {
            base.TargetType = TargetType.AllEnemies;
            base.GunName = GunNameID.GetGunFromId(4158);
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            Unit[] targets = selector.GetUnits(base.Battle);
            yield return PerformAction.Spell(base.Owner, nameof(ShouVajraBuddhist));
            yield return new DamageAction(base.Owner, targets, this.Damage, base.GunName, GunType.Single);

            //Only apply the status effect if the enemy is still alive after the attack. 
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            else
            {
                yield return new ApplyStatusEffectAction<ShouHardenSe>(base.Battle.Player, 0, base.Value1, 0, 0, 0.2f);
                yield return new ApplyStatusEffectAction<Firepower>(base.Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            }
        }
    }
}

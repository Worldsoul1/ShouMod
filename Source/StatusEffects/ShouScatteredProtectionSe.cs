using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System;

namespace ShouMod.StatusEffects
{
    public sealed class ShouScatteredProtectionSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.RelativeEffects = new List<string>() { nameof(TempFirepowerNegative), nameof(ShouHardenSe) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouScatteredProtectionSeDef))]
    public sealed class ShouScatteredProtectionSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, this.OnPlayerDamageDealt);
        }

        public IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
        {
            if (args.Source == base.Battle.Player && args.Target != null && args.DamageInfo.DamageType == DamageType.Attack)
            {
                if (args.Target.IsAlive && base.Battle.Player.HasStatusEffect<ShouHardenSe>())
                {
                    yield return new ApplyStatusEffectAction<TempFirepowerNegative>(args.Target, base.Level, 0, 0, 0, 0.2f);
                    //DamageInfo must be either Reaction or HpLoss since Attack could potentially trigger an infinite loop without additional checks.
                }
            }
        }
    }
}

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
    public sealed class ShouLimitlessBenevolenceSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.RelativeEffects = new List<string>() { nameof(LockedOn), nameof(ShouVigorSe) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouLimitlessBenevolenceSeDef))]
    public sealed class ShouLimitlessBenevolenceSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Owner.DamageReceived, this.OnDamageReceived);
        }

        public IEnumerable<BattleAction> OnDamageReceived(DamageEventArgs args)
        {
            if (args.Source != base.Owner && args.Source.IsAlive && args.DamageInfo.DamageType == DamageType.Attack && args.DamageInfo.Amount > 0f)
            {
                if (base.Battle.Player.HasStatusEffect<ShouVigorSe>())
                {
                    yield return new ApplyStatusEffectAction<LockedOn>(args.Source, base.Level, 0, 0, 0, 0.2f);
                    //DamageInfo must be either Reaction or HpLoss since Attack could potentially trigger an infinite loop without additional checks.
                }
            }
        }
    }
}
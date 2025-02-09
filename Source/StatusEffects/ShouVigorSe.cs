using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System;
using JetBrains.Annotations;
using UnityEngine;
using ShouMod.StatusEffects;
using LBoL.Base;

namespace ShouMod.StatusEffects
{
    // Token: 0x02000096 RID: 150
    public sealed class ShouVigorSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            config.HasDuration = true;
            config.DurationDecreaseTiming = LBoL.Base.DurationDecreaseTiming.TurnStart;
            return config;
        }
    }
    [EntityLogic(typeof(ShouVigorSeDef))]
    public sealed class ShouVigorSe : StatusEffect
    {
        // Token: 0x17000253 RID: 595
        // (get) Token: 0x06000721 RID: 1825 RVA: 0x000149DC File Offset: 0x00012BDC
        public int ExtraDamage
        {
            get
            {
                if (base.Battle.Player.HasStatusEffect<ShouResonanceSe>())
                {
                    return 30 + base.Battle.Player.GetStatusEffect<ShouResonanceSe>().Level;
                }
                else
                {
                    return 30;
                }
            }
        }
        // Token: 0x060007D4 RID: 2004 RVA: 0x00016C93 File Offset: 0x00014E93
        protected override void OnAdded(Unit unit)
        {
            base.HandleOwnerEvent<DamageDealingEventArgs>(unit.DamageDealing, new GameEventHandler<DamageDealingEventArgs>(this.OnDamageDealing));
        }

        // Token: 0x060007D5 RID: 2005 RVA: 0x00016CB0 File Offset: 0x00014EB0
        private void OnDamageDealing(DamageDealingEventArgs args)
        {
            int Value = 30;
            if (base.Battle.Player.HasStatusEffect<ShouResonanceSe>())
            {
                Value += base.Battle.Player.GetStatusEffect<ShouResonanceSe>().Level;
            }
            DamageInfo damageInfo = args.DamageInfo;
            if (damageInfo.DamageType == DamageType.Attack)
            {
                damageInfo.Damage = damageInfo.Amount * (1f + (float)Value / 100f);
                args.DamageInfo = damageInfo;
                args.AddModifier(this);
            }
        }
    }
}
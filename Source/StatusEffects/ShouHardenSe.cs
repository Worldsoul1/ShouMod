using System;
using JetBrains.Annotations;
using LBoL.Core.Battle;
using LBoL.Core.Units;
using LBoL.Core.StatusEffects;
using LBoL.ConfigData;
using LBoL.Core;
using UnityEngine;
using ShouMod.StatusEffects;
using LBoLEntitySideloader.Attributes;

namespace ShouMod.StatusEffects
{
    // Token: 0x02000096 RID: 150
    public sealed class ShouHardenSeDef : SampleCharacterStatusEffectTemplate
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
    [EntityLogic(typeof(ShouHardenSeDef))]
    public sealed class ShouHardenSe : StatusEffect
    {
        // Token: 0x17000253 RID: 595
        // (get) Token: 0x06000721 RID: 1825 RVA: 0x000149DC File Offset: 0x00012BDC
        public int ExtraShield
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
        // Token: 0x06000722 RID: 1826 RVA: 0x00014A14 File Offset: 0x00012C14
        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
            {
                base.HandleOwnerEvent<BlockShieldEventArgs>(unit.BlockShieldGaining, new GameEventHandler<BlockShieldEventArgs>(this.OnBlockGaining));
                return;
            }
            Debug.LogError(this.Name + " added to enemy " + unit.Name + ", which has no effect.");
        }

        // Token: 0x06000723 RID: 1827 RVA: 0x00014A64 File Offset: 0x00012C64
        private void OnBlockGaining(BlockShieldEventArgs args)
        {
            int Value = 30;
            if (base.Battle.Player.HasStatusEffect<ShouResonanceSe>()) 
            {
                Value += base.Battle.Player.GetStatusEffect<ShouResonanceSe>().Level;
            }
            if (args.Cause != ActionCause.Card && args.Cause != ActionCause.OnlyCalculate)
            {
                return;
            }
            float num = 1f + (float)Value / 100f;
            if (args.Type == BlockShieldType.Direct)
            {
                return;
            }
            args.Block *= num;
            args.Shield *= num;
        }
    }
}


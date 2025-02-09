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
    public sealed class ShouHardenedSeDef : SampleCharacterStatusEffectTemplate
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
    [EntityLogic(typeof(ShouHardenedSeDef))]
    public sealed class ShouHardenedSe : StatusEffect
    { }
}
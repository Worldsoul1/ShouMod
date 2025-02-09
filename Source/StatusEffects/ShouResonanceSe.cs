using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.StatusEffects;
using System;
using System.Text;

namespace ShouMod.StatusEffects
{
    public sealed class ShouResonanceSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            config.RelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(ShouVigorSe) };
            config.Order = 5;
            return config;
        }
    }
    [EntityLogic(typeof(ShouResonanceSeDef))]

    public sealed class ShouResonanceSe : StatusEffect
    {
    }
}
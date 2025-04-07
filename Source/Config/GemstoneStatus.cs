using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader;
using System;
using UnityEngine;
using LBoL.Core.StatusEffects;
using LBoL.Base;
using System.Collections.Generic;
using Mono.Cecil;
using LBoL.Core.Units;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using ShouMod.StatusEffects;
using System.Threading.Tasks;

namespace ShouMod.Source.Config
{
    public sealed class GemstoneEffect : SampleCharacterStatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(GemstoneStatus);
        }

        //not used for anything
        [DontOverwrite]
        public override Sprite LoadSprite()
        {
            return null;
        }

        public override StatusEffectConfig MakeConfig()
        {
            //not used for anything
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
    }
    [EntityLogic(typeof(GemstoneEffect))]
    public sealed class GemstoneStatus : StatusEffect
    {
    }
}


using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using ShouMod.ImageLoader;
using ShouMod.Localization;
using ShouMod.Config;

namespace ShouMod.StatusEffects
{
    public class SampleCharacterStatusEffectTemplate : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.GetDefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.StatusEffectsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return SampleCharacterImageLoader.LoadStatusEffectLoader(status: this);
        }

        public override StatusEffectConfig MakeConfig()
        {
            return GetDefaultStatusEffectConfig();
        }

        public static StatusEffectConfig GetDefaultStatusEffectConfig()
        {
            return SampleCharacterDefaultConfig.GetDefaultStatusEffectConfig();
        }        
    }
}
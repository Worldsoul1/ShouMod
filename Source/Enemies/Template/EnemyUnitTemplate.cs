using System;
using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using ShouMod.Config;
using ShouMod.Localization;


namespace ShouMod.Enemies.Template
{
    public class SampleCharacterEnemyUnitTemplate : EnemyUnitTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override EnemyUnitConfig MakeConfig()
        {
            return SampleCharacterDefaultConfig.GetEnemyUnitDefaultConfig();
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.EnemiesUnitBatchLoc.AddEntity(this);
        }

        public override Type TemplateType()
        {
            return typeof(EnemyUnitTemplate);
        }

        public EnemyUnitConfig GetEnemyUnitDefaultConfig()
        {
            return SampleCharacterDefaultConfig.GetEnemyUnitDefaultConfig();
        }


    }
}
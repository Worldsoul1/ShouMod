using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using ShouMod.Config;


namespace ShouMod.Enemies.Template
{
    public abstract class SampleCharacterEnemyGroupTemplate : EnemyGroupTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override EnemyGroupConfig MakeConfig()
        {
            return SampleCharacterDefaultConfig.GetEnemyGroupDefaultConfig();
        }

        public EnemyGroupConfig GetEnemyGroupDefaultConfig()
        {
            return SampleCharacterDefaultConfig.GetEnemyGroupDefaultConfig();
        }
    }
}
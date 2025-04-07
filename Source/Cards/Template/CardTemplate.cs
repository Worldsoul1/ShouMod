using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using ShouMod.Config;
using ShouMod.ImageLoader;
using ShouMod.Localization;


namespace ShouMod.Cards.Template
{
    public abstract class SampleCharacterCardTemplate : CardTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override CardImages LoadCardImages()
        {
            return SampleCharacterImageLoader.LoadCardImages(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.CardsBatchLoc.AddEntity(this);
        }

        public CardConfig GetCardDefaultConfig()
        {
            return SampleCharacterDefaultConfig.GetCardDefaultConfig();
        }
    }


}



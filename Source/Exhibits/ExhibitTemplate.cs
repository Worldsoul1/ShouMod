using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using ShouMod.Config;
using ShouMod.ImageLoader;
using ShouMod.Localization;

namespace ShouMod.Exhibits
{
    public class SampleCharacterExhibitTemplate : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.ExhibitsBatchLoc.AddEntity(this);
        }

        public override ExhibitSprites LoadSprite()
        {
            return SampleCharacterImageLoader.LoadExhibitSprite(exhibit: this);
        }

        public override ExhibitConfig MakeConfig()
        {
            return GetDefaultExhibitConfig();
        }

        public ExhibitConfig GetDefaultExhibitConfig()
        {
            return SampleCharacterDefaultConfig.GetDefaultExhibitConfig();
        }

    }
}
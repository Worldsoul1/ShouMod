using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using ShouMod.StatusEffects;

namespace ShouMod.Cards.Template
{
    public abstract class ShouGemstoneReferenceCardTemplate : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.RelativeCards = new List<string> { nameof(ShouAmber), nameof(ShouDiamond), nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };
            config.UpgradedRelativeCards = new List<string> { nameof(ShouAmber), nameof(ShouDiamond), nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };
            return config;
        }
    }
}
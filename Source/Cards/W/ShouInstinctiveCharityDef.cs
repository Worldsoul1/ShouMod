using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using ShouMod.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouInstinctiveCharityDef : ShouGemstoneReferenceCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;
            config.Value1 = 2;

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(ShouInstinctiveCharitySe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouInstinctiveCharitySe) };

            config.RelativeCards = new List<string>() { nameof(ShouAmber), nameof(ShouDiamond), nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };
            config.UpgradedRelativeCards = new List<string>() { nameof(ShouAmber), nameof(ShouDiamond), nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };


            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouInstinctiveCharityDef))]
    public sealed class ShouInstinctiveCharity : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return BuffAction<ShouInstinctiveCharitySe>(base.Value1, 0, 0, 0, 0.2f);

            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
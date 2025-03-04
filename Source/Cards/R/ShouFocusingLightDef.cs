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
    public sealed class ShouFocusingLightDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 2 };
            config.UpgradedCost = new ManaGroup() {  Any = 1, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Value2 = 1;

            config.Mana = new ManaGroup() { Philosophy = 1 };
            config.UpgradedMana = new ManaGroup() { Philosophy = 1 };

            //Keyword that doesn't add an effect to the card, but to add to the card's tooltips.

            config.RelativeEffects = new List<string>() { nameof(Firepower), nameof(ShouFocusingLightSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Firepower), nameof(ShouFocusingLightSe) };

            config.Illustrator = "c7肘";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouFocusingLightDef))]
    public sealed class ShouFocusingLight : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<ShouFocusingLightSe>(base.Value1, 0, 0, 0, 0.2f);
            if(this.IsUpgraded) { base.BuffAction<Firepower>(base.Value2, 0, 0, 0, 0.2f); }
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
        }
    }
}
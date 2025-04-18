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

namespace ShouMod.Cards
{
    public sealed class ShouDiamondDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);
            //If IsPooled is false then the card cannot be discovered or added to the library at the end of combat.
            config.IsPooled = false;

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 2;

            config.ToolPlayableTimes = 1;

            config.Keywords = Keyword.Replenish;
            //Setting Upgrading Keyword only provides the keyword when the card is upgraded.    
            config.UpgradedKeywords = Keyword.Replenish;

            config.RelativeEffects = new List<string>() { nameof(ShouGemstoneReferenceSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouGemstoneReferenceSe) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouDiamondDef))]
    public sealed class ShouDiamond : ShouGemstoneCard
    {
        public override void Initialize()
        {
            base.Initialize();
            DeckCounter = ToolPlayableTimes;
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.

            yield return BuffAction<Amulet>(base.Value1, 0, 0, 0, 0.2f);
            yield return BuffAction<AmuletForCard>(base.Value1, 0, 0, 0, 0.2f);
            DeckCounter--;
            if (this.DeckCounter <= 0) { yield return new ExileCardAction(this); }
            yield break;
            yield break;
        }
    }
}

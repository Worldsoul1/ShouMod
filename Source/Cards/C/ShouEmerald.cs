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

namespace ShouMod.Cards
{
    public sealed class ShouEmeraldDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);
            //If IsPooled is false then the card cannot be discovered or added to the library at the end of combat.
            config.IsPooled = false;

            config.Colors = new List<ManaColor>() { ManaColor.Green };
            config.Cost = new ManaGroup() { Any = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Mana = new ManaGroup() { Philosophy = 2, Colorless = 1 };

            config.ToolPlayableTimes = 1;

            config.Keywords = Keyword.Replenish | Keyword.Tool;
            //Setting Upgrading Keyword only provides the keyword when the card is upgraded.    
            config.UpgradedKeywords = Keyword.Replenish | Keyword.Tool;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouEmeraldDef))]
    public sealed class ShouEmerald : ShouGemstoneCard
    {
        public override void Initialize()
        {
            base.Initialize();
            DeckCounter = ToolPlayableTimes;
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.

            yield return new GainManaAction(base.Mana);
            yield break;
        }
    }
}

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
using ShouMod.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouSapphireDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);
            //If IsPooled is false then the card cannot be discovered or added to the library at the end of combat.
            config.IsPooled = false;

            config.Colors = new List<ManaColor>() { ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 9;
            config.UpgradedDamage = 9;

            config.ToolPlayableTimes = 2;

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

    [EntityLogic(typeof(ShouSapphireDef))]
    public sealed class ShouSapphire : ShouGemstoneCard
    {
        public override void Initialize()
        {
            base.Initialize();
            DeckCounter = ToolPlayableTimes;
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.

            yield return base.AttackAction(selector, base.GunName);
            DeckCounter--;
            if (this.DeckCounter <= 0) { yield return new ExileCardAction(this); }
            yield break;
        }
    }
}
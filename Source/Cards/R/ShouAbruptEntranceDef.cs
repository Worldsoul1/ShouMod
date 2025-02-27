using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouAbruptEntranceDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1};
            config.UpgradedCost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 13;

            //Add Lock On descrption when hovering over the card.
            config.Keywords = Keyword.Initial | Keyword.Accuracy | Keyword.Exile;
            config.UpgradedKeywords = Keyword.Initial | Keyword.Accuracy | Keyword.Exile | Keyword.Replenish;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(ShouAbruptEntranceDef))]
    public sealed class ShouAbruptEntrance : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.
            yield return base.AttackAction(selector, base.GunName);
            //If the battle were to end, skip the rest of the method.
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            //Apply the Lock On Debuff to all alive enemies.
            yield break;
        }
    }
}



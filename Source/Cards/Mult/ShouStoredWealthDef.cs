using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Base.Extensions;
using LBoL.Core.Cards;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using System;
using LBoL.EntityLib.Cards.Character.Marisa;
using System.Linq;

namespace ShouMod.Cards
{
    public sealed class ShouStoredWealthDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, White = 1, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 16;
            config.UpgradedDamage = 20;

            config.Value1 = 5;
            config.UpgradedValue1 = 6;


            //Add Lock On descrption when hovering over the card.

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouStoredWealthDef))]
    public sealed class ShouStoredWealth : ShouGemstoneReferenceCard
    {
        protected override int AdditionalDamage
        {
            get
            {
                if (base.Battle != null)
                {
                    List<Card> pullGemstone = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DrawZoneToShow.Where(c => c is ShouGemstoneCard)).ToList<Card>();
                    return base.Value1 * (pullGemstone.Count);
                }
                return 0;
            }
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.
            yield return base.AttackAction(selector, base.GunName);
            //If the battle were to end, skip the rest of the method.
           
            //Apply the Lock On Debuff to all alive enemies.
            yield break;
        }
    }
}
using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using ShouMod.StatusEffects;
using LBoL.Core.StatusEffects;
using LBoL.Base.Extensions;
using LBoL.Core.Cards;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using System;

namespace ShouMod.Cards
{
    public sealed class ShouSweepingBlastDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 10;
            config.UpgradedDamage = 13;
            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.RelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouVigorousSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouVigorSe), nameof(ShouVigorousSe) };

            //Add Lock On descrption when hovering over the card.

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouSweepingBlastDef))]
    public sealed class ShouSweepingBlast : SampleCharacterCard
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
            else
            {
                if (base.Battle.Player.HasStatusEffect<ShouVigorSe>()) { yield return base.AttackAction(selector, base.GunName); }
            }
            //Apply the Lock On Debuff to all alive enemies.
            yield break;
        }
    }
}
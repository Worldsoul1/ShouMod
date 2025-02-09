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
    public sealed class ShouArcingRaysDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 18;
            config.UpgradedDamage = 25;

            config.Value1 = 2;

            config.RelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(ShouHardenedSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouHardenSe), nameof(ShouHardenedSe) };

            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            //Add Lock On descrption when hovering over the card.

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouArcingRaysDef))]
    public sealed class ShouArcingRays : SampleCharacterCard
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
                if (base.Battle.Player.HasStatusEffect<ShouHardenSe>()) { yield return new DrawManyCardAction(base.Value1); }
            }
            //Apply the Lock On Debuff to all alive enemies.
            yield break;
        }
    }
}

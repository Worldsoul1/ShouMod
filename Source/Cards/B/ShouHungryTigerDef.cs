using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards.Template;
using ShouMod.GunName;

namespace ShouMod.Cards
{
    // Token: 0x020002AD RID: 685
    public sealed class ShouHungryTigerDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);
            //If IsPooled is false then the card cannot be discovered or added to the library at the end of combat.
            config.IsPooled = false;

            config.Colors = new List<ManaColor>() { ManaColor.Black };
            config.Cost = new ManaGroup() { Any = 2, Black = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 18;
            config.UpgradedDamage = 21;

            config.Value1 = 10;

            config.Keywords = Keyword.Exile;
            //Setting Upgrading Keyword only provides the keyword when the card is upgraded.    
            config.UpgradedKeywords = Keyword.Exile | Keyword.Accuracy;

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouHungryTigerDef))]
    public sealed class ShouHungryTiger : SampleCharacterCard
    {
        // Token: 0x06000A39 RID: 2617 RVA: 0x000150EC File Offset: 0x000132EC
        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, new EventSequencedReactor<DieEventArgs>(this.OnEnemyDied));
        }

        // Token: 0x06000A3A RID: 2618 RVA: 0x0001510B File Offset: 0x0001330B
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
            {
                yield return HealAction(base.Value1);
            }
            yield break;
        }
    }
}

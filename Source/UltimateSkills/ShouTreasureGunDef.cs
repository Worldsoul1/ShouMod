using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using System;
using ShouMod.GunName;
using ShouMod.Cards;
using LBoL.Base.Extensions;
//using SampleCharacterMod.BattleActions;

namespace ShouMod.SampleCharacterUlt
{
    public sealed class ShouTreasureGunDef : SampleCharacterUltTemplate
    {
        public override UltimateSkillConfig MakeConfig()
        {
            UltimateSkillConfig config = GetDefaulUltConfig();
            config.Damage = 5;
            config.Value1 = 8;
            config.Value2 = 2;
            config.RelativeCards = new List<string>() { nameof(ShouDiamond), nameof(ShouEmerald), nameof(ShouOnyx), nameof(ShouOpal), nameof(ShouPearl), nameof(ShouRuby), nameof(ShouSapphire) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouTreasureGunDef))]
    public sealed class ShouTreasureGun : UltimateSkill
    {
        public ShouTreasureGun()
        {
            base.TargetType = TargetType.SingleEnemy;
            base.GunName = GunNameID.GetGunFromId(4158);
        }
        public List<Type> Gemstones = new List<Type>
        {
            typeof(ShouDiamond),
            typeof(ShouEmerald),
            typeof(ShouOnyx),
            typeof(ShouOpal),
            typeof(ShouPearl),
            typeof(ShouRuby),
            typeof(ShouSapphire),
        };

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
			    EnemyUnit enemy = selector.GetEnemy(base.Battle);
            yield return PerformAction.Spell(base.Owner, nameof(ShouTreasureGun));
            List<Card> list = new List<Card>();
            this.Gemstones.Shuffle(base.GameRun.BattleCardRng);
            for (int i = 0; i < base.Value2; i++)
            {
                list.Add(Library.CreateCard(this.Gemstones[i]));
            }
            for (int i = 0; i < base.Value1; i++)
            {
                yield return new DamageAction(base.Owner, enemy, this.Damage, base.GunName, GunType.Single);
            }
            if(base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            else
            {
                yield return new AddCardsToHandAction(list, AddCardsType.OneByOne);
            }
                yield break;
        }
    }
}

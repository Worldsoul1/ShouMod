using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using System;

namespace ShouMod.StatusEffects
{
    public sealed class ShouBloodMoneySeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(ShouBloodMoneySeDef))]
    public sealed class ShouBloodMoneySe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.Owner.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, this.OnEnemyDied);
        }

        // Token: 0x0600013B RID: 315 RVA: 0x000045DE File Offset: 0x000027DE
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (!args.Unit.HasStatusEffect<Servant>())
            {
                yield return new GainMoneyAction(base.Level, SpecialSourceType.None);
            }
            yield break;
        }
    }
}
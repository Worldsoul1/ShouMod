using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.Base.Extensions;
using System;

namespace ShouMod.StatusEffects
{
    public sealed class ShouAbsoluteJusticeSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.RelativeEffects = new List<string>() { nameof(Spirit) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouAbsoluteJusticeSeDef))]
    public sealed class ShouAbsoluteJusticeSe : StatusEffect
    {
        private int Counter { get; set; }

        private List<Type> Types { get; set; }
        protected override void OnAdded(Unit unit)
        {
            base.OnAdded(unit);
            this.Counter = 0;
            this.Types = new List<Type>
            {
                typeof(Weak),
                typeof(Vulnerable)
            };
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {

            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (this.Counter % 2 == 0)
            {
                this.Types.Shuffle(base.GameRun.EnemyBattleRng);
            }
            base.NotifyActivating();
            Type type = this.Types[this.Counter % 2];
            Unit player = base.Battle.RandomAliveEnemy;
            int? duration = new int?(base.Level);
            yield return new ApplyStatusEffectAction(type, player, null, duration, null, null, 0f, false);
            int counter = this.Counter + 1;
            this.Counter = counter;
            yield break;
        }
    }
}
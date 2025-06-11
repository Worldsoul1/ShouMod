using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace ShouMod.StatusEffects
{
    public sealed class ShouFocusingLightSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.RelativeEffects = new List<string>() { nameof(Firepower) };
            return config;
        }
    }

    [EntityLogic(typeof(ShouFocusingLightSeDef))]
    public sealed class ShouFocusingLightSe : StatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Single(ManaColor.Philosophy);
            }
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            //At the start of the Player's turn, gain Spirit.
            if (!base.Battle.BattleShouldEnd)
            {
                yield return new GainManaAction(ManaGroup.Philosophies(1));
                yield return BuffAction<Firepower>(base.Level, 0, 0, 0, 0.2f);
                //This is equivalent to:
                //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(base.Owner, base.Level, 0, 0, 0, 0.2f);
                yield break;
            }
            yield break;
        }
    }
}
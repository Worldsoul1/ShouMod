using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;

namespace ShouMod.StatusEffects
{
    // Token: 0x0200006A RID: 106
     public sealed class ShouByakurenTeammateBuffSeDef : SampleCharacterStatusEffectTemplate
{
    public override StatusEffectConfig MakeConfig()
    {
        StatusEffectConfig config = GetDefaultStatusEffectConfig();
        config.HasLevel = false;
        return config;
    }
}
public sealed class ShouByakurenTeammateBuffSe : StatusEffect
    {
        public ManaGroup Mana
        {
            get
            {
                return ManaGroup.Empty;
            }
        }
        // Token: 0x06000177 RID: 375 RVA: 0x00004E50 File Offset: 0x00003050
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnTurnStarted));
        }

        // Token: 0x06000178 RID: 376 RVA: 0x00004E74 File Offset: 0x00003074
        private IEnumerable<BattleAction> OnTurnStarted(UnitEventArgs args)
        {
            Card[] array = base.Battle.RollCardsWithoutManaLimit(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.AllOnes, CardTypeWeightTable.CanBeLoot, false), base.Level, (CardConfig config) => config.Rarity == Rarity.Rare && config.Id != "ShouByakurenTeammateDef");
            foreach (Card card in array)
            {
                card.IsEthereal = true;
                card.IsExile = true;
                card.SetBaseCost(Mana);
            }
            yield return new AddCardsToHandAction(array);
            yield break;
        }
    }
}
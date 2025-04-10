using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.ExtraTurn;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;

namespace ShouMod.StatusEffects
{
    // Token: 0x02000081 RID: 129
    public sealed class ShouTimeisMoneySeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig statusEffectConfig = GetDefaultStatusEffectConfig();
            statusEffectConfig.HasLevel = true;
            return statusEffectConfig;
        }
    }

    [EntityLogic(typeof(ShouTimeisMoneySeDef))]
    public sealed class ShouTimeisMoneySe : ExtraTurnPartner
    {
        public List<Type> Gemstones = new List<Type>
        {
            typeof(ShouAmber),
            typeof(ShouDiamond),
            typeof(ShouEmerald),
            typeof(ShouOnyx),
            typeof(ShouOpal),
            typeof(ShouPearl),
            typeof(ShouRuby),
            typeof(ShouSapphire),
        };
        // Token: 0x060001E1 RID: 481 RVA: 0x00005C34 File Offset: 0x00003E34
        protected override void OnAdded(Unit unit)
        {
            base.ThisTurnActivating = false;
            base.HandleOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, delegate (UnitEventArgs _)
            {
                if (base.Battle.Player.IsExtraTurn && !base.Battle.Player.IsSuperExtraTurn && base.Battle.Player.GetStatusEffectExtend<ExtraTurnPartner>() == this)
                {
                    base.ThisTurnActivating = true;
                }
            });
            base.HandleOwnerEvent<CardEventArgs>(base.Battle.Predraw, new GameEventHandler<CardEventArgs>(this.OnPredraw));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        // Token: 0x060001E2 RID: 482 RVA: 0x00005CA4 File Offset: 0x00003EA4
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.ThisTurnActivating)
            {
                this.Gemstones.Shuffle(base.GameRun.BattleCardRng);
                List<Card> list = new List<Card>();
                for (int i = 0; i < Level; i++)
                {
                    list.Add(Library.CreateCard(this.Gemstones[i]));
                }
                yield return new AddCardsToHandAction(list);
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            yield break;
        }

        // Token: 0x060001E3 RID: 483 RVA: 0x00005CB4 File Offset: 0x00003EB4
        private void OnPredraw(CardEventArgs args)
        {
            if (base.ThisTurnActivating && args.Cause == ActionCause.TurnStart)
            {
                args.CancelBy(this);
            }
        }
    }
}
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Base.Extensions;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using ShouMod.StatusEffects;
using System.Linq;

namespace ShouMod.Cards
{
    public sealed class ShouByakurenTeammateDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red, ManaColor.Black };
            config.Cost = new ManaGroup() { Any = 2, White = 1, Red = 1, Black = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Friend;
            config.TargetType = TargetType.Self;

            config.Block = 5;
            config.UpgradedBlock = 8;

            //Loyalty is called "Unity" ingame.
            config.Loyalty = 4;
            config.UpgradedLoyalty = 5;
            //Passive cost is the passive amount of Unity gained/consumed at the strt of each turn.  
            config.PassiveCost = 2;
            config.UpgradedPassiveCost = 2;
            //Cost of the Active ability. 
            config.ActiveCost = -3;
            config.UpgradedActiveCost = -3;
            //Cost of the Ultimate ability.
            config.UltimateCost = -9;
            config.UpgradedUltimateCost = -8;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;
            config.Value2 = 3;
            config.UpgradedValue2 = 3;

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);

            return config;
        }
    }

    [EntityLogic(typeof(ShouByakurenTeammateDef))]
    public sealed class ShouByakurenTeammate : SampleCharacterCard
    {
        int Counter = 0;
        public string Indent { get; } = "<indent=80>";
        public string PassiveCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Passive\" name=\"{0}\">{1}", base.PassiveCost, Indent);
            }
        }
        public string ActiveCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Active\" name=\"{0}\">{1}", base.ActiveCost, Indent);
            }
        }
        public string UltimateCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Ultimate\" name=\"{0}\">{1}", base.UltimateCost, Indent);
            }
        }

        


        //Effect to trigger at the start of the end.
        public override IEnumerable<BattleAction> OnTurnEndingInHand()
        {
            return this.GetPassiveActions();
        }

        public override IEnumerable<BattleAction> GetPassiveActions()
        {
            //Triigger the effect only if the card has been summoned. 
            if (!base.Summoned || base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            base.NotifyActivating();
            //Increase base loyalty.
            base.Loyalty += base.PassiveCost;
            int num;
            //Trigger the action multiple times if "Mental Energy Injection" is active.
            for (int i = 0; i < base.Battle.FriendPassiveTimes; i = num + 1)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                yield return BuffAction<Reflect>(base.Block.Block * (Counter % 3), 0, 0, 0, 0.2f);
                yield return new CastBlockShieldAction(base.Battle.Player, base.Block.Block * (Counter % 3), 0, BlockShieldType.Direct, false);
                num = i;
            }
            Counter = 0;
            yield break;
        }

        //Action to perform when the teammate card is summoned.
        protected override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            base.ReactBattleEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            yield return BuffAction<Firepower>(base.Value1, 0, 0, 0, 0.2f);

            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
            {
                yield return battleAction;
            }
            IEnumerator<BattleAction> enumerator = null;

            yield break;
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            int CountUp = Counter + 1;

            Counter = CountUp; 
            yield break;
        }

        //When the summoned card is played, choose and resolve either the active or ultimate effect.
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //
            if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Active)
            {
                //Adjust the card's loyalty. 
                //Because the ActiveCost is negative, the Cost has to be added instead of substracted.
                base.Loyalty += base.ActiveCost;
                yield return BuffAction<ShouVigorSe>(0, base.Value1, 0, 0, 0.2f);
                yield return BuffAction<ShouHardenSe>(0, base.Value1, 0, 0, 0.2f);
                if (this.IsUpgraded)
                {
                    yield return BuffAction<ShouResonanceSe>(5, 0, 0, 0, 0.2f);
                }
                yield return base.SkillAnime;
            }
            else
            {
                base.Loyalty += base.UltimateCost;
                base.UltimateUsed = true;
                yield return BuffAction<ShouByakurenTeammateBuffSe>(1, 0, 0, 0, 0.2f);
                yield return base.SkillAnime;
            }
            yield break;
        }
    }
}
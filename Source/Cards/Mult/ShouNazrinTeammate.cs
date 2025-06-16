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
using System.Linq;
using LBoL.Core.Units;
using LBoL.Core.Stations;

namespace ShouMod.Cards
{
    public sealed class ShouNazrinTeammateDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red, ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, Red = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Friend;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 30;
            config.UpgradedDamage = 35;

            //Loyalty is called "Unity" ingame.
            config.Loyalty = 4;
            config.UpgradedLoyalty = 5;
            //Passive cost is the passive amount of Unity gained/consumed at the strt of each turn.  
            config.PassiveCost = 1;
            config.UpgradedPassiveCost = 1;
            //Cost of the Active ability. 
            config.ActiveCost = -4;
            config.UpgradedActiveCost = -3;
            //Cost of the Ultimate ability.
            config.UltimateCost = -9;
            config.UpgradedUltimateCost = -9;

            config.Value1 = 1;
            config.Value2 = 3;
            config.UpgradedValue2 = 5;
            
            
            config.Illustrator = "Fe (tetsu)";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            
            return config;
        }
    }

    [EntityLogic(typeof(ShouNazrinTeammateDef))]
    public sealed class ShouNazrinTeammate : ShouGemstoneReferenceCard
    {
        bool hasActivated = false;
        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, this.OnEnemyDied);
        }
        public string Indent {get;} = "<indent=80>";
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
        public override IEnumerable<BattleAction> OnTurnStartedInHand()
		{
			return this.GetPassiveActions();
		}

        public override IEnumerable<BattleAction> GetPassiveActions()
		{
            //Triigger the effect only if the card has been summoned. 
			if (!base.Summoned || base.Battle.BattleShouldEnd)
			{
                hasActivated = false;
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
                List<Card> list = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).Concat(base.Battle.DrawZoneToShow.Where(c => c is ShouGemstoneCard)).SampleManyOrAll(1, base.GameRun.BattleRng).ToList<Card>();
                int count = list.Count;
                if (count > 0)
                {
                    foreach (Card card in list)
                    {
                        yield return new MoveCardAction(card, CardZone.Hand);
                    }
                }
                num = i;
			}
			yield break;
		}

        //Action to perform when the teammate card is summoned.
        protected override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
            List<Card> list = new List<Card>();
            this.CommonGemstones.Shuffle(base.GameRun.BattleCardRng);
            for (int i = 0; i < base.Value2; i++)
            {
                list.Add(Library.CreateCard(this.CommonGemstones[i]));
            }
            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false)
            {
                Source = this
            };
            yield return new InteractionAction(interaction, false);
            Card selectedCard = interaction.SelectedCard;
            if (selectedCard != null)
            {
                yield return new AddCardsToDiscardAction(new Card[] { selectedCard });
            }
            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
            {
                yield return battleAction;
            }
            IEnumerator<BattleAction> enumerator = null;

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
                if (base.Battle.DiscardZone.Count > 0)
                {
                    List<Card> pullGemstone = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).ToList<Card>();
                    if (!pullGemstone.Empty<Card>())
                    {
                        SelectCardInteraction interaction = new SelectCardInteraction(0, 2, pullGemstone);
                        yield return new InteractionAction(interaction, false);
                        List<Card> selected = interaction.SelectedCards.ToList<Card>();
                        if (selected.Count > 0)
                        {
                            foreach (Card card in selected)
                            {
                                yield return new MoveCardAction(card, CardZone.Hand);
                            }
                        }
                        yield break;
                    }

                }
                yield return base.SkillAnime;
			}
			else
			{
				base.Loyalty += base.UltimateCost;
                base.UltimateUsed = true;
                foreach (Unit enemyUnit in base.Battle.AllAliveEnemies) 
                {
                    yield return new DamageAction(base.Battle.Player, enemyUnit, DamageInfo.Attack(base.Damage.Damage, true), base.GunName, GunType.Single);
                }
                yield return base.SkillAnime;
			}
			yield break;
		}
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (args.DieSource == this && args.Unit is EnemyUnit enemyUnit && (base.Battle.EnemyGroup.EnemyType == EnemyType.Elite || base.Battle.EnemyGroup.EnemyType == EnemyType.Boss) && !args.Unit.HasStatusEffect<Servant>() && !hasActivated) //EnemyType.Elite = 2; EnemyType.Boss = 3. 
            {
                GameRun.CurrentStation.Rewards.Add(StationReward.CreateExhibit(base.GameRun.CurrentStage.GetEliteEnemyExhibit()));
                hasActivated = true;
            }
            yield break;
        }
    }
}



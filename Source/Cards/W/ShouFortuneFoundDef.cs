using System;
using System.Collections.Generic;
using System.Linq;
using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Base.Extensions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Randoms;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;


namespace ShouMod.Cards
{ 
    public sealed class ShouFortuneFoundDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
        
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 0 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.SingleEnemy;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.Value2 = 1;

            config.RelativeEffects = new List<string>() { nameof(Weak) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Weak) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(ShouFortuneFoundDef))]
    public sealed class ShouFortuneFound : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            if (base.Battle.DiscardZone.Count > 0)
            {
                List<Card> list = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).SampleManyOrAll(base.Value1, base.GameRun.BattleRng).ToList<Card>();
                int count = list.Count;
                if (count > 0) 
                {
                    foreach (Card card in list) 
                    {
                        yield return new MoveCardAction(card, CardZone.Hand);
                    }
                }

            }

            yield return base.DebuffAction<Weak>(selectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);

            //To choose more than 1 card.
            /*Interaction interactionMultiple = new SelectCardInteraction(0, base.Value2, array, 0);
            IReadOnlyList<Card> cards = ((SelectCardInteraction)interactionMultiple).SelectedCards;
            yield return new AddCardsToHandAction(cards);*/

            yield break;
        }
    }
}



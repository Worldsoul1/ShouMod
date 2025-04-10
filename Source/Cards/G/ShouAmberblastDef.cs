using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.Cards;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Cirno;
using System.Linq;

namespace ShouMod.Cards
{
    public sealed class ShouAmberblastDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Green };
            //Hybrid colors:
            //0 = W/U
            //1 = W/B
            //2 = W/R
            //3 = W/G
            //4 = U/B
            //5 = U/R
            //6 = U/G
            //7 = B/R
            //8 = B/G
            //9 = R/G
            //As of 1.5.1: Colorless hybrid are not supported.    
            config.Cost = new ManaGroup() { Any = 1, Green = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 15;
            config.UpgradedDamage = 20;

            config.Value2 = 2;
            config.UpgradedValue2 = 2;

            

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouAmberblastDef))]
    public sealed class ShouAmberblast : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            base.AttackAction(selector);
            List<ShouAmberblast> list = Library.CreateCards<ShouAmberblast>(2, this.IsUpgraded).ToList<ShouAmberblast>();
            ShouAmberblast first = list[0];
            ShouAmberblast gemChoice = list[1];
            first.ChoiceCardIndicator = 1;
            gemChoice.ChoiceCardIndicator = 2;
            first.SetBattle(base.Battle);
            gemChoice.SetBattle(base.Battle);
            MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false)
            {
                Source = this
            };
            yield return new InteractionAction(interaction, false);
            if (interaction.SelectedCard == first)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<ShouEmerald>(base.Value2, false));
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouAmber>(base.Value2, false));
            }
            else
            {
                yield return new AddCardsToHandAction(Library.CreateCards<ShouAmber>(base.Value2, false));
                yield return new AddCardsToDiscardAction(Library.CreateCards<ShouEmerald>(base.Value2, false));
            }
            yield break;
        }
    }
}
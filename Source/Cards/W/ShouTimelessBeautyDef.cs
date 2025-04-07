using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using ShouMod.StatusEffects;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using System.Linq;

namespace ShouMod.Cards
{
    public sealed class ShouTimelessBeautyDef : ShouGemstoneReferenceCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
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
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 3;
            config.UpgradedValue1 = 3;
            config.Block = 5;
            config.UpgradedBlock = 7;


            config.Illustrator = "frozen time";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouTimelessBeautyDef))]
    public sealed class ShouTimelessBeauty : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            DrawManyCardAction drawAction = new DrawManyCardAction(base.Value1);
            yield return drawAction;
            IReadOnlyList<Card> drawnCards = drawAction.DrawnCards;
            int num = drawnCards.Where(c => c is ShouGemstoneCard).Count();
            if (num > 0)
            {
                yield return base.DefenseAction(base.Block.Block * num, 0, BlockShieldType.Direct, false);
            }
            yield break;
        }
    }
}

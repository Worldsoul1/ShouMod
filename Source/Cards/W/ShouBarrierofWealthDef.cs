using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using ShouMod.Cards.Template;
using ShouMod.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Base.Extensions;
using LBoL.Core.StatusEffects;

namespace ShouMod.Cards
{
    public sealed class ShouBarrierofWealthDef : ShouGemstoneReferenceCardTemplate
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
            config.Cost = new ManaGroup() { White = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 15;
            config.UpgradedBlock = 20;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.RelativeEffects = new List<string>() { nameof(ShouHardenSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(ShouHardenSe) };

            config.Illustrator = "Radal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShouBarrierofWealthDef))]
    public sealed class ShouBarrierofWealth : ShouGemstoneReferenceCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new CastBlockShieldAction(base.Battle.Player, base.Block.Block, 0, BlockShieldType.Normal, true);
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            else if (base.Battle.DiscardZone.Count > 0)
            {
                List<Card> list = base.Battle.DiscardZone.Where(c => c is ShouGemstoneCard).SampleManyOrAll(base.Value1, base.GameRun.BattleRng).ToList<Card>();
                int count = list.Count;
                if (count > 0)
                {
                    foreach (Card card in list)
                    {
                        yield return new ExileCardAction(card);
                        yield return new ApplyStatusEffectAction<ShouHardenSe>(base.Battle.Player, 0, base.Value2, 0, 0, 0.2f);
                    }
                }

            }
            yield break;
        }
    }
}
using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Exhibits;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;

namespace ShouMod.Exhibits
{
    public sealed class ShouMouseBasketDef : SampleCharacterExhibitTemplate
    {   
        public override ExhibitConfig MakeConfig()
        {

            ExhibitConfig exhibitConfig = this.GetDefaultExhibitConfig();
            exhibitConfig.Value1 = 1;
            exhibitConfig.Value2 = 1;
            exhibitConfig.Mana = new ManaGroup() { White = 1 };
            exhibitConfig.BaseManaColor = ManaColor.White;

            exhibitConfig.HasCounter = true;
            exhibitConfig.InitialCounter = 0;

            exhibitConfig.RelativeCards = new List<string>() { nameof(ShouNazrinTeammate) };
            
            return exhibitConfig;
        }
    }

    [EntityLogic(typeof(ShouMouseBasketDef))]
    public sealed class ShouMouseBasket : ShiningExhibit
    {
        protected override void OnEnterBattle()
        {
            base.ReactBattleEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarted));
        }

        private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
        {
            if (base.Battle.Player.TurnCounter == 1)
            {
                base.NotifyActivating();
                yield return new AddCardsToHandAction(Library.CreateCards<ShouNazrinTeammate>(base.Value1, false), AddCardsType.Normal);
            }
            yield break;
        }
    }
}
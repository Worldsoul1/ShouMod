using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.Exhibits;
using LBoLEntitySideloader.Attributes;
using ShouMod.Cards;
using LBoL.Core.StatusEffects;
using System;
using LBoL.Base.Extensions;
using YamlDotNet.Core.Tokens;

namespace ShouMod.Exhibits
{
    public sealed class ShouDullPagodaDef : SampleCharacterExhibitTemplate
    {
        public override ExhibitConfig MakeConfig()
        {
            ExhibitConfig exhibitConfig = this.GetDefaultExhibitConfig();

            exhibitConfig.Value1 = 1;
            exhibitConfig.Mana = new ManaGroup() { Philosophy = 1 };
            exhibitConfig.BaseManaColor = ManaColor.Red;
            exhibitConfig.RelativeEffects = new List<string>() { nameof(Firepower) };
            return exhibitConfig;
        }
    }

    [EntityLogic(typeof(ShouDullPagodaDef))]
    public sealed class ShouDullPagoda : ShiningExhibit
    {
        
        protected override void OnEnterBattle()
        {
            base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(this.OnBattleStarted));
        }

        private IEnumerable<BattleAction> OnBattleStarted(GameEventArgs args)
        {
            base.NotifyActivating();
            yield return new GainManaAction(base.Mana);
            yield return new ApplyStatusEffectAction<Firepower>(base.Owner, 1, null, null, null, 0f, true);
            yield break;
        }
    }
}
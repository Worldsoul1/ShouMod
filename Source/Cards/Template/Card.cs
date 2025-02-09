using LBoL.Core.Cards;

namespace ShouMod.Cards.Template
{
    public class SampleCharacterCard : Card
    {
        //SampleCharacterCard can be used to give additional properties to all the cards.
        //For instance, this can be used to give every card a new custom parameter called Value3. 
        //Custom value for display purposes.
        protected virtual int BaseValue3 { get; set; } = 0;
        protected virtual int BaseUpgradedValue3 { get; set; } = 0;

        public virtual bool isGemstone { get; set; }
        public int Value3
        {
            get
            {
                if (this.IsUpgraded)
                {
                    return BaseUpgradedValue3;
                }
                return BaseValue3;
            }
        }
    }
}
using ShouMod.Cards;
using LBoL.EntityLib.Cards.Neutral.TwoColor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShouMod.Cards.Template
{
    public abstract class ShouGemstoneReferenceCard : SampleCharacterCard
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

        public List<Type> CommonGemstones = new List<Type>
        {
            typeof(ShouEmerald),
            typeof(ShouOnyx),
            typeof(ShouPearl),
            typeof(ShouRuby),
            typeof(ShouSapphire),
            typeof(ShouOpal),
        };

    }
}

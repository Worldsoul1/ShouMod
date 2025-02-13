using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using ShouMod.Cards;
using ShouMod.Exhibits;
using ShouMod.SampleCharacterUlt;
namespace ShouMod
{
    public class SampleCharacterLoadouts
    {
        public static string UltimateSkillA = nameof(ShouVajraBuddhist);
        public static string UltimateSkillB = nameof(ShouTreasureGun);

        public static string ExhibitA = nameof(ShouDullPagoda);
        public static string ExhibitB = nameof(ShouMouseBasket);
        public static List<string> DeckA = new List<string>{
            nameof(Shoot),
            nameof(Shoot),
            nameof(Boundary),
            nameof(Boundary),
            nameof(ShouAttackW),
            nameof(ShouAttackR), 
            nameof(ShouBlockW), 
            nameof(ShouBlockW),
            nameof(ShouBlockR),
            nameof(ShouChannelingPower), 
        };

        public static List<string> DeckB = new List<string>{
            nameof(Shoot),
            nameof(Shoot),
            nameof(Boundary),
            nameof(Boundary),
            nameof(ShouAttackW),
            nameof(ShouAttackW), 
            nameof(ShouBlockW), 
            nameof(ShouBlockR),
            nameof(ShouBlockW),
            nameof(ShouScatteredTreasure),
        };

        public static PlayerUnitConfig playerUnitConfig = new PlayerUnitConfig(
            Id: BepinexPlugin.modUniqueID,
            ShowOrder: 8, 
            Order: 0,
            UnlockLevel: 0,
            ModleName: "",
            NarrativeColor: "#f3c259",
            IsSelectable: true,
            MaxHp: 80,
            InitialMana: new ManaGroup() { White = 3, Red = 1},
            InitialMoney: 99,
            InitialPower: 0,
            UltimateSkillA: SampleCharacterLoadouts.UltimateSkillA,
            UltimateSkillB: SampleCharacterLoadouts.UltimateSkillB,
            ExhibitA: SampleCharacterLoadouts.ExhibitA,
            ExhibitB: SampleCharacterLoadouts.ExhibitB,
            DeckA: SampleCharacterLoadouts.DeckA,
            DeckB: SampleCharacterLoadouts.DeckB,
            DifficultyA: 1,
            DifficultyB: 3
        );
    }
}

using HarmonyLib;

namespace ShouMod
{
    public static class PInfo
    {
        //Rename the variable below to prevent conflicts between mod.
        public const string GUID = "Worldsoul15.LBoL.Character.ShouMod";
        public const string Name = "ShouMod";
        public const string version = "0.1.5";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}

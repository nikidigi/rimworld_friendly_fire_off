using HarmonyLib;
using UnityEngine;
using Verse;

namespace FriendlyFireOff
{
    public class Mod : Verse.Mod
    {
        public ModSettings settings;

        public Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings>();

            Harmony harmony = new("nikidigi.friendlyfireoff");

            harmony.PatchAll();

            Log.Message("Friendly Fire Off initialized.");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Friendly Fire Off";
        }
    }
}

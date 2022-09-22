using UnityEngine;
using Verse;

namespace FriendlyFireOff
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool disableExplosives = false;

        public static bool disableRanged = true;

        public static bool displayDamage = true;

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard options = new();

            options.Begin(inRect);

            options.CheckboxLabeled("Ranged weapons", ref disableRanged, "Disable friendly fire from ranged weapons");
            options.CheckboxLabeled("Explosives", ref disableExplosives, "Disable friendly fire from explosives (grenades, rockets)");

            options.GapLine();
            options.CheckboxLabeled("Display friendly fire damage", ref displayDamage);
            options.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref disableExplosives, "disableExplosives", false);
            Scribe_Values.Look(ref disableRanged, "disableRanged", true);

            Scribe_Values.Look(ref displayDamage, "displayDamage", true);
        }
    }
}

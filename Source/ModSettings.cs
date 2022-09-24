using UnityEngine;
using Verse;

namespace FriendlyFireOff
{
    public class ModSettings : Verse.ModSettings
    {
        public static bool displayDamage = true;

        public static bool playerRanged = true;

        public static bool playerExplosives = false;

        public static bool friendlyRanged = true;

        public static bool friendlyExplosives = false;

        public static bool hostileRanged = true;

        public static bool hostileExplosives = false;

        public static bool IsRangedDisabled(bool IsPlayer, bool IsHostile)
        {
            if (IsPlayer)
            {
                return playerRanged;
            }

            return IsHostile ? hostileRanged : friendlyRanged;
        }

        public static bool IsExplosivesDisabled(bool IsPlayer, bool IsHostile)
        {
            if (IsPlayer)
            {
                return playerExplosives;
            }

            return IsHostile ? hostileExplosives : friendlyExplosives;
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard options = new();

            options.Begin(inRect);

            options.Label("General");
            options.GapLine();
            options.CheckboxLabeled("Display friendly fire damage", ref displayDamage);
            options.Gap();

            options.Label("Player");
            options.GapLine();
            options.CheckboxLabeled("Ranged weapons", ref playerRanged, "Check to disable friendly fire from ranged weapons");
            options.CheckboxLabeled("Explosives", ref playerExplosives, "Check to disable friendly fire from explosives (grenades, rockets)");
            options.Gap();

            options.Label("Friendly AI");
            options.GapLine();
            options.CheckboxLabeled("Ranged weapons", ref friendlyRanged, "Check to disable friendly fire from ranged weapons");
            options.CheckboxLabeled("Explosives", ref friendlyExplosives, "Check to disable friendly fire from explosives (grenades, rockets)");
            options.Gap();

            options.Label("Hostile AI");
            options.GapLine();
            options.CheckboxLabeled("Ranged weapons", ref hostileRanged, "Check to disable friendly fire from ranged weapons");
            options.CheckboxLabeled("Explosives", ref hostileExplosives, "Check to disable friendly fire from explosives (grenades, rockets)");
            options.Gap();

            options.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref displayDamage, "displayDamage", true);

            Scribe_Values.Look(ref playerRanged, "playerRanged", true);
            Scribe_Values.Look(ref playerExplosives, "playerExplosives", false);

            Scribe_Values.Look(ref friendlyRanged, "friendlyRanged", true);
            Scribe_Values.Look(ref friendlyExplosives, "friendlyExplosives", false);

            Scribe_Values.Look(ref hostileRanged, "hostileRanged", true);
            Scribe_Values.Look(ref hostileExplosives, "hostileExplosives", false);
        }
    }
}

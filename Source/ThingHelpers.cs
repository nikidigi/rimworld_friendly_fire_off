using RimWorld;
using UnityEngine;
using Verse;

namespace FriendlyFireOff
{
    public static class ThingHelpers
    {
        public static bool IsPrisoner(Thing thing)
        {
            return thing is Pawn && (thing as Pawn).IsPrisoner;
        }

        public static bool IsSlave(Thing thing)
        {
            return thing is Pawn && (thing as Pawn).IsSlave;
        }

        public static bool IsHostileFaction(Thing thing, Thing other)
        {
            if (thing.Faction is null || other.Faction is null)
            {
                return true;
            }

            return thing.Faction.loadID != other.Faction.loadID &&
                thing.Faction.RelationKindWith(other.Faction) == FactionRelationKind.Hostile;
        }

        public static void ThrowText(Thing thing, string text, Color? color = null)
        {
            if (thing.Map is not null)
            {
                MoteMaker.ThrowText(thing.DrawPos, thing.Map, text, color ?? Color.white, 3.33f);
            }
        }
    }
}

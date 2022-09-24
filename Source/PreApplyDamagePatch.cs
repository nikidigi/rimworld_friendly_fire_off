using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace FriendlyFireOff
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.PreApplyDamage))]
    class PreApplyDamagePatch
    {
        public static bool Prefix(Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;

            if (dinfo.Def.isExplosive)
            {
                return PrefixExplosive(__instance, ref dinfo, out absorbed);
            }

            if (dinfo.Def.isRanged)
            {
                return PrefixRanged(__instance, ref dinfo, out absorbed);
            }

            return true;
        }

        private static bool PrefixExplosive(Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;

            Thing attacker = dinfo.Instigator;
            Thing target = dinfo.IntendedTarget;

            if (attacker is null || target?.ThingID == __instance.ThingID)
            {
                return true;
            }

            if (ThingHelpers.IsHostileFaction(__instance, attacker))
            {
                return true;
            }

            if (ThingHelpers.IsPrisoner(attacker) || ThingHelpers.IsSlave(attacker))
            {
                return true;
            }

            bool IsPlayer = __instance.Faction?.IsPlayer == true || attacker.Faction?.IsPlayer == true;
            bool IsHostile = __instance.Faction?.HostileTo(Faction.OfPlayer) ?? true;

            if (ModSettings.IsExplosivesDisabled(IsPlayer, IsHostile))
            {
                absorbed = true;

                if (ModSettings.displayDamage)
                {
                    ThingHelpers.ThrowText(__instance, $"{dinfo.Amount} ({attacker})", Color.magenta);
                }

                if (Prefs.DevMode)
                {
                    Log.Message($"Friendly Fire Off | {attacker} hit {__instance} with an explosive. {dinfo.Amount} damage has been absorbed.");
                }

                return false;
            }

            return true;
        }

        private static bool PrefixRanged(Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;

            Thing attacker = dinfo.Instigator;
            Thing target = dinfo.IntendedTarget;

            if (attacker is null || target is null || target.ThingID == __instance.ThingID)
            {
                return true;
            }

            if (ThingHelpers.IsHostileFaction(__instance, attacker))
            {
                return true;
            }

            if (ThingHelpers.IsPrisoner(attacker) || ThingHelpers.IsSlave(attacker))
            {
                return true;
            }

            bool IsPlayer = __instance.Faction?.IsPlayer == true || attacker.Faction?.IsPlayer == true;
            bool IsHostile = __instance.Faction?.HostileTo(Faction.OfPlayer) ?? true;

            if (ModSettings.IsRangedDisabled(IsPlayer, IsHostile))
            {
                absorbed = true;

                if (ModSettings.displayDamage)
                {
                    ThingHelpers.ThrowText(__instance, $"{dinfo.Amount} ({attacker})", Color.cyan);
                }

                if (Prefs.DevMode)
                {
                    Log.Message($"Friendly Fire Off | {attacker} fired at {target} and hit {__instance}. {dinfo.Amount} damage has been absorbed.");
                }

                return false;
            }

            return true;
        }
    }
}

using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using System.Reflection;
using HarmonyLib;
using System;

namespace CombatExtended.ExtendedLoadout
{
    public static class CombatReflected
    {
        public static float margin = 6f; //CE.Dialog_ManageLoadouts._margin 
        public static float barHeight = 24f; //CE.Dialog_ManageLoadouts._margin
        public static float IconSize = 16f; //CE.PawnColumnWorker_Loadout.IconSize

        //method CE.PawnColumnWorker_Loadout.textGetter(): string textGetter (string str)
        static MethodInfo textGetterInfo = AccessTools.Method(typeof(PawnColumnWorker_Loadout), "textGetter");
        public static string textGetter(string untranslatedString)
        {
            if (textGetterInfo == null) 
                throw new Exception("Have no access to textGetter (method is null)");
            return (string)textGetterInfo.Invoke(null, new object[] { untranslatedString });
        }

        //method CE.Utility_HoldTracker.HoldTrackerAnythingHeld(): bool HoldTrackerAnythingHeld(this Pawn pawn)
        static Type holdTracker = AccessTools.TypeByName("Utility_HoldTracker");
        static MethodInfo anythingHeld = AccessTools.Method(holdTracker, "HoldTrackerAnythingHeld");
        public static bool AnythingHeld(Pawn pawn)
        {
            if (anythingHeld == null) 
                throw new Exception("Have no access to HoldTrackerAnythingHeld (method is null)");
            return (bool)anythingHeld.Invoke(null, new object[] { pawn});
        }

        //Rimworld.PawnColumnWorker textures
        //SortingIcon
        static FieldInfo sortingIconField = AccessTools.Field(typeof(PawnColumnWorker), "SortingIcon");
        public static Texture2D SortingIcon = (Texture2D)sortingIconField.GetValue(null);

        //SortingDescendingIcon
        static FieldInfo sortingIconDescField = AccessTools.Field(typeof(PawnColumnWorker), "SortingDescendingIcon");
        public static Texture2D SortingDescendingIcon = (Texture2D)sortingIconDescField.GetValue(null);

        //Loadout.uniqueID
        static FieldInfo uniqueID = AccessTools.Field(typeof(Loadout), "uniqueID");
        public static int GetUniqueID(this Loadout loadout)
        {
            if (loadout == null || uniqueID == null)
                throw new Exception("Error to access uniqueID of Loadout. Something is null");
            return (int)uniqueID.GetValue(loadout);
        }
    }
}
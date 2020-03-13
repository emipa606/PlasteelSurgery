using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlasteelSurgery
{
    [DefOf]
    public static class PS_DefOf
    {
        static PS_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PS_DefOf));
        }

        public static HediffDef PS_HediffDefs_BotchedLaryngoplasty;
        
    }
}

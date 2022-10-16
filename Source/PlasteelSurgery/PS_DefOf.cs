using RimWorld;
using Verse;

namespace PlasteelSurgery;

[DefOf]
public static class PS_DefOf
{
    public static HediffDef PS_HediffDefs_BotchedLaryngoplasty;

    static PS_DefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PS_DefOf));
    }
}
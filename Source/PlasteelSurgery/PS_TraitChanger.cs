using RimWorld;
using Verse;

namespace PlasteelSurgery;

public static class PS_TraitChanger
{
    public static void Remove(Pawn pawn, Trait trait)
    {
        var theirTrait = pawn.story?.traits.allTraits!.FirstOrDefault(x => x.def == trait.def);
        if (theirTrait == null)
        {
            return;
        }

        pawn.story?.traits.allTraits.Remove(theirTrait);
        SafeApplyChange(pawn);
    }

    public static void AddTrait(Pawn pawn, Trait trait)
    {
        pawn?.story?.traits.GainTrait(trait);
        SafeApplyChange(pawn);
    }

    public static void SafeApplyChange(Pawn pawn)
    {
        pawn.Notify_DisabledWorkTypesChanged();
        pawn.workSettings?.Notify_DisabledWorkTypesChanged();

        pawn.skills?.Notify_SkillDisablesChanged();

        if (!pawn.Dead && pawn.RaceProps.Humanlike)
        {
            pawn.needs.mood.thoughts.situational.Notify_SituationalThoughtsDirty();
        }
    }
}
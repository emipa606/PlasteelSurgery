using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace PlasteelSurgery
{
    public static class PS_TraitChanger
    {
        public static void Remove(Pawn pawn, Trait trait)
        {
            var theirTrait = pawn.story?.traits.allTraits.Where(x => x.def == trait.def).FirstOrDefault();
            if (theirTrait == null)
                return;

            pawn?.story?.traits.allTraits.Remove(theirTrait);
            SafeApplyChange(pawn);
        }

        public static void AddTrait(Pawn pawn, Trait trait)
        {
            pawn?.story?.traits.GainTrait(trait);
            SafeApplyChange(pawn);
        }

        public static void SafeApplyChange(Pawn pawn)
        {

            if (pawn.workSettings != null)
            {
                pawn.workSettings.Notify_DisabledWorkTypesChanged();
            }
            //pawn.story.Notify_TraitChanged(); <- Internal method, need to use reflection
            typeof(Pawn_StoryTracker).GetField("cachedDisabledWorkTypes", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(pawn.story, null);
            if (pawn.skills != null)
            {
                pawn.skills.Notify_SkillDisablesChanged();
            }
            if (!pawn.Dead && pawn.RaceProps.Humanlike)
            {
                pawn.needs.mood.thoughts.situational.Notify_SituationalThoughtsDirty();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace PlasteelSurgery;

public class PS_Recipes_Surgery_ImmunityBoost : Recipe_Surgery
{
    public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
    {
        var hasTraits = pawn?.story?.traits != null;
        if (!hasTraits)
        {
            yield break;
        }

        var trait = pawn.story?.traits?.allTraits?.Where(x => x.def.defName == "Immunity").FirstOrDefault();
        if (trait is { Degree: 1 })
        {
            yield break;
        }

        yield return pawn.RaceProps.body.corePart;
    }

    public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients,
        Bill bill)
    {
        if (billDoer == null)
        {
            return;
        }

        if (!CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
        {
            TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);

            var trait = pawn?.story?.traits?.allTraits?.Where(x => x.def.defName == "Immunity")
                .FirstOrDefault();
            if (trait is { Degree: -1 })
            {
                PS_TraitChanger.Remove(pawn, new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), -1));
            }
            else if (trait == null)
            {
                PS_TraitChanger.AddTrait(pawn, new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), 1));
            }

            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort,
                    pawn?.LabelShort, "PS_Messages_Surgery_ImmunityBoost".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.TaskCompletion);
        }
        else
        {
            var trait = pawn?.story?.traits?.allTraits?.Where(x => x.def.defName == "Immunity")
                .FirstOrDefault();
            if (trait is { Degree: 1 })
            {
                PS_TraitChanger.Remove(pawn, new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), 1));
            }
            else if (trait == null)
            {
                PS_TraitChanger.AddTrait(pawn, new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), -1));
            }

            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort,
                    pawn?.LabelShort, "PS_Messages_Surgery_ImmunityBoost".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.NegativeHealthEvent);
        }
    }
}
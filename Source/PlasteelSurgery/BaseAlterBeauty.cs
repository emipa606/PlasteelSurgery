using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PlasteelSurgery;

public class BaseAlterBeauty : Recipe_InstallArtificialBodyPart
{
    private static readonly TraitDef beauty = TraitDef.Named("Beauty");

    protected virtual List<int> AllowedDegrees()
    {
        return [0];
    }

    protected virtual int GetChange()
    {
        return 0;
    }

    protected virtual int GetFailBeauty()
    {
        return 0;
    }

    public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
    {
        if (pawn?.story?.traits == null)
        {
            yield break;
        }

        var currentBeauty = GetCurrentBeauty(pawn);
        var allowedDegrees = AllowedDegrees();

        if (allowedDegrees.Contains(currentBeauty))
        {
            yield return pawn.RaceProps.body.corePart;
        }
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
            SetBeauty(pawn, GetCurrentBeauty(pawn) + GetChange());
            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort,
                    pawn.LabelShort, "PS_Messages_Surgery_Facial".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.TaskCompletion);
        }
        else
        {
            SetBeauty(pawn, GetCurrentBeauty(pawn) - 1);
            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort,
                    pawn.LabelShort, "PS_Messages_Surgery_Facial".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.NegativeHealthEvent);
        }
    }

    private static void SetBeauty(Pawn pawn, int degree)
    {
        if (degree > 2)
        {
            degree = 2;
        }

        if (degree < -2)
        {
            degree = -2;
        }

        if (pawn.story.traits.HasTrait(beauty))
        {
            PS_TraitChanger.Remove(pawn, pawn.story.traits.GetTrait(beauty));
        }

        if (degree != 0)
        {
            PS_TraitChanger.AddTrait(pawn, new Trait(beauty, degree));
        }
    }

    private static int GetCurrentBeauty(Pawn pawn)
    {
        return pawn.story.traits.DegreeOfTrait(beauty);
    }
}
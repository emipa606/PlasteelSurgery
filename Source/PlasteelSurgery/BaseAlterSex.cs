﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PlasteelSurgery;

public class BaseAlterSex : Recipe_InstallArtificialBodyPart
{
    public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
    {
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
            pawn.gender = pawn.gender == Gender.Male ? Gender.Female : Gender.Male;
            pawn.Drawer.renderer.SetAllGraphicsDirty();
            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort,
                    pawn.LabelShort, "PS_Messages_Surgery_SexChange".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.TaskCompletion);
        }
        else
        {
            Messages.Message(
                string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort,
                    pawn.LabelShort, "PS_Messages_Surgery_SexChange".Translate()), new LookTargets(pawn),
                MessageTypeDefOf.NegativeHealthEvent);
        }
    }
}
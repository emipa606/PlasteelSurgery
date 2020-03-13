using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class BaseAlterBody : Recipe_InstallArtificialBodyPart
    {
        protected virtual BodyTypeDef GetTargetBody(bool IsMale = false) { return BodyTypeDefOf.Thin; }
        
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            if (pawn?.story?.bodyType == null)
                yield break;

            var currentBody = GetCurrentBodyType(pawn);

            if (currentBody == GetTargetBody(IsMale: pawn.gender == Gender.Male))
                yield break;
            else
                yield return pawn.RaceProps.body.corePart;
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (!base.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[] {
                        billDoer,
                        pawn
                    });
                    pawn.story.bodyType = GetTargetBody(IsMale: pawn.gender == Gender.Male);
                    pawn.Drawer.renderer.graphics.ResolveAllGraphics();

                    //var hediff = HediffMaker.MakeHediff(PS_DefOf.PS_Hediff_HadPlasteelSurgery, pawn, part);
                    //pawn.health.AddHediff(hediff);
                    Messages.Message(string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort, pawn.LabelShort, "PS_Messages_Surgery_Body".Translate()), new LookTargets(pawn), MessageTypeDefOf.TaskCompletion);
                }
                else
                {
                    Messages.Message(string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort, pawn.LabelShort, "PS_Messages_Surgery_Body".Translate()), new LookTargets(pawn), MessageTypeDefOf.NegativeHealthEvent);

                }
            }
        }

        private static BodyTypeDef GetCurrentBodyType(Pawn pawn)
        {
            return pawn.story.bodyType;
        }
    }
}

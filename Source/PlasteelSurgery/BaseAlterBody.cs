using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class BaseAlterBody : Recipe_InstallArtificialBodyPart
    {
        protected virtual BodyTypeDef GetTargetBody(bool IsMale = false, BodyTypeDef bodyType = null)
        {
            if (bodyType != null)
            {
                return bodyType;
            }

            return IsMale ? BodyTypeDefOf.Male : BodyTypeDefOf.Female;
        }

        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            if (pawn?.story?.bodyType == null)
            {
                yield break;
            }

            if (recipe.GetModExtension<SurgeryDef>().gender != 0 &&
                pawn.gender != recipe.GetModExtension<SurgeryDef>().gender)
            {
                yield break;
            }

            var currentBody = GetCurrentBodyType(pawn);

            if (currentBody == GetTargetBody(pawn.gender == Gender.Male, recipe.GetModExtension<SurgeryDef>().bodyType))
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
                pawn.story.bodyType = GetTargetBody(pawn.gender == Gender.Male,
                    bill.recipe.GetModExtension<SurgeryDef>().bodyType);
                pawn.Drawer.renderer.graphics.ResolveAllGraphics();

                //var hediff = HediffMaker.MakeHediff(PS_DefOf.PS_Hediff_HadPlasteelSurgery, pawn, part);
                //pawn.health.AddHediff(hediff);
                Messages.Message(
                    string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort,
                        pawn.LabelShort, "PS_Messages_Surgery_Body".Translate()), new LookTargets(pawn),
                    MessageTypeDefOf.TaskCompletion);
            }
            else
            {
                Messages.Message(
                    string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort,
                        pawn.LabelShort, "PS_Messages_Surgery_Body".Translate()), new LookTargets(pawn),
                    MessageTypeDefOf.NegativeHealthEvent);
            }
        }

        private static BodyTypeDef GetCurrentBodyType(Pawn pawn)
        {
            return pawn.story.bodyType;
        }
    }
}
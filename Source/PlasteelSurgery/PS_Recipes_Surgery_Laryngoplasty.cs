using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class PS_Recipes_Surgery_Laryngoplasty : Recipe_Surgery
    {
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            var hasTrait = (pawn?.story?.traits?.allTraits?.Where(x => x.def == TraitDefOf.AnnoyingVoice || x.def == TraitDefOf.CreepyBreathing).Any() ?? false);
            var hasHediff = (pawn?.health?.hediffSet?.GetHediffs<Hediff>()?.Where(x => x.def == PS_DefOf.PS_HediffDefs_BotchedLaryngoplasty).Any() ?? false);

            Log.Message(string.Format("PlasteelSurgery: {0} HasTrait: {1} HasHediff: {2}", pawn.LabelShort, hasTrait, hasHediff));

            if(!hasTrait && !hasHediff) 
                yield break;
            
            var part = pawn.RaceProps?.body?.GetPartsWithDef(recipe.appliedOnFixedBodyParts.First())?.FirstOrDefault();
            if (part == null)
                yield break;

            yield return part;
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

                    var hediff = pawn?.health?.hediffSet?.GetHediffs<Hediff>()?.Where(x => x.def == PS_DefOf.PS_HediffDefs_BotchedLaryngoplasty).FirstOrDefault();
                    if (hediff != null)
                        pawn.health.RemoveHediff(hediff);
                    else if(pawn.story.traits.allTraits.Where(x => x.def == TraitDefOf.AnnoyingVoice).Any())
                        PS_TraitChanger.Remove(pawn, new Trait(TraitDefOf.AnnoyingVoice));
                    else if (pawn.story.traits.allTraits.Where(x => x.def == TraitDefOf.CreepyBreathing).Any())
                        PS_TraitChanger.Remove(pawn, new Trait(TraitDefOf.CreepyBreathing));
                    Messages.Message(string.Format("PS_Messages_SurgeryResult_Success".Translate(), billDoer.LabelShort, pawn.LabelShort, "PS_Messages_Surgery_Laryngoplasty".Translate()), new LookTargets(pawn), MessageTypeDefOf.TaskCompletion);
                }
                else
                {
                    var hasHediff = (pawn?.health?.hediffSet?.GetHediffs<Hediff>()?.Where(x => x.def == PS_DefOf.PS_HediffDefs_BotchedLaryngoplasty).Any() ?? false);
                    if (!hasHediff)
                    {
                        var hediff = HediffMaker.MakeHediff(PS_DefOf.PS_HediffDefs_BotchedLaryngoplasty, pawn, part);
                        pawn.health.AddHediff(hediff);
                    }
                    Messages.Message(string.Format("PS_Messages_SurgeryResult_Botched".Translate(), billDoer.LabelShort, pawn.LabelShort, "PS_Messages_Surgery_Laryngoplasty".Translate()), new LookTargets(pawn), MessageTypeDefOf.NegativeHealthEvent);
                }
            }
        }

        private static BodyTypeDef GetCurrentBodyType(Pawn pawn)
        {
            return pawn.story.bodyType;
        }
    }
}

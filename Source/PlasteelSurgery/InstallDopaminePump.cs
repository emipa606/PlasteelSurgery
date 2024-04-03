using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PlasteelSurgery;

public class InstallDopaminePump : Recipe_InstallArtificialBodyPart
{
    public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
    {
        var targetSkill = GetSkill(recipe.defName);
        var isAdvanced = recipe.defName.Contains("_Adv_");

        var brainPart = pawn.health.hediffSet.GetBrain();

        var currentPassion = pawn.skills.GetSkill(targetSkill).passion;

        if (currentPassion == Passion.None || isAdvanced && currentPassion == Passion.Minor)
        {
            yield return brainPart;
        }
    }

    public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients,
        Bill bill)
    {
        if (billDoer == null)
        {
            return;
        }

        Log.Message("Log 1");
        if (!CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
        {
            Log.Message("Log 2");
            var newImplant = ingredients
                .FirstOrDefault(x => x.def.thingCategories.Contains(ThingCategoryDef.Named("PS_Dopamine_Pumps"))
                                     || x.def.thingCategories.Contains(
                                         ThingCategoryDef.Named("PS_Dopamine_Pumps_Adv")));
            var newImplantName = newImplant?.def.defName;

            Log.Message("Log 3");
            var currentImplantHeDiff = pawn.health.hediffSet.hediffs.FirstOrDefault(x =>
                x.Part == part && x.def.defName.StartsWith("PS_Dopamine_Pump_"));
            if (currentImplantHeDiff != null)
            {
                ResetPassion(pawn, currentImplantHeDiff);
                if (currentImplantHeDiff.def.spawnThingOnRemoved != null)
                {
                    GenPlace.TryPlaceThing(ThingMaker.MakeThing(currentImplantHeDiff.def.spawnThingOnRemoved),
                        pawn.Position, pawn.Map, ThingPlaceMode.Near, out _);
                }
            }


            Log.Message("Log 4");
            AddPassion(pawn, newImplantName, part, GetSkill(newImplantName));

            Log.Message("Log 5");
            TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
        }
        else
        {
            Log.Message("Log 7 failed");
        }
    }

    private static void ResetPassion(Pawn pawn, Hediff currentImplantHeDiff)
    {
        var passionBoost = (int)currentImplantHeDiff.Severity;
        Log.Message($"Passion boost of removed implant: {passionBoost}");
        var wasAdvanced = currentImplantHeDiff.def.defName.Contains("_Adv_");
        var skill = GetSkill(currentImplantHeDiff.def.defName);
        switch (passionBoost)
        {
            //if(passionBoost == 0)
            //    //No Effect
            case 1:
                pawn.skills.GetSkill(skill).passion = wasAdvanced ? Passion.Minor : Passion.None;
                break;
            case 2:
                pawn.skills.GetSkill(skill).passion = Passion.None;
                break;
        }

        pawn.health.RemoveHediff(currentImplantHeDiff);
    }

    private static void AddPassion(Pawn pawn, string newImplantName, BodyPartRecord part, SkillDef skill)
    {
        var hediff = HediffDef.Named(newImplantName);
        var startingPassion = pawn.skills.GetSkill(GetSkill(newImplantName)).passion;
        var isAdvanced = newImplantName.Contains("_Adv_");
        switch (startingPassion)
        {
            case Passion.None:
                hediff.initialSeverity = isAdvanced ? 2 : 1;
                break;
            case Passion.Minor:
                hediff.initialSeverity = isAdvanced ? 1 : 0;
                break;
            case Passion.Major:
                hediff.initialSeverity = 0;
                break;
        }

        pawn.health.AddHediff(hediff, part);

        Passion newPassion;
        if (isAdvanced)
        {
            newPassion = Passion.Major;
        }
        else if (startingPassion == Passion.Major)
        {
            newPassion = Passion.Major;
        }
        else
        {
            newPassion = Passion.Minor;
        }

        pawn.skills.GetSkill(skill).passion = newPassion;
    }

    private static SkillDef GetSkill(string implantName)
    {
        if (implantName.EndsWith("Artistic"))
        {
            return SkillDefOf.Artistic;
        }

        if (implantName.EndsWith("Animals"))
        {
            return SkillDefOf.Animals;
        }

        if (implantName.EndsWith("Construction"))
        {
            return SkillDefOf.Construction;
        }

        if (implantName.EndsWith("Cooking"))
        {
            return SkillDefOf.Cooking;
        }

        if (implantName.EndsWith("Crafting"))
        {
            return SkillDefOf.Crafting;
        }

        if (implantName.EndsWith("Intellectual"))
        {
            return SkillDefOf.Intellectual;
        }

        if (implantName.EndsWith("Medicine"))
        {
            return SkillDefOf.Medicine;
        }

        if (implantName.EndsWith("Melee"))
        {
            return SkillDefOf.Melee;
        }

        if (implantName.EndsWith("Mining"))
        {
            return SkillDefOf.Mining;
        }

        if (implantName.EndsWith("Plants"))
        {
            return SkillDefOf.Plants;
        }

        if (implantName.EndsWith("Shooting"))
        {
            return SkillDefOf.Shooting;
        }

        if (implantName.EndsWith("Social"))
        {
            return SkillDefOf.Social;
        }

        throw new NotImplementedException($"Unknown implant type: {implantName}");
    }
}
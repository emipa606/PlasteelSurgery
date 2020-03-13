using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class MakeStandard : BaseAlterBody
    {
        protected override BodyTypeDef GetTargetBody(bool IsMale = false) { return IsMale ? BodyTypeDefOf.Male : BodyTypeDefOf.Female; }
    }
}

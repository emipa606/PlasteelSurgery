﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class MakeMale : BaseAlterBody
    {
        protected override BodyTypeDef GetTargetBody(bool IsMale = false) { return BodyTypeDefOf.Male; }
    }
}

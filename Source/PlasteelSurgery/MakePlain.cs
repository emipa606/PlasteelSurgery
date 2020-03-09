using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class MakePlain : BaseAlterBeauty
    {
        protected override int GetChange() { return 0; }
        protected override int GetFailBeauty()
        {
            var random = Rand.Value;
            if (random <= 0.5f)
                return 0;
            else if (random <= 0.9f)
                return -1;
            else
                return -2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class MakeUgly : BaseAlterBeauty
    {
        protected override int GetChange() { return -1; }
        protected override int GetFailBeauty()
        {
            var random = Rand.Value;
            if (random <= 0.8f)
                return 0;
            else
                return 1;
        }
    }
}

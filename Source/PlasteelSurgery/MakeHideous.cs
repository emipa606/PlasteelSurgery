using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PlasteelSurgery
{
    public class MakeHideous : BaseAlterBeauty
    {
        protected override int GetChange() { return -2; }
        protected override int GetFailBeauty()
        {
            var random = Rand.Value;
            if (random <= 0.8f)
                return 0;
            else if (random <= 0.95f)
                return 1;
            else
                return 2;
        }
    }
}

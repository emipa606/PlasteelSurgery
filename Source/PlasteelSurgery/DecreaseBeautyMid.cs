using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlasteelSurgery
{
    public class DecreaseBeautyMid : BaseAlterBeauty
    {
        protected override List<int> AllowedDegrees() { return new List<int> { -1, 2 }; }
        protected override int GetChange() { return -1; }
    }
}

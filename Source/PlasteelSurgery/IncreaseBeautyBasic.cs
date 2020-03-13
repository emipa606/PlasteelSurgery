using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlasteelSurgery
{
    public class IncreaseBeautyBasic : BaseAlterBeauty
    {
        protected override List<int> AllowedDegrees() { return new List<int> { -1 }; }
        protected override int GetChange() { return 1; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlasteelSurgery
{
    public class DecreaseBeautyBasic : BaseAlterBeauty
    {
        protected override List<int> AllowedDegrees() { return new List<int> { 0, 1 }; }
        protected override int GetChange() { return -1; }
    }
}

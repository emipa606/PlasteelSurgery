using System.Collections.Generic;

namespace PlasteelSurgery
{
    public class DecreaseBeautyBasic : BaseAlterBeauty
    {
        protected override List<int> AllowedDegrees()
        {
            return new List<int> {0, 1};
        }

        protected override int GetChange()
        {
            return -1;
        }
    }
}
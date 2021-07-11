using System.Collections.Generic;

namespace PlasteelSurgery
{
    public class IncreaseBeautyAdv : BaseAlterBeauty
    {
        protected override List<int> AllowedDegrees()
        {
            return new List<int> {-2, 1};
        }

        protected override int GetChange()
        {
            return 1;
        }
    }
}
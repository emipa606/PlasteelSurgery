using System.Collections.Generic;

namespace PlasteelSurgery;

public class IncreaseBeautyBasic : BaseAlterBeauty
{
    protected override List<int> AllowedDegrees()
    {
        return new List<int> { -1 };
    }

    protected override int GetChange()
    {
        return 1;
    }
}
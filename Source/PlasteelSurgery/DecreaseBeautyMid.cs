using System.Collections.Generic;

namespace PlasteelSurgery;

public class DecreaseBeautyMid : BaseAlterBeauty
{
    protected override List<int> AllowedDegrees()
    {
        return [-1, 2];
    }

    protected override int GetChange()
    {
        return -1;
    }
}
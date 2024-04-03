using System.Collections.Generic;

namespace PlasteelSurgery;

public class IncreaseBeautyMid : BaseAlterBeauty
{
    protected override List<int> AllowedDegrees()
    {
        return [0];
    }

    protected override int GetChange()
    {
        return 1;
    }
}
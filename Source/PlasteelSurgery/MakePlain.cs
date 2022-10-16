using Verse;

namespace PlasteelSurgery;

public class MakePlain : BaseAlterBeauty
{
    protected override int GetChange()
    {
        return 0;
    }

    protected override int GetFailBeauty()
    {
        var random = Rand.Value;
        switch (random)
        {
            case <= 0.5f:
                return 0;
            case <= 0.9f:
                return -1;
            default:
                return -2;
        }
    }
}
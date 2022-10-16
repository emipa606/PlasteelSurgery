using Verse;

namespace PlasteelSurgery;

public class MakePretty : BaseAlterBeauty
{
    protected override int GetChange()
    {
        return 1;
    }

    protected override int GetFailBeauty()
    {
        var random = Rand.Value;
        switch (random)
        {
            case <= 0.3f:
                return 0;
            case <= 0.9f:
                return -1;
            default:
                return -2;
        }
    }
}
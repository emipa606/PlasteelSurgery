using Verse;

namespace PlasteelSurgery
{
    public class MakeHideous : BaseAlterBeauty
    {
        protected override int GetChange()
        {
            return -2;
        }

        protected override int GetFailBeauty()
        {
            var random = Rand.Value;
            switch (random)
            {
                case <= 0.8f:
                    return 0;
                case <= 0.95f:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}
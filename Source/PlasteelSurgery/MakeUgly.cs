using Verse;

namespace PlasteelSurgery
{
    public class MakeUgly : BaseAlterBeauty
    {
        protected override int GetChange()
        {
            return -1;
        }

        protected override int GetFailBeauty()
        {
            var random = Rand.Value;
            return random <= 0.8f ? 0 : 1;
        }
    }
}
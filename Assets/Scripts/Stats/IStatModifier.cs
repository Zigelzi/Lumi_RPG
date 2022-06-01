using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IStatModifier
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);
        IEnumerable<float> GetPercentageModifier(Stat stat);
    }
}

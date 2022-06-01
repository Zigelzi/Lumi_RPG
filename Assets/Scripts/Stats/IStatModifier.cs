using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IStatModifier
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
    }
}

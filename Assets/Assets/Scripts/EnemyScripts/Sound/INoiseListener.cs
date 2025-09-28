using UnityEngine;

namespace Game
{
    public interface INoiseListener
    {
        void OnNoiseHeard(NoiseInfo noise);
    }
}


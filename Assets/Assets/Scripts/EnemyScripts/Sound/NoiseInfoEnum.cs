using UnityEngine;

namespace Game
{
    public enum NoiseType
    {
        Common,
        ImportantNoise
    }
    public struct NoiseInfo
    {
        public GameObject owner;
        public NoiseType type;
        public Vector3 position;
        public float Radius;
    }
}


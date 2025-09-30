using UnityEngine;

namespace Game
{
        public enum AttackType
        {
            Common,
            Lethal
        }
        public struct AttackInfo
        {
            public GameObject owner;
            public AttackType type;
            public Vector3 position;
            public float amount;    
            public float Radius;
        }
    
}


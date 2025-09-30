using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class NoiseSystem : MonoBehaviour
    {
        [SerializeField]
        LayerMask CharactersLayers = Physics.DefaultRaycastLayers;

        public static NoiseSystem Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

       
        public void MakeNoise(NoiseInfo noise)
        {
            var colliders = Physics.OverlapSphere(noise.position, noise.Radius, CharactersLayers, QueryTriggerInteraction.Ignore);

            foreach( var collider in colliders)
            {
                INoiseListener listener = collider.GetComponentInParent<INoiseListener>();
                listener?.OnNoiseHeard(noise);
            }
        }

    }

   
}


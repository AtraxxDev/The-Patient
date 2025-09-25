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

        public void MakeNoise(NoiseInfo noise)
        {
            var colliders = Physics.OverlapSphere(noise.position, noise.Radius, CharactersLayers, QueryTriggerInteraction.Ignore);

            foreach( var collider in colliders)
            {
                NoiseListenerInferface listener = collider.GetComponentInParent<NoiseListenerInferface>();
                listener?.OnNoiseHeard(noise);
            }
        }
        void Awake()
        {
            Instance = this;
        }
    }

   
}


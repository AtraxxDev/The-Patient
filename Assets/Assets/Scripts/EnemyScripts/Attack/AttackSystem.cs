using UnityEngine;

namespace Game
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField]
        LayerMask CharactersLayers = Physics.DefaultRaycastLayers;

        public static AttackSystem Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        public void MakeAttack(AttackInfo attack)
        {
            var colliders = Physics.OverlapSphere(attack.position, attack.Radius, CharactersLayers, QueryTriggerInteraction.Ignore);

            foreach (var collider in colliders)
            {
                IAttackListener listener = collider.GetComponentInParent<IAttackListener>();
                listener?.OnAttacked(attack);
            }
        }
    }
}


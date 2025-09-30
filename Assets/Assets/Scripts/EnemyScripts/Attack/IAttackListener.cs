using UnityEngine;

namespace Game
{
    public interface IAttackListener
    {
        void OnAttacked(AttackInfo noise);
    }
}

using Game;
using UnityEngine;

public class AttackMaker : MonoBehaviour
{
    public float damageAmount = 10f;
    public AttackType type = AttackType.Common;

    public void MakeAttack(AttackInfo attackInfo)
    {
        attackInfo.owner = gameObject;

       AttackSystem.Instance?.MakeAttack(attackInfo);
    }

    public void Debuggin(string debugtext)
    {
        Debug.Log(debugtext);
    }
}

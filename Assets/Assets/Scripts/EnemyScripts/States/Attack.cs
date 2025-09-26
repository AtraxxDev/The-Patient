using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource attackScream;
    
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        attackScream = _npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        AudioClip attackClip= npc.GetComponent<AIBaseEnemy>().GetAudio(1);
        agent.isStopped = true;
        attackScream.clip = attackClip;
        attackScream.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float Angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        //base.Update();
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isShooting");
        attackScream.Stop();
        base.Exit();
    }
}

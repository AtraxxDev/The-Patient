using Game;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource attackScream;
    CreateAttack aTTACK;


    
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        attackScream = _npc.GetComponent<AudioSource>();
        aTTACK = _npc.GetComponent<CreateAttack>();
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        AudioClip attackClip= npc.GetComponent<AIBaseEnemy>().GetScreamAudio(1);
        agent.isStopped = true;
        attackScream.clip = attackClip;
        attackScream.Play();
        aTTACK.MakeAttack(new Game.NoiseInfo
        {
            position = npc.transform.position,
            Radius = 6f,
            type = NoiseType.Common
        });
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

using Game;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource attackScream;
   AttackMaker attackMaker;


    
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        attackScream = _npc.GetComponent<AudioSource>();
        attackMaker=_npc.GetComponent<AttackMaker>();
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
      
        AudioClip attackClip= npc.GetComponent<AIBaseEnemy>().GetScreamAudio(1);
        agent.isStopped = true;
        attackScream.clip = attackClip;
        attackScream.Play();
        attackMaker.Debuggin("Entra el ataque");
       

        base.Enter();
    }

    public override void Update()
    {
        attackMaker.Debuggin("Ando buscando ataque");
        Vector3 direction = player.position - npc.transform.position;
        float Angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        if (Random.Range(0, 100) < 10)
        {
            attackMaker.Debuggin("Te ataque");
            attackMaker.MakeAttack(new AttackInfo
            {
                position = npc.transform.position + npc.transform.forward,
                Radius = 2f,
                type = AttackType.Common,
                amount=40f
            });
            
        }
    

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

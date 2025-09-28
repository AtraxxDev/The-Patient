using UnityEngine;
using UnityEngine.AI;

public class InvestigateSoundState : State
{
    NPCSenses npcSense;

    public InvestigateSoundState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        agent.speed = 2.5f;
        name = STATE.INVESTIGATESOUND;
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        npcSense=npc.GetComponent<NPCSenses>();
      
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(npcSense.NoisePosition);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            if (CanSeePlayer())
            {
                nextState=new PursueState(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                if (Random.Range(0, 500) < 10)
                {
                    nextState = new Patrol(npc, agent, anim, player);
                    stage = EVENT.EXIT;
                }
                   
            }
        }
        else
        {
            if (Random.Range(0, 500) < 10)
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        //base.Update();
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isShooting");        
        base.Exit();
    }
}

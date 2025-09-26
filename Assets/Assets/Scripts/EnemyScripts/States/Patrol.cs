
using Game;
using UnityEngine;

public class Patrol : State
{
    int currentIndex = -1;


    public Patrol(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        npc.GetComponent<NPCSenses>();

        float lastDistance = Mathf.Infinity;
        for (int i = 0; i < CheckCheckpoints.Singleton.CheckpointsObjects.Count; i++)
        {
            GameObject thisWP = CheckCheckpoints.Singleton.CheckpointsObjects[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDistance)
            {
                currentIndex = i - 1;
                lastDistance = distance;
            }
        }
        // anim.SetTrigger("isWalking");
       
        base.Enter();
    }

    public override void Update()
    {
        //UpdateHearing();

        if (agent.remainingDistance < 1)
        {
            if(Random.Range(0, 500) < 10)
            {
                if (currentIndex >= CheckCheckpoints.Singleton.CheckpointsObjects.Count - 1)
                    currentIndex = 0;
                else
                    currentIndex++;

                agent.SetDestination(CheckCheckpoints.Singleton.CheckpointsObjects[currentIndex].transform.position);
            }
            
        }

        if (CanSeePlayer())
        {
            nextState = new PursueState(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

        if (HesRightBehindMe())
        {
            nextState = new Scared(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

       

    }

    public override void Exit()
    {
        //anim.ResetTrigger("isWalking");
        base.Exit();
    }
}

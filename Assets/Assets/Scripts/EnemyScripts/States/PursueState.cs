using UnityEngine;
using UnityEngine.AI;

public class PursueState:State
{
    AudioSource pursueScream;
    AudioSource stepsSound;

    public PursueState(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
      base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
        var aSources = _npc.GetComponents<AudioSource>();
        pursueScream = aSources[0];
        stepsSound = aSources[1];
    }

    public override void Enter()
    {
        // anim.SetTrigger("isRunning");
        npc.GetComponent<NPCSenses>();

        AudioClip attackClip = npc.GetComponent<AIBaseEnemy>().GetScreamAudio(0);        
        pursueScream.clip = attackClip;
        pursueScream.Play();


        AudioClip[] listofSteps = npc.GetComponent<AIBaseEnemy>().GetStepsAudioList();
        AudioClip stepsClips = npc.GetComponent<AIBaseEnemy>().GetStepsAudio(Random.Range(0, listofSteps.Length));
        stepsSound.clip = stepsClips;
        stepsSound.pitch = 1.5f;
        stepsSound.Play();
        

        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new InvestigateSoundState(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

    }

    public override void Exit()
    {
        //anim.ResetTrigger("isRunning");
        pursueScream.Stop();
        stepsSound.pitch = 1f;
        base.Exit();
    }
}

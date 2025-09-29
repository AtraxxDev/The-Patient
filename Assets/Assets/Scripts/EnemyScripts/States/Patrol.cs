
using Game;
using UnityEngine;

public class Patrol : State
{
    int currentIndex = -1;
    AudioSource normalSounds;
    AudioSource stepsSound;
    AudioClip attackClip;

    public Patrol(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player) :
        base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 2f;
        agent.isStopped = false;

        var aSources = _npc.GetComponents<AudioSource>();
        normalSounds = aSources[0];
        stepsSound = aSources[1];
      
        npc.GetComponent<NPCSenses>();
    }

    public override void Enter()
    {
        attackClip = npc.GetComponent<AIBaseEnemy>().GetScreamAudio(3);
        normalSounds.clip = attackClip;
        normalSounds.Stop();

        AudioClip[] listofSteps = npc.GetComponent<AIBaseEnemy>().GetStepsAudioList();
        AudioClip stepsClips = npc.GetComponent<AIBaseEnemy>().GetStepsAudio(Random.Range(0,listofSteps.Length));
        stepsSound.clip = stepsClips;
        stepsSound.Play();


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
            stepsSound.Stop();
            if(Random.Range(0, 500) < 10)
            {
                if (currentIndex >= CheckCheckpoints.Singleton.CheckpointsObjects.Count - 1)
                    currentIndex = 0;
                else
                    currentIndex++;

                agent.SetDestination(CheckCheckpoints.Singleton.CheckpointsObjects[currentIndex].transform.position);
                stepsSound.Play();
            }
            
        }

        if (HeardSomething())
        {
            nextState = new InvestigateSoundState(npc, agent, anim, player);
            stage = EVENT.EXIT;
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

        if (Random.Range(0, 5000) < 10)
        {
            normalSounds.PlayOneShot(attackClip);
            
        }


    }

    public override void Exit()
    {
        //anim.ResetTrigger("isWalking");
        base.Exit();
    }
}

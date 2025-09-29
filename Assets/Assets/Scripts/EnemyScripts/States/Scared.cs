using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static State;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class Scared : State
{
    AudioSource scaredScream;
    AudioSource stepsSound;

    public Scared(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
            base(_npc, _agent, _anim, _player)
    {
        name = STATE.SCARED;
        var aSources = _npc.GetComponents<AudioSource>();
        scaredScream = aSources[0];
        stepsSound = aSources[1];
    }

    public override void Enter()
    {
    //  anim.SetTrigger("isRunning");
        MakeSounds();


        agent.speed = 6;
        agent.isStopped = false;
        Transform safeZonetransform = CheckCheckpoints.Singleton.GetSafeZone.transform;
        agent.SetDestination(safeZonetransform.position);
        base.Enter();
    }

    public override void Update()
    {

        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        //   anim.ResetTrigger("isRunning");
        stepsSound.pitch = 1f;
        base.Exit();
    }

    private void MakeSounds()
    {
        AudioClip attackClip = npc.GetComponent<AIBaseEnemy>().GetScreamAudio(2);
        scaredScream.clip = attackClip;
        scaredScream.Play();

        AudioClip[] listofSteps = npc.GetComponent<AIBaseEnemy>().GetStepsAudioList();
        AudioClip stepsClips = npc.GetComponent<AIBaseEnemy>().GetStepsAudio(Random.Range(0, listofSteps.Length));
        stepsSound.clip = stepsClips;
        stepsSound.pitch = 1.5f;
        stepsSound.Play();
    }
}

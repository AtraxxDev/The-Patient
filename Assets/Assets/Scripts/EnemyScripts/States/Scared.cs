using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static State;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class Scared : State
{ 
        public Scared(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) :
            base(_npc, _agent, _anim, _player)
        {
        name = STATE.SCARED;

        }

        public override void Enter()
        {
          //  anim.SetTrigger("isRunning");
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
            base.Exit();
        }
}

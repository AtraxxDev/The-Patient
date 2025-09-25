using UnityEngine;
using UnityEngine.AI;

public class State 
{
    //Defino que estados van a existir en el enum
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, SCARED
    };

    //Cada estado debe de tener una manera de entrar, lo que va a pasar y una manera de salir
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    float visDist = 10.0f;
    float sisDist = 3.0f;
    float visAngle = 45.0f;
    float sisAngle = 120.0f;
    float attackDist = 2.0f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;

    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;


    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < attackDist)
        {
            return true;
        }
        return false;
    }

    public bool HesRightBehindMe()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(npc.transform.forward, direction);

        if (direction.magnitude < sisDist && angle > sisAngle)
        {
            return true;
        }
        return false;
    }
}

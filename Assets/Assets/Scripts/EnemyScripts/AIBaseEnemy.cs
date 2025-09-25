using UnityEngine;
using UnityEngine.AI;

public class AIBaseEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    State currentState;

    [SerializeField] AudioClip[] _audioClip; //Clip1 es audio de perseguir, Clip 2 de ataque
    //[SerializeField] AudioClip attackClip;

   // [SerializeField] private bool 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        currentState = new Idle(this.gameObject, agent, anim, player);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

    public AudioClip GetAudio(int i)
    {
        return _audioClip[i];
    }
}

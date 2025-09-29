using Game;

using UnityEngine;
using UnityEngine.AI;

public class AIBaseEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    State currentState;

    [SerializeField] AudioClip[] _audioClip; //Clip1 es audio de perseguir, Clip 2 de ataque,Clip 3, CLip 4 de grito al huir
    [SerializeField] AudioClip[] _stepsClip; //Lista de audios de pasos que se van a estar ciclando

    

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

    

    public AudioClip GetScreamAudio(int i)
    {
        return _audioClip[i];
    }

    public AudioClip[] GetStepsAudioList()
    {
        return _stepsClip;
    }
    public AudioClip GetStepsAudio(int i)
    {
        return _stepsClip[i];
    }
}

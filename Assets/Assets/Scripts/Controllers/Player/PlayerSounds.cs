using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource playerScreamAudioSource;
    [SerializeField] private AudioSource playerStepsAudioSource;

    [SerializeField] AudioClip[] _audioClip; //Clip1 es audio de perseguir, Clip 2 de ataque
    [SerializeField] AudioClip[] _stepsClip; //Lista de audios de pasos que se van a estar ciclando

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

    public AudioSource ReturnPlayerScream()
    {
        return playerScreamAudioSource;
    }

    public AudioSource ReturnPlayerStepsAudio()
    {
        return playerStepsAudioSource;
    }
}

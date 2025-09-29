using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Title("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Title("Music Clips")]
    public List<NamedAudioClip> musicClips;

    [Title("SFX Clips")]
    public List<NamedAudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudio()
    {
        musicDict = new Dictionary<string, AudioClip>();
        sfxDict = new Dictionary<string, AudioClip>();

        foreach (var item in musicClips)
        {
            if (!musicDict.ContainsKey(item.name))
                musicDict[item.name] = item.clip;
        }

        foreach (var item in sfxClips)
        {
            if (!sfxDict.ContainsKey(item.name))
                sfxDict[item.name] = item.clip;
        }

        Debug.Log("AudioManager: Audio inicializado");
    }

    public static AudioManager GetInstance()
    {
        if (Instance == null)
            Debug.LogError("AudioManager no encontrado en la escena!");
        return Instance;
    }

    // ======= Métodos de reproducción =======
    public void PlayMusic(string name, bool loop = true)
    {
        if (!musicDict.TryGetValue(name, out var clip) || clip == null)
        {
            Debug.LogWarning($"Music clip '{name}' no encontrado");
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        if (!sfxDict.TryGetValue(name, out var clip) || clip == null)
        {
            Debug.LogWarning($"SFX clip '{name}' no encontrado");
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }
}

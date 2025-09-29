using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;


// Script encargado de contener todas las pistas de audio por nombre y audioclip
[CreateAssetMenu(fileName = "AudioDataSO", menuName = "Scriptable Objects/AudioDataSO")]
public class AudioDataSO : ScriptableObject
{
    [Title("Music Clips")]
    [InfoBox("Here are all the music audios")]
    public List<NamedAudioClip> musicClips;

    [Title("SFX Clips")]
    [InfoBox("Here are all the audios of the sound effects")]
    public List<NamedAudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    private bool isInitialized = false;


    // Inicializa los diccionarios y asigna todos los clips de audio con su nombre
    public void Initialize()
    {
        if (isInitialized) return;

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

        isInitialized = true;
        Debug.Log("Se inicializo el AudioDtaSO");
    }

    public AudioClip GetMusicClip(string name)
    {
        if (!isInitialized) Initialize();
        return musicDict.TryGetValue(name, out var clip) ? clip : null;
    }

    public AudioClip GetSFXClip(string name)
    {
        if (!isInitialized) Initialize();
        return sfxDict.TryGetValue(name, out var clip) ? clip : null;
    }

}


[System.Serializable]
public class NamedAudioClip
{
    public string name;
    public AudioClip clip;
}


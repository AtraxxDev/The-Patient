using Game;
using UnityEngine;

public class NoiseMaker:MonoBehaviour
{

    //Donde deber�a de poner esto?
    public void MakeNoise(NoiseInfo noiseInfo)
    {
        noiseInfo.owner = gameObject;

        NoiseSystem.Instance?.MakeNoise(noiseInfo);
    }
}

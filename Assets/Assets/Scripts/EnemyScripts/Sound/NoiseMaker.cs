using Game;
using UnityEngine;

public class NoiseMaker:MonoBehaviour
{

    //Donde debería de poner esto?
    public void MakeNoise(NoiseInfo noiseInfo)
    {
        noiseInfo.owner = this.gameObject;

        NoiseSystem.Instance?.MakeNoise(noiseInfo);

        //Debug.Log("Haciendo Sonido");
    }
}

using Game;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSenses : MonoBehaviour,INoiseListener
{
    public bool NoiseHeard { get; private set; } = false;

    public Vector3 NoisePosition { get; private set; }
    public NoiseType NoiseType { get; private set; } = NoiseType.Common;

    public float NoiseTime { get; private set; } = 0f;

    public float TimeSinceHeardNoise => Time.time - NoiseTime;
    public float EntryExpirationTime = 1.5f;

    [Header("Hearing")]
    [SerializeField, Multiline] string hearingDebug = "";
    [SerializeField] Color noisedGizmosColor = Color.magenta;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateHearing();
    }
    public void OnNoiseHeard(NoiseInfo noise)
    {
        if (noise.owner == gameObject)
        {
            return;
        }
        NoiseHeard = true;
        NoiseTime = Time.time;
        NoiseType = noise.type;

        if (NavMesh.SamplePosition(noise.position, out NavMeshHit hit, 4f, NavMesh.AllAreas))
        {
            NoisePosition = hit.position;
        }
        else
        {
            NoisePosition = noise.position;
        }
    }

    public void ForgetNoise()
    {
        NoiseHeard = false;
    }

    private void OnDrawGizmosSelected()
    {
        DrawNoiseGizmos();
    }

    void DrawNoiseGizmos()
    {
        if(NoiseHeard==false) {
            return;
        }
        Gizmos.color = noisedGizmosColor;
        Gizmos.DrawSphere(NoisePosition, 0.2f);


    }

    void UpdateHearing()
    {
        if (NoiseHeard == false)
        {
            hearingDebug = "NONE";
            return;
        }

        hearingDebug = $"Heard Noise {Mathf.RoundToInt(TimeSinceHeardNoise)} s ago.\n\r";
        hearingDebug += $" Type={NoiseType}";

       // Debug.Log("Te Escuche");

        if (TimeSinceHeardNoise >= EntryExpirationTime)
        {
            ForgetNoise();
        }
    }

    
}

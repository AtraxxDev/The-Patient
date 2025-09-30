using Game;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class NPCSenses : MonoBehaviour,INoiseListener,IAttackListener
{
    [Header("DamageSense")]
    public bool DamageFelt { get; private set; } = false;

    public Vector3 DamagePosition { get; private set; }
    public AttackType DamageType { get; private set; } = AttackType.Common;

    public float DamageTime { get; private set; } = 0f;

    public float TimeSinceDamageFelt => Time.time - DamageTime;
    public float EntryIFramesTime = 0.75f;

    [Header("Damage")]
    [SerializeField, Multiline] string damageDebug = "";
    [SerializeField] Color damageGizmosColor = Color.tomato;

    [Header("HearingSenses")]
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
        UpdateDamage();
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

    public void OnAttacked(AttackInfo attack)
    {
        if (attack.owner == gameObject)
        {
            return;
        }
        DamageFelt = true;
        DamageTime = Time.time;
        DamageType = attack.type;

       var playerDeath=GetComponent<PlayerDeath>();
        {
            playerDeath.YouAreDead();
        }

        if (NavMesh.SamplePosition(attack.position, out NavMeshHit hit, 4f, NavMesh.AllAreas))
        {
            NoisePosition = hit.position;
        }
        else
        {
            NoisePosition = attack.position;
        }
    }

    public void ForgetNoise()
    {
        NoiseHeard = false;
    }

    public void ForgetDamage()
    {
        DamageFelt = false;
    }

    private void OnDrawGizmosSelected()
    {
        DrawNoiseGizmos();
        DrawAttackGizmos();
    }

    void DrawNoiseGizmos()
    {
        if(NoiseHeard==false) {
            return;
        }
        Gizmos.color = noisedGizmosColor;
        Gizmos.DrawSphere(NoisePosition, 0.2f);


    }

    void DrawAttackGizmos()
    {
        if (DamageFelt == false)
        {
            return;
        }

        Gizmos.color = noisedGizmosColor;
        Gizmos.DrawSphere(DamagePosition, 0.2f);
    }

    public void UpdateDamage()
    {
        if(DamageFelt == false)
        {
            damageDebug = "none";
            return;
        }

        damageDebug=$"Damage felt auch {Mathf.RoundToInt(TimeSinceDamageFelt)} s ago.\n\r";
        damageDebug += $" Type={DamageType}";

        if (TimeSinceDamageFelt >= EntryIFramesTime)
        {
            ForgetDamage();
        }
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

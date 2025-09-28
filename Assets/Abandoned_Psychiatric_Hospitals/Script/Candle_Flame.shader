Shader "AE/HDRP_CandleFlame_Merged"
{
    Properties
    {
        _Textures("Textures", 2D) = "white" {}
        _Textures1("Textures1", 2D) = "white" {}
        _Noise("Noise", 2D) = "white" {}
        _BrightnessMultiplier("Brightness Multiplier", Range(0,15)) = 2.5
        _FlameFlickerSpeed("Flame Flicker Speed", Float) = 0.13
        _NoiseScale("Noise Scale", Float) = 0.44
        _CoreColour("Core Colour", Color) = (0.9,0.78,0.36,1)
        _OuterColour("Outer Colour", Color) = (0.79,0.29,0.17,1)
        _BaseColour("Base Colour", Color) = (0.16,0.28,0.78,1)
        _FakeGlow("Fake Glow", Range(0,1)) = 0.4
    }

    HLSLINCLUDE
    #pragma vertex vert
    #pragma fragment frag
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

    CBUFFER_START(UnityPerFrame)
        float4x4 unity_MatrixVP;
        float4 _Time;
    CBUFFER_END

    struct Attributes
    {
        float3 positionOS : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    TEXTURE2D(_Textures);
    SAMPLER(sampler_Textures);
    TEXTURE2D(_Textures1);
    SAMPLER(sampler_Textures1);
    TEXTURE2D(_Noise);
    SAMPLER(sampler_Noise);

    float _BrightnessMultiplier;
    float4 _CoreColour, _OuterColour, _BaseColour;
    float _FlameFlickerSpeed, _NoiseScale, _FakeGlow;

    Varyings vert(Attributes v)
    {
        Varyings o;
        // Use only local mesh position for vertex transform
        o.positionCS = mul(unity_MatrixVP, float4(v.positionOS, 1.0));
        o.uv = v.uv;
        return o;
    }

    float4 frag(Varyings i) : SV_Target
    {
        // FIX: All flames share the same noise coordinates (centered at 0.5,0.5)
    float2 worldUV = i.uv + float2(0.5, 0.5) - 0.5; // keeps UVs roughly correct

        float2 flicker = worldUV * _NoiseScale;
        flicker += -_FlameFlickerSpeed * _Time.y;

        float4 noise = SAMPLE_TEXTURE2D(_Noise, sampler_Noise, flicker);
        float2 distortedUV = i.uv + i.uv.y * noise.rg * i.uv.x * i.uv.y;

        float4 tex = SAMPLE_TEXTURE2D(_Textures, sampler_Textures, distortedUV);
        float4 tex1 = SAMPLE_TEXTURE2D(_Textures1, sampler_Textures1, i.uv);

        float3 emission = (_CoreColour.rgb * tex.r) +
                          (_OuterColour.rgb * tex.g) +
                          (_BaseColour.rgb * tex.b) +
                          (_CoreColour.rgb * _FakeGlow * tex1.a);

        emission *= _BrightnessMultiplier;

        return float4(emission, tex1.a);
    }
    ENDHLSL

    SubShader
    {
        Tags { "RenderPipeline"="HDRenderPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            Name "ForwardUnlit"
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDHLSL
        }
    }
}

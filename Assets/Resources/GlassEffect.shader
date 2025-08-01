Shader "Custom/URP_GlassEnhanced"
{
    Properties
    {
        _GlassColor("Glass Color", Color) = (0.8, 0.95, 1, 0.3)
        _RefractionStrength("Refraction Strength", Range(0, 0.5)) = 0.07
        _Smoothness("Smoothness", Range(0, 5)) = 0.9
        _Metallic("Metallic", Range(0, 5)) = 0.0
        _SpecularColor("Specular Color", Color) = (1, 1, 1, 1)
        _FresnelPower("Fresnel Power", Range(0.5, 20)) = 4
        _IOR("Index of Refraction", Range(1.0, 1.5)) = 1.02
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 300

        Pass
        {
            Name "GlassPass"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };

            float4 _GlassColor;
            float _RefractionStrength;
            float _Smoothness;
            float _Metallic;
            float4 _SpecularColor;
            float _FresnelPower;
            float _IOR;

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            Varyings vert(Attributes input)
            {
                Varyings o;
                float3 worldPos = TransformObjectToWorld(input.positionOS.xyz);
                o.worldPos = worldPos;
                o.normalWS = TransformObjectToWorldNormal(input.normalOS);
                o.viewDirWS = normalize(GetWorldSpaceViewDir(worldPos));
                o.screenPos = ComputeScreenPos(TransformWorldToHClip(worldPos));
                o.positionHCS = TransformWorldToHClip(worldPos);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float2 screenUV = i.screenPos.xy / i.screenPos.w;

                // Fresnel: edge glow
                float fresnel = pow(1.0 - saturate(dot(i.viewDirWS, i.normalWS)), _FresnelPower);

                // Refraction
                float eta = 1.0 / _IOR;
                float3 refracted = refract(i.viewDirWS, i.normalWS, eta);
                screenUV += refracted.xy * _RefractionStrength;

                float3 bgColor = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV).rgb;

                // Blinn-Phong highlight
                float3 lightDir = normalize(_MainLightPosition.xyz);
                float3 halfwayDir = normalize(i.viewDirWS + lightDir);
                float spec = pow(saturate(dot(i.normalWS, halfwayDir)), 64.0);
                float3 specular = _SpecularColor.rgb * spec * _Smoothness;

                // Combine base color + fresnel glow + specular
                float3 finalColor = lerp(bgColor, _GlassColor.rgb, _GlassColor.a);
                finalColor += fresnel * _SpecularColor.rgb;
                finalColor += specular;

                return float4(finalColor, _GlassColor.a);
            }

            ENDHLSL
        }
    }
    FallBack Off
}

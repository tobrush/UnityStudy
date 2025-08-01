Shader "Custom/URP_LiquidRealistic"
{
    Properties
    {
        _Color("Liquid Color", Color) = (0.2, 0.7, 1, 0.8)
        _FillHeight("Fill Height", Float) = 0.0
        _BobX("Bob X", Float) = 0.0
        _BobZ("Bob Z", Float) = 0.0

        _EdgeDarken("Edge Darken", Float) = 0.5
        _HighlightColor("Highlight Color", Color) = (1, 1, 1, 1)
        _RefractionStrength("Refraction Strength", Float) = 0.02
        _SpecularPower("Specular Power", Range(8, 128)) = 64
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Name "LiquidPass"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
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
                float3 localX : TEXCOORD2;
                float3 localZ : TEXCOORD3;
                float3 viewDirWS : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };

            float4 _Color;
            float4 _HighlightColor;
            float _FillHeight;
            float _BobX;
            float _BobZ;
            float _EdgeDarken;
            float _RefractionStrength;
            float _SpecularPower;

            Varyings vert(Attributes input)
            {
                Varyings o;
                float3 worldPos = TransformObjectToWorld(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);

                o.worldPos = worldPos;
                o.normalWS = normalWS;

                o.localX = TransformObjectToWorldDir(float3(1, 0, 0));
                o.localZ = TransformObjectToWorldDir(float3(0, 0, 1));

                float3 viewDirWS = GetWorldSpaceViewDir(worldPos);
                o.viewDirWS = normalize(viewDirWS);

                o.screenPos = ComputeScreenPos(TransformWorldToHClip(worldPos));
                o.positionHCS = TransformWorldToHClip(worldPos);
                return o;
            }

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            half4 frag(Varyings i) : SV_Target
            {
                // Wobble surface height
                float waveX = sin(_Time.y * 4 + dot(i.worldPos, i.localX) * 3.0) * _BobX;
                float waveZ = sin(_Time.y * 4 + dot(i.worldPos, i.localZ) * 3.0) * _BobZ;
                float surfaceHeight = _FillHeight + waveX + waveZ;

                float dist = i.worldPos.y - surfaceHeight;

                // Alpha fade near the surface
                float alpha = smoothstep(0.01, 0.0, dist);

                // Edge darkening
                float edge = smoothstep(0.02, 0.0, abs(dist));
                float3 edgeColor = lerp(_Color.rgb, _Color.rgb * _EdgeDarken, edge);

                // Specular highlight
                float3 reflectDir = reflect(-i.viewDirWS, i.normalWS);
                float spec = pow(saturate(dot(reflectDir, normalize(i.viewDirWS))), _SpecularPower);
                float3 specular = _HighlightColor.rgb * spec;

                // Refraction
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                screenUV += (i.normalWS.xy) * _RefractionStrength;
                float3 refractedCol = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV).rgb;

                // Final blend
                float3 finalColor = lerp(refractedCol, edgeColor, 0.6) + specular;
                return float4(finalColor, _Color.a * alpha);
            }

            ENDHLSL
        }
    }

    FallBack Off
}

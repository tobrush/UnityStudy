Shader "Custom/GlassWithBlurFeatherZigzag"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurStrength ("Blur Strength", Range(0, 5)) = 1
        _FeatherStrength ("Feather Strength", Range(0, 1)) = 0.5
        _ZigzagStrength ("Zigzag Strength", Range(0, 0.1)) = 0.02
        _Color ("Color Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize; // x=1/width, y=1/height

            float _BlurStrength;
            float _FeatherStrength;
            float _ZigzagStrength;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // 지그재그 노이즈 생성 (간단한 사인파 변위)
            float2 ZigzagOffset(float2 uv, float time)
            {
                float zigzagX = sin(uv.y * 50 + time * 10) * _ZigzagStrength;
                float zigzagY = cos(uv.x * 50 + time * 10) * _ZigzagStrength;
                return float2(zigzagX, zigzagY);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y;

                // UV에 지그재그 노이즈 오프셋 적용
                float2 uvZigzag = i.uv + ZigzagOffset(i.uv, time);

                // 간단한 4방향 블러 샘플링
                float2 texelSize = _MainTex_TexelSize.xy;
                fixed4 col = tex2D(_MainTex, uvZigzag) * 0.4;
                col += tex2D(_MainTex, uvZigzag + float2(texelSize.x, 0)) * 0.15 * _BlurStrength;
                col += tex2D(_MainTex, uvZigzag - float2(texelSize.x, 0)) * 0.15 * _BlurStrength;
                col += tex2D(_MainTex, uvZigzag + float2(0, texelSize.y)) * 0.15 * _BlurStrength;
                col += tex2D(_MainTex, uvZigzag - float2(0, texelSize.y)) * 0.15 * _BlurStrength;

                // 페더: UV 가장자리로 갈수록 알파 감소
                float feather = smoothstep(0.0, _FeatherStrength, min(min(i.uv.x, 1.0 - i.uv.x), min(i.uv.y, 1.0 - i.uv.y)));

                // 색상과 알파 조절
                col.rgb *= _Color.rgb;
                col.a *= feather * _Color.a;

                return col;
            }
            ENDCG
        }
    }
}

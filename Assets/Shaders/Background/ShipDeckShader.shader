Shader "Background/ShipDeckShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TexWidth ("Texture Width", int) = 256
        _TexHeight ("Texture Height", int) = 224
        _SkyTop ("Sky Top Section Row", int) = 20
        _SkyMiddle ("Sky Middle Section Row", int) = 48
        _SkyBottom ("Sky Bottom Section Row", int) = 112
        _SkySpeed ("Sky Scroll Speed", float) = 800.0
        _WaterAmplitude ("Water Wave Amplitude", float) = 1
        _WaterFrequency ("Water Wave Frequency", float) = 1
        _WaterSpeed ("Water Wave Drift Speed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            int _TexWidth;
            int _TexHeight;

            int _SkyTop;
            int _SkyMiddle;
            int _SkyBottom;
            float _SkySpeed;

            float _WaterAmplitude;
            float _WaterFrequency;
            float _WaterSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Convert the UV coordinate to a pixel coordinate
                // Floor both coordinates, and offset Y coordinate, so the number starts from top to bottom
                // like old CRT screens
                float2 pixel = float2(floor(i.uv.x * _TexWidth), floor(_TexHeight - i.uv.y * _TexHeight));
                float2 offset = float2(0, 0);

                // Compute Sky offsets
                if (pixel.y < _SkyTop)
                {
                    offset.x = _Time * _SkySpeed;
                }
                if (pixel.y >= _SkyTop && pixel.y < _SkyMiddle)
                {
                    offset.x = _Time * _SkySpeed / 2;
                }
                if (pixel.y >= _SkyMiddle && pixel.y < _SkyBottom)
                {
                    offset.x = _Time * _SkySpeed / 8;
                }

                // Compute Water offsets
                if (pixel.y >= _SkyBottom)
                {
                    float _Amplitude = _WaterAmplitude;
                    float _Speed = _WaterSpeed * 0.001;
                    offset.x = _Amplitude * sin(pixel.y * _Time * _WaterFrequency) + pow(pixel.y, 2) * _Time * _Speed;
                }

                offset.x = floor(offset.x) / _TexWidth;
                
                // Apply offsets
                float2 uv = i.uv + offset;

                // Sample the texture with the modified UV coordinates
                fixed4 col = tex2D(_MainTex, uv);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
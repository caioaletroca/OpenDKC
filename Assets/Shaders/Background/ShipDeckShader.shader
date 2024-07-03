Shader "Background/ShipDeckShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SkyTop ("Sky Top Section Row", int) = 20
        _SkyMiddle ("Sky Middle Section Row", int) = 48
        _SkyBottom ("Sky Bottom Section Row", int) = 112
        _Speed ("Scroll Speed", float) = 1.0
        _SpeedFactor ("Speed Factor", float) = 1.0
        _TexHeight ("Texture Height", int) = 224
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

            int _TexHeight;
            int _SkyTop;
            int _SkyMiddle;
            int _SkyBottom;

            float _Speed;
            float _SpeedFactor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Convert the UV y-coordinate to a pixel coordinate
                float yPixel = i.uv.y * _TexHeight;

                float shiftAmount = floor(_Time * 2);

                float scrollOffset = 0.0;

                // Compute Sky offsets
                if (yPixel > _TexHeight - _SkyTop)
                {
                    scrollOffset = _SpeedFactor * _Time * _Speed;
                }
                if (yPixel <= _TexHeight - _SkyTop && yPixel > _TexHeight - _SkyMiddle)
                {
                    scrollOffset = _SpeedFactor * _Time * _Speed / 2;
                }
                if (yPixel <= _TexHeight - _SkyMiddle && yPixel > _TexHeight - _SkyBottom)
                {
                    scrollOffset = _SpeedFactor * _Time * _Speed / 4;
                }

                // TODO: Make water effect
                
                float2 uv = i.uv + float2(scrollOffset, 0);

                // Sample the texture with the modified UV coordinates
                fixed4 col = tex2D(_MainTex, uv);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
Shader"Water"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}
         _FoamTex("Foam", 2D) = "white" {}
        _BumpMap("Normalmap", 2D) = "bump" {}
        _Tint("Color Tint", Color) = (1,1,1,1)
        _Freq("Frequency", Range(0,5)) = 3
        _Speed("Speed",Range(0,100)) = 10
        _Amp("Amplitude", Range(0,1)) = 0.5
        _ScrollX("Scroll X", Range(-5,-5)) = 1
        _ScrollY("Scroll Y", Range(-5,-5)) = 1
        _BumpScale("Scale", Range(1,20)) = 1
        
    }
    
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        float _ScrollX;
        float _ScrollY;
        struct Input
        {
            float2 uv_MainTex;
            float3 vertColor;
        };

        float4 _Tint;
        float _Freq;
        float _Speed;
        float _Amp;
        sampler2D _FoamTex;
        sampler2D _BumpMap;
        float _BumpScale;

        struct appdata
        {
            float4 vertex: POSITION;
            float3 normal: NORMAL;
             float4 tangent : TANGENT;
            float4 texcoord: TEXCOORD0;
            float4 texcoord1: TEXCOORD1;
            float4 texcoord2: TEXCOORD2;
        };

        void vert (inout appdata v, out Input o)
            {
                UNITY_INITIALIZE_OUTPUT(Input,o);
            float t = _Time * _Speed;
            float waveHeight = sin(t + v.vertex.x * _Freq) * _Amp + sin (t*2 + v.vertex.x * _Freq * 2) * _Amp;
            v.vertex.y = v.vertex.y + waveHeight;
            v.normal = normalize(float3(v.normal.x + waveHeight, v.normal.y, v.normal.z));
            o.vertColor = waveHeight + 2;
            }

        sampler2D _MainTex;
        void surf (Input IN, inout SurfaceOutput o)
        {
            _ScrollX *= _Time;
            _ScrollY *= _Time;
            float3 foam = (tex2D (_MainTex, IN.uv_MainTex + float2(_ScrollX/2.0, _ScrollY/2.0)));
            float3 water = (tex2D (_MainTex, IN.uv_MainTex + float2(_ScrollX, _ScrollY))).rgb;
            float3 bumpTex = UnpackNormal(tex2D(_BumpMap,IN.uv_MainTex));
            o.Normal = bumpTex * _BumpScale;
            float4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c * IN.vertColor.rgb * tex2D(_MainTex,water);
        }
        ENDCG
    }
    Fallback    "Diffuse"
}
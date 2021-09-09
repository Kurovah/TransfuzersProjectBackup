Shader "Custom/TerrainBase"
{
    Properties
    {

    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        const static int macColCount = 10;
        const static float e = 1E-4;


        int colCount;
        float3 colors[macColCount];
        float baseHeights[macColCount];
        float blends[macColCount];

        float minHeight;
        float maxHeight;

        sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
        };

        float InverseLerp(float a, float b, float val)
        {
            return saturate((val - a) / (b - a));
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float hPerc = InverseLerp(minHeight, maxHeight, IN.worldPos.y);
            //o.Albedo = hPerc;
            for (int i = 0; i < colCount; i++)
            {
                float drawStrength = InverseLerp(-blends[i] / 2 - e, blends[i] / 2 , hPerc - baseHeights[i]); // how far above the start height the current pixel is
                o.Albedo = o.Albedo * (1 - drawStrength) + colors[i] * drawStrength;
            }

        }
        ENDCG
    }
    FallBack "Diffuse"
}

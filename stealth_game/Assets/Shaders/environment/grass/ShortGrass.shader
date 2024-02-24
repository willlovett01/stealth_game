Shader "Custom/ShortGrass"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _TipColor("Tip Color", Color) = (1, 1, 1, 1)
        _BladeTexture("Blade Texture", 2D) = "white" {}

        _BladeWidthMin("Blade Width (Min)", Range(0, 0.1)) = 0.02
        _BladeWidthMax("Blade Width (Max)", Range(0, 0.1)) = 0.05
        _BladeHeightMix("Blade Height (Min)", Range(0, 2)) = 0.1
        _BladeHeightMax("Blade Height (Max)", Range(0, 2)) = 0.2

        _BladeSegments("Blade Segments", Range(1, 10)) = 3
        _BladeBendDistance("Blade Forward Amount", Float) = 0.38
        _BladeBendCurve("Blade Curvature Amount", Range(1, 4)) = 2

        _BendDelta("Bend Variation", Range(0, 1)) = 0.2

        _TessellationGrassDistance("Tessellation Grass Distance", Range(0.01, 2)) = 0.1

        _GrassMap("Grass Visibility Map", 2D) = "white" {}
        _GrassThreshold("Grass Visibility Threshold", Range(-0.1, 1)) = 0.5
        _GrassFalloff("Grass Visibility Fade-In Falloff", Range(0, 0.5)) = 0.05

        _WindMap("Wind Offset Map", 2D) = "bump" {}
        _WindVelocity("Wind Velocity", Vector) = (1,0,0,0)
        _WindFrequency("Wind Frequency", Range(0,1)) = 0.01

    }
    SubShader
    {

        Tags {
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
            "RenderPipelind" = "UniversalPipeline"
        }
        LOD 100
        Cull off

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _BaseColor;
            float4 _TipColor;
            sampler2D _BladeTexture;

            float _BladeWidthMin;
            float _BladeWidthMax;
            float _BladeHeightMix;
            float _BladeHeightMax;

            float _BladeBendDistance;
            float _BladeBendCurve;

            float _BendDelta;

            float _TessellationGrassDistance;

            sampler2D _GrassMap;
            float4 _GrassMap_ST;
            float _GrassThreshold;
            float _GrassFalloff;

            sampler2D _WindMap;
            float4 _WindMap_ST;
            float4 _WindVelocity;
            float _WindFrequency;

            float4 _ShadowColor;
        CBUFFER_END

        struct VertexInput {
            float4 vertex  : POSITION;
            float3 normal  : NORMAL;
            float4 tangent : TANGENT;
            float2 uv      : TEXCOORD0;
        };

        struct VertexOutput {
            float4 vertex  : SV_POSITION;
            float3 normal  : NORMAL;
            float4 tangent : TANGENT;
            float2 uv      : TEXCOORD0;
        };

        struct GeomData {
            float4 pos : SV_POSITION;
            float2 uv  : TEXCOORD0;
            float3 worldPos : TEXCOORD1;
        };

        // Vertex shader which translates from object to world space.
        VertexOutput geomVert(VertexInput v) {
            
            VertexOutput o;
            o.vertex = float4(TransformObjectToWorld(v.vertex), 1.0f);
            o.normal = TransformObjectToWorldNormal(v.normal);
            o.tangent = v.tangent;
            o.uv = TRANSFORM_TEX(v.uv, _GrassMap);
            return o;
        }

        // This function applies a transformation (during the geometry shader),
        // converting to clip space in the process
        GeomData TransformGeomToClip(float3 pos, float3 offset, float3x3 transformationMatrix, float2 uv) {
            GeomData o;

            o.pos = TransformObjectToHClip(pos + offset);
            o.uv = uv;
            o.worldPos = TransformObjectToWorld(pos + mul(transformationMatrix, offset));

            return o;
        }


        [maxvertexcount(3)]
        void geom(point VertexOutput input[1], inout TriangleStream<GeomData> triStream){
            
            float3 pos = input[0].vertex.xyz;
            float3 normal = input[0].normal;
            float4 tangent = input[0].tangent;


            float3x3 transformationMatrix = float3x3 (
                1, 0, 0,
                0, 1, 0,
                0, 0 ,1
            );

            triStream.Append(TransformGeomToClip(pos, float3(0, 0, 0), transformationMatrix, float2(0.0f, 0.0f)));
            triStream.Append(TransformGeomToClip(pos, float3(0, 0, 0), transformationMatrix, float2(1.0f, 0.0f)));
            triStream.Append(TransformGeomToClip(pos, float3(0, 0, 0), transformationMatrix, float2(0.5f, 1.0f)));

            triStream.RestartStrip();

        }


        ENDHLSL

        pass{
            Name "GrassPass"
            Tags{ "LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex geomVert
            #pragma fragment frag
            #pragma require geometry
            #pragma geometry geom

            float4 frag(GeomData i) : SV_Target {
                float4 color = lerp(_BaseColor, _TipColor, i.uv.y);

                return color;
            }


        ENDHLSL


        }
    }
}


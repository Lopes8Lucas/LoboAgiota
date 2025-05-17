Shader "UI/StencilWrite"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Lighting Off
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil {
            Ref 1
            Comp Always
            Pass Replace
        }

        Pass {
            ColorMask 0 // Don�ft render color, just stencil
        }
    }
}

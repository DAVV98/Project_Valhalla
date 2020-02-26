// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// setup borrowed from: https://www.youtube.com/watch?v=00qMZlacZQo
// shader code borrowed from: https://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse

Shader "Outlined/Silhouette Only" {
	Properties{
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 5.0)) = .005
	}

		CGINCLUDE
#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v) {
		// just make a copy of incoming vertex data but scaled according to normal direction
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

		SubShader{
			Tags { "Queue" = "Transparent" }

			Pass {
				Name "BASE"
				Cull Back
				Blend Zero One

		// uncomment this to hide inner details:
		//Offset -8, -8

		SetTexture[_OutlineColor] {
			ConstantColor(0,0,0,0)
			Combine constant
		}
	}

		// note that a vertex shader is specified here but its using the one above
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front

		// you can choose what kind of blending mode you want for the outline
		//Blend SrcAlpha OneMinusSrcAlpha // Normal
		//Blend One One // Additive
		Blend One OneMinusDstColor // Soft Additive
		//Blend DstColor Zero // Multiplicative
		//Blend DstColor SrcColor // 2x Multiplicative

CGPROGRAM
#pragma vertex vert
#pragma fragment frag

half4 frag(v2f i) :COLOR {
	return i.color;
}
ENDCG
		}


	}

		Fallback "Diffuse"
}

//Shader "Custom/OutlineShader"
//{
//    Properties
//    {
//        _Color ("Color", Color) = (1,1,1,1)
//        _MainTex ("Albedo (RGB)", 2D) = "white" {}
//        _Glossiness ("Smoothness", Range(0,1)) = 0.5
//        _Metallic ("Metallic", Range(0,1)) = 0.0
//    }
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//        LOD 200
//
//        CGPROGRAM
//        // Physically based Standard lighting model, and enable shadows on all light types
//        #pragma surface surf Standard fullforwardshadows
//
//        // Use shader model 3.0 target, to get nicer looking lighting
//        #pragma target 3.0
//
//        sampler2D _MainTex;
//
//        struct Input
//        {
//            float2 uv_MainTex;
//        };
//
//        half _Glossiness;
//        half _Metallic;
//        fixed4 _Color;
//
//        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//        // #pragma instancing_options assumeuniformscaling
//        UNITY_INSTANCING_BUFFER_START(Props)
//            // put more per-instance properties here
//        UNITY_INSTANCING_BUFFER_END(Props)
//
//        void surf (Input IN, inout SurfaceOutputStandard o)
//        {
//            // Albedo comes from a texture tinted by color
//            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//            o.Albedo = c.rgb;
//            // Metallic and smoothness come from slider variables
//            o.Metallic = _Metallic;
//            o.Smoothness = _Glossiness;
//            o.Alpha = c.a;
//        }
//        ENDCG
//    }
//    FallBack "Diffuse"
//}

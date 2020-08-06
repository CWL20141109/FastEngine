// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OutlineNew" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_OutlineColor ("Outline Color", Color) = (0.08,0.06,0.04,1)
		_Outline("Thick of Outline",range(0.0,0.1))=0.02
		_Factor("Factor",range(0,1))=0.5
		_LightVector("Light Vector", Vector) = (1,1,1,0)
		_LightPower("Light Power, 0~3", Float) = 1

	}
	SubShader {
		Tags { "Queue"="AlphaTest+1" "IgnoreProjector"="True" "RenderType"="Opaque" }
		LOD 500

		Stencil{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		// outline pass
		pass{
		Tags{"LightMode"="Always"}
		Cull Front
		ZWrite On
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		float _Outline;
		float _Factor;
		float4 _OutlineColor;

		struct v2f {
			float4 pos:SV_POSITION;
		};

		v2f vert (appdata_full v) {
			v2f o;
			float3 dir=normalize(v.vertex.xyz);
			float3 dir2=v.normal;
			float D=dot(dir,dir2);
			dir=dir*sign(D);
			dir=dir*_Factor+dir2*(1-_Factor);
			v.vertex.xyz+=dir*_Outline;
			o.pos=UnityObjectToClipPos(v.vertex);
			return o;
		}
		float4 frag(v2f i):COLOR
		{
			float4 c = _OutlineColor / 5;
			return c;
		}
		ENDCG
		}
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BladeGs fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_DiffuseTex;
			float3 viewDir;
			INTERNAL_DATA
		};

		fixed4 _Color;
		fixed _Glossiness;
		fixed _FresnelPower;
		fixed _FresnelBias;
		half _Shininess;
		fixed4 _LightVector;
		fixed _LightPower;

		fixed4 _ReflectColor;
		fixed4 _SpecularColor;

		struct SurfaceOutputGs 
{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Specular;
			half Gloss;
			half3 Reflect;
			half Fresnel;
			fixed Alpha;
			fixed3 SpecularColor;
			fixed LightPower;
			fixed3 lightDir;
		};

		inline fixed4 LightingBladeGs(SurfaceOutputGs s, fixed3 lightDir, half3 viewDir)
		{

			fixed diff = max(0, dot(s.Normal, s.lightDir));
			fixed4 c;
			c.rgb = s.Albedo * diff * s.LightPower;
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutputGs o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 diffuseValue = tex2D(_MainTex, IN.uv_DiffuseTex);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = c.a;

			o.SpecularColor = _SpecularColor.rgb;
			o.Alpha = diffuseValue.a;
			o.LightPower = _LightPower;
			o.lightDir = _LightVector.xyz;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

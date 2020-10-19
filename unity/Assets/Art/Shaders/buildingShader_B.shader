// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "buildingShader_B"
{
	Properties
	{
		_MainTexture("MainTexture", 2D) = "white" {}
		_MainColor("MainColor", Color) = (0,0,0,0)
		_Texture0("Texture 0", 2D) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv2_texcoord2;
			float2 uv_texcoord;
		};

		uniform sampler2D _Texture0;
		uniform float4 _Texture0_ST;
		uniform float4 _MainColor;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color23 = IsGammaSpace() ? float4(0.4056604,0.135858,0.3485868,0) : float4(0.13687,0.01651578,0.09964208,0);
			float2 uv2_Texture0 = i.uv2_texcoord2 * _Texture0_ST.xy + _Texture0_ST.zw;
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float4 MainColor50 = ( ( 1.2 * max( color23 , tex2D( _Texture0, uv2_Texture0 ) ) ) * ( ( 1.3 * _MainColor ) * tex2D( _MainTexture, uv_MainTexture ) ) );
			o.Albedo = MainColor50.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
-1913;14;1906;1021;1699.113;1016.671;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;5;-1176.302,-586.9134;Inherit;False;1597.613;833.3835;Diffuse;12;50;22;41;40;19;16;3;1;4;2;30;18;Diffuse;0,0.5019608,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;17;-1020.918,-810.6923;Inherit;False;563.4572;248.6216;StyleCtrl;2;15;23;StyleCtrl;0.02595115,1,0,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;18;-1130.001,-399.3048;Inherit;True;Property;_Texture0;Texture 0;2;0;Create;True;0;0;False;0;None;05e6eb3fd9c318d4da26c7c05e336e99;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ColorNode;23;-716.806,-758.6345;Inherit;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0.4056604,0.135858,0.3485868,0;0.8178759,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-842.2482,-397.598;Inherit;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;False;0;None;None;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-992.4679,-729.5699;Inherit;False;Constant;_DiffColorLighting;DiffColorLighting;3;0;Create;True;0;0;False;0;1.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-784.5021,-143.556;Inherit;False;Property;_MainColor;MainColor;1;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;4;-1122.409,26.4309;Inherit;True;Property;_MainTexture;MainTexture;0;0;Create;True;0;0;False;0;None;ec514a08fe0f7ef40a8651585b6e3b48;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-540.8237,-527.577;Inherit;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;1.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;30;-446.0233,-377.9689;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-831.5898,25.8451;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-496.5546,-160.3801;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-356.3237,-523.7766;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-190.4007,8.15538;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;8.673706,-151.2747;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;180.5499,-155.3113;Inherit;False;MainColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;62;-1021.15,402.2363;Inherit;False;1590.106;406.9174;OutLine;7;60;53;58;57;52;55;54;OutLine;0.4150943,0,0.3745508,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;51;659.8682,-272.7005;Inherit;False;50;MainColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OutlineNode;57;-147.0952,487.0528;Inherit;False;0;True;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-370.4095,492.09;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;52;-672.033,462.6806;Inherit;False;50;MainColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;53;-963.4568,540.8942;Inherit;False;Constant;_OutLineColor;OutLineColor;3;0;Create;True;0;0;False;0;0.5058824,0.4039216,0.6313726,0;0.5061797,0.4025009,0.6320754,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;59;658.1241,2.457602;Inherit;False;58;OutLinaCus;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;54;-694.8618,540.7819;Inherit;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-701.1697,632.4191;Inherit;False;Constant;_OutLineWidth;OutLineWidth;4;0;Create;True;0;0;False;0;0.015;0.04;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;58;106.4419,487.0527;Inherit;False;OutLinaCus;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;919.8972,-267.2523;Float;False;True;2;ASEMaterialInspector;0;0;Standard;buildingShader_B;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0.5754717,0.3175952,0.3175952,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;18;0
WireConnection;30;0;23;0
WireConnection;30;1;19;0
WireConnection;1;0;4;0
WireConnection;16;0;15;0
WireConnection;16;1;2;0
WireConnection;41;0;40;0
WireConnection;41;1;30;0
WireConnection;3;0;16;0
WireConnection;3;1;1;0
WireConnection;22;0;41;0
WireConnection;22;1;3;0
WireConnection;50;0;22;0
WireConnection;57;0;55;0
WireConnection;57;1;60;0
WireConnection;55;0;52;0
WireConnection;55;1;54;0
WireConnection;54;0;53;0
WireConnection;58;0;57;0
WireConnection;0;0;51;0
ASEEND*/
//CHKSM=C927E6F2E0AA48528DAEC8CD63B1ED8FD3EF592B
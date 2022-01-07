﻿Shader "FogOfWar/FogOfWar"
{
	Properties
	{

		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
	}
	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};
	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;

	v2f vert (appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}
			
	fixed4 frag (v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv);
		return col;
	}
	ENDCG

	SubShader
	{
		Tags{ "Queue" = "Geometry+1" }
			ZTest NotEqual // 포그 플레인이 항상 화면 앞에 그려지게 함
			Blend SrcAlpha OneMinusSrcAlpha
			Color[_Color]
			Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}

	}
}

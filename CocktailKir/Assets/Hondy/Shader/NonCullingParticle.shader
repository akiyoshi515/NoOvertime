Shader "Particle/NonCullingParticle" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader
	{

	Tags
	{
		"RenderType" = "Transparent"
		"Queue" = "Transparent" 	  
		"LightMode" = "ForwardBase"
		"Projector" = "True"
	}
		Cull OFF
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On
		

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag	  
#pragma target 2.0							 
#pragma multi_compile_fwdbase
#include "UnityCG.cginc"

		sampler2D _MainTex;
	fixed4 _Color;

	struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
	};

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color * _Color;
#ifdef PIXELSNAP_ON
		OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

		return OUT;
	}


	fixed4 frag(v2f IN) : SV_Target
	{
		fixed4 c = tex2D(_MainTex, IN.texcoord);
		return c;
	}
	ENDCG
	}
}
}

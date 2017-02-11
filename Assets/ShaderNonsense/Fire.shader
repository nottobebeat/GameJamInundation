Shader "Shader Pack/Fire" {
Properties
{
	_MainTex ("Main Texture", 2D) = "white" {}
	_NoiseTex("Noise Texture", 2D) = "white" {}
}

Category
{
	Tags { "Queue"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	ZWrite Off
	
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			sampler2D_float _CameraDepthTexture;
			float _InvFade;

			fixed4 frag (v2f i) : SV_Target
			{		
				fixed4 col = 2.0f * i.color * tex2D(_MainTex, i.texcoord) * tex2D(_NoiseTex, float2(i.texcoord.x, i.texcoord.y -= _Time.x*30));
				return col;
			}
			ENDCG 
		}
	}	
}
}
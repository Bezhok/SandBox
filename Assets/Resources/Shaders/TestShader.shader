// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShader"
{
    Properties {
        _Tint("Tint", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Pass {
        CGPROGRAM
        
        #pragma vertex VertexProgram
        #pragma fragment FragmentProgram
        #include "UnityCG.cginc"
        float4 _Tint;
        sampler2D _MainTex;
        float4 _MainTex_ST;
        
        struct Data {
			float4 position : SV_POSITION;
			float2 uv : TEXCOORD0;
			float3 loc : loc;
		};
			
		struct InputData {
		    float4 position : POSITION;
		    float2 uv : TEXCOORD0;
		};
		
        Data VertexProgram(InputData inp) {
            Data d;
       
            d.loc = mul(UNITY_MATRIX_M, float4(0,0,0,1)).xyz;
            d.position = UnityObjectToClipPos(inp.position);
            d.uv = inp.uv* _MainTex_ST.xy + _MainTex_ST.zw;
            
            return d;
        }
        
        float4 FragmentProgram(Data d) : SV_TARGET {
            //return float4(d.uv, 1, 1);
            return tex2D(_MainTex, d.uv)*_Tint;
        }
        
        ENDCG
        }
    }
}

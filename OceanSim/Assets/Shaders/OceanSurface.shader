Shader "Unlit/OceanSurface"
{
	Properties
	{
		_HighColor("High Color", Color) = (1,1,1,1)
		_HighColorHeight("High Color Height", Float) = 5.0
		_LowColor("Low Color", Color) = (1,1,1,1)
		_LowColorHeight("Low Color Height", Float) = -1.0
		_ColorGain("Color Gain", Float) = 1.0
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		//_BumpMap("Bumpmap", 2D) = "bump" {}
		//_BumpIntensity("Bumpmap intensity", Range(-2,2)) = 1.0
		//_Glossiness("Smoothness", Range(0,1)) = 0.5
		//_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _LowColor;
			fixed4 _HighColor;
			float _LowColorHeight;
			float _HighColorHeight;
			float _ColorGain;

			//Used by vert function
			int _WaveCount;
			float _WaveTime;
			float _Amplitude[20];
			float _DirectionX[20];
			float _DirectionZ[20];
			float _Q[20];
			float _Frequency[20];
			float _PhaseConstant[20];

			struct WaveResult {
				float3 pos;
				float3 normal;
				float2 uv;
			};

			WaveResult getWaveResult(float3 pos) {
				float3 wavePosition = float3(pos.x, 0.0, pos.z);
				float3 waveNormal = float3(0.0, 1.0, 0.0);

				for (int i = 0; i < _WaveCount; i++) {
					float2 direction = float2(_DirectionX[i], _DirectionZ[i]);
					float constant = dot(direction, float2(pos.x, pos.z)) * _Frequency[i] + _WaveTime * _PhaseConstant[i];
					float wa = _Frequency[i] * _Amplitude[i];
					float s = sin(constant);
					float c = cos(constant);

					float3 iWavePos =
						float3(
							_Q[i] * _Amplitude[i] * direction.x * c,
							_Amplitude[i] * s,
							_Q[i] * _Amplitude[i] * direction.y * c
							);

					float3 iNormal =
						float3(
							direction.x * wa * c,
							_Q[i] * wa * s,
							direction.y * wa * c
							);

					wavePosition += iWavePos;
					waveNormal -= iNormal;

				}


				WaveResult result;
				result.pos = wavePosition;
				result.normal = waveNormal;


				return result;
			}
			
			v2f vert (appdata v)
			{
				float4 pos = v.vertex;
				float4 worldPos = mul(unity_ObjectToWorld, pos);
				WaveResult result = getWaveResult(float3(worldPos.x, pos.y, worldPos.z));
				float3 withWave = result.pos;
				float4 localPos = mul(unity_WorldToObject, float4(withWave, worldPos.w));
				float4 movedVertex = float4(localPos.x, withWave.y, localPos.z, localPos.w);
				float2 newUv = float2(-worldPos.z, worldPos.x) / 20;
				v2f o;
				o.vertex = UnityObjectToClipPos(movedVertex);
				o.normal = result.normal;
				o.uv = TRANSFORM_TEX(newUv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

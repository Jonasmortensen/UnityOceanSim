﻿Shader "Custom/GerstnerSurface" {
	Properties {
		_HighColor ("High Color", Color) = (1,1,1,1)
		_HighColorHeight("High Color Height", Float) = 5.0
		_LowColor ("Low Color", Color) = (1,1,1,1)
		_LowColorHeight("Low Color Height", Float) = -1.0
		_ColorGain("Color Gain", Float) = 1.0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_BumpIntensity("Bumpmap intensity", Range(-2,2)) = 1.0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags {"RenderType"="Opaque" }


		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard vertex:vert addshadow
		#pragma surface surf Standard vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 worldPos;
		};

		//Used by surf function
		half _Glossiness;
		half _Metallic;
		half _BumpIntensity;
		fixed4 _LowColor;
		fixed4 _HighColor;
		float _LowColorHeight;
		float _HighColorHeight;
		float _ColorGain;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

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

		void vert(inout appdata_full IN) {
			float4 pos = IN.vertex;
			//Get the global position of the vertex
			float4 worldPos = mul(unity_ObjectToWorld, pos);

			//Manipulate the position

			WaveResult result = getWaveResult(float3(worldPos.x, pos.y, worldPos.z));

			float3 withWave = result.pos;

			//Convert the position back to local
			float4 localPos = mul(unity_WorldToObject, float4(withWave, worldPos.w));

			//Assign the modified vertex
			IN.vertex = float4(localPos.x, withWave.y, localPos.z, localPos.w);
			IN.normal = result.normal;
			//World tiling
			IN.texcoord = float4(-worldPos.z, worldPos.x, 0.0, 0.0) / 20;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//float lowPos = -1.0;
			float height = _HighColorHeight - _LowColorHeight;
			float heightSaturation = clamp((IN.worldPos.y - _LowColorHeight) / height, 0.0, 1.0);
			float gainedSaturation = ((heightSaturation - 0.5) * _ColorGain) + 0.5;
			fixed4 gradC = lerp(_LowColor, _HighColor, gainedSaturation);


			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * gradC;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 0.8f;
			o.Normal = lerp(UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)), fixed3(0, 0, 1), -_BumpIntensity + 1);
		}
		ENDCG
	}
	FallBack "Diffuse"
}

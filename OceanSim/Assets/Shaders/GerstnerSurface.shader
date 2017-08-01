﻿Shader "Custom/GerstnerSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		float _WaveTime;
		float _Amplitude;
		float _DirectionX;
		float _DirectionZ;
		float _Q;
		float _Frequency;
		float _PhaseConstant;

		struct WaveResult {
			float3 pos;
			float3 normal;
		};

		WaveResult getWaveResult(float3 pos) {
			//float amplitude = 1.0;
			//float waveLength = 4.0;
			//float speed = 3.0;
			//float2 direction = float2(-1.0, 0.0);
			//float frequency = 2.0 / waveLength;
			//float phaseConstant = speed * frequency;
			float2 direction = float2(_DirectionX, _DirectionZ);
			float constant = dot(direction, float2(pos.x, pos.z)) * _Frequency + _WaveTime * _PhaseConstant;

			float wa = _Frequency * _Amplitude;
			float s = sin(constant);
			float c = cos(constant);
			//float q = 1.7;

			float3 wavedPos =
				float3(
					pos.x + _Q * _Amplitude * direction.x * c,
					_Amplitude * s,
					pos.z + _Q * _Amplitude * direction.y * c
					);

			float3 normal = float3(-direction.x * wa * c, 1 - _Q * wa * s, -direction.y * wa * c);

			pos.y = _Amplitude * sin(constant);
			WaveResult result;
			result.pos = wavedPos;
			result.normal = normal;


			return result;
		}

		void vert(inout appdata_full IN) {
			//Get the global position of the vertex
			float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

			//Manipulate the position

			WaveResult result = getWaveResult(worldPos.xyz);

			float3 withWave = result.pos;

			//Convert the position back to local
			float4 localPos = mul(unity_WorldToObject, float4(withWave, worldPos.w));

			//Assign the modified vertex
			IN.vertex = localPos;
			IN.normal = result.normal;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
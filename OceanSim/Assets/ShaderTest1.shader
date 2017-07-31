// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ShaderTest1" {
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

		float _WaterTime;

		float3 getWavePos(float3 pos) {
			float amplitude = 5.0;
			float waveLength = 10.0;
			float speed = 1.0;
			//float directionX = 1.0;
			float2 direction = float2(1.0, 0.0);
			float frequency = 2.0 / waveLength;
			float phaseConstant = speed * frequency;
			float constant = dot(direction, float2(pos.x, pos.z)) * frequency + _WaterTime * phaseConstant;
			pos.y = amplitude * sin(constant);

			return pos;
		}

		void vert(inout appdata_full IN) {
			//Get the global position of the vertex
			float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

			//Manipulate the position
			float3 withWave = getWavePos(worldPos.xyz);

			//Convert the position back to local
			float4 localPos = mul(unity_WorldToObject, float4(withWave, worldPos.w));

			//Assign the modified vertex
			IN.vertex = localPos;
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

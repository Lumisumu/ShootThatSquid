﻿// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

Shader "Ocean/Shape/Whirlpool"
{
	Properties
	{
		_Amplitude( "Amplitude", float ) = 1
		_Radius( "Radius", float) = 3
	}

	Category
	{
		// base simulation runs on the Geometry queue, before this shader.
		// this shader adds interaction forces on top of the simulation result.
		Tags { "Queue"="Transparent" }

		SubShader
		{
			Pass
			{
				Name "BASE"
				Tags { "LightMode" = "Always" }
				Blend One One

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "UnityCG.cginc"
				#include "../../../Crest/Shaders/Shapes/MultiscaleShape.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					float2 worldOffsetScaled : TEXCOORD0;
					float2 uv : TEXCOORD1;
				};

				uniform float _EyeRadius;
				uniform float _Radius;
				uniform float _Swirl;
				uniform float _MaxSpeed;

				v2f vert( appdata_t v )
				{
					v2f o;
					o.vertex = UnityObjectToClipPos( v.vertex );

					float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
					float3 centerPos = unity_ObjectToWorld._m03_m13_m23;
					o.worldOffsetScaled.xy = worldPos.xz - centerPos.xz;

					// shape is symmetric around center with known radius - fix the vert positions to perfectly wrap the shape.
					o.worldOffsetScaled.xy = sign(o.worldOffsetScaled.xy);
					float4 newWorldPos = float4(centerPos, 1.);
					newWorldPos.xz += o.worldOffsetScaled.xy * _Radius;
					o.vertex = mul(UNITY_MATRIX_VP, newWorldPos);
					o.uv = v.texcoord;

					return o;
				}

				uniform float _Amplitude;

				float2 frag (v2f i) : SV_Target
				{
					float2 col = float2(0, 0);

					float2 uv_from_cent = (i.uv - float2(.5, .5)) * 2.;

					float r       =           .1; // eye of whirlpool radius
					const float R =            1; // whirlpool radius
					float2 o      = float2(0, 0); // origin
					float  s      =       1; //_Swirl; // whirlpool 'swirlyness', can vary from 0 - 1
					float2 p      = uv_from_cent; // our current position
					float  V      =        70.0; //_MaxSpeed; // maximum whirlpool speed

					float2 PtO  =       o - p;    // vector from position to origin
					float  lPtO = length(PtO);

					if(lPtO >= R) {
						col = float2(0,0);
					} else if (lPtO <= r) {
						col = float2(0,0);
					} else {
						float c = 1.0 - ((lPtO - r) / (R - r));
						// dynamically calvulate current value of velocity field
						// (TODO: Make this a texture lookup?)
						float2 v = V * c * normalize(
							(s * c * normalize(float2(-PtO.y, PtO.x))) +
							((s - 1.0) * (c - 1.0) * normalize(PtO))
						);
						col = v;
					}
					return col;
				}

				ENDCG
			}
		}
	}
}

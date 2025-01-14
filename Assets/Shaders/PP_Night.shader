﻿Shader "Hidden/PP_Night"
{
	HLSLINCLUDE

#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_CameraGBufferTexture3, sampler_CameraGBufferTexture3);
	TEXTURE2D_SAMPLER2D(_CameraGBufferTexture0, sampler_CameraGBufferTexture0);
	//NOTE: If you're super keen to optimize, change all these floats to fixeds.
	float4 _MainTex_ST;
	float4 _NVColor;
	float4 _TargetWhiteColor;
	float _BaseLightingContribution;
	float _LightSensitivityMultiplier;

	float4 Frag(VaryingsDefault i) : SV_Target
	{
		float4 col = SAMPLE_TEXTURE2D(_CameraGBufferTexture3, sampler_CameraGBufferTexture3, i.texcoord);
		float4 dfse = SAMPLE_TEXTURE2D(_CameraGBufferTexture0, sampler_CameraGBufferTexture0, i.texcoord);			
				
		//Get the luminance of the pixel				
		float lumc = dot(col.rgb, float3(0.299f, 0.587f, 0.114f));
		lumc = smoothstep(0.0005, 0.1, lumc);
				
		//return float4(lumc, lumc, lumc, 1);
		
		//Desat + green the image
		//col = dot(col, _NVColor);	
		dfse = dfse * _NVColor;
		//Make bright areas/lights too bright
		
		
		//Add some of the regular diffuse texture based off how bright each pixel is
		col = col * _NVColor;
		col.rgb = lerp(col.rgb, dfse.rgb, lumc+_BaseLightingContribution);
		col.rgb = lerp(col.rgb, _TargetWhiteColor.rgb, lumc * _LightSensitivityMultiplier);
		//col.rgb = mul(_NVColor.rgb, col.rgb);
		//Increase the brightness of all normal areas by a certain amount
		//col.rb = max (col.r - 0.75, 0)*4;
		return col;
	}

	ENDHLSL

	SubShader
	{
	Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}

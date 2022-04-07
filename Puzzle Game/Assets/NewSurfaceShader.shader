// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Background/CrazyBackground"
{
	Properties
	{
		_MainTex("-- Primary Texture --", 2D) = "white" {}
		_BlendTex("-- Secondary Texture --", 2D) = "white" {}

		_MainTintColor("Blend: Primary Tint", Color) = (1,1,1,1)
		_BlendTintColor("Blend: Secondary Tint", Color) = (1,1,1,1)

		_BlendMode("Blend: Mode", int) = 0
		_BlendWarp("Blend: Warp Factor", float) = 0.25
		_BlendStay("Blend: Stay Factor", float) = 1
		_BlendTimeScaling("Blend: Time Scaling", float) = 20

		_TextureSize("Texture Size", int) = 256

		_AmplitudeX("Distort: Amplitude X", float) = 16.0
		_FrequencyX("Distort: Frequency X", float) = 0.1
		_CompressionX("Distort: Compression X", float) = 1.0
		_TimeScalingX("Distort: Time Scaling X", float) = 32

		_AmplitudeY("Distort: Amplitude Y", float) = 16.0
		_FrequencyY("Distort: Frequency Y", float) = 0.1
		_CompressionY("Distort: Compression Y", float) = 1.0
		_TimeScalingY("Distort: Time Scaling Y", float) = 32

		_DistortX("Distort: X Mode", int) = 0
		_DistortY("Distort: Y Mode", int) = 0
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				// Generic stuff
				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				// Important stuff
				sampler2D _MainTex;
				sampler2D _BlendTex;

				fixed4 _MainTintColor;
				fixed4 _BlendTintColor;

				float _BlendTimeScaling;

				int _TextureSize;

				float _AmplitudeX;
				float _FrequencyX;
				float _CompressionX;
				float _TimeScalingX;

				float _AmplitudeY;
				float _FrequencyY;
				float _CompressionY;
				float _TimeScalingY;

				int _DistortX;
				int _DistortY;

				int _BlendMode;
				float _BlendStay;
				float _BlendWarp;

				fixed4 frag(v2f i) : SV_Target
				{
					// position values are [0,1], so we scale it to texture size, and will later scale it back down.
					float x = i.uv.x * _TextureSize;
					float y = i.uv.y * _TextureSize;

					// ================================= Distortion =================================

					float new_x = x;
					float new_y = y;

					// =========== DISTORT X ===========
					if (_DistortX == 1)
					{
						float offset_x = _AmplitudeX * sin(_FrequencyX * x + _TimeScalingX * _Time.x);
						new_x = x * _CompressionX + offset_x;
					}
					else if (_DistortX == 2)
					{
						float offset_x = _AmplitudeX * cos(_FrequencyX * x + _TimeScalingX * _Time.x);
						new_x = x * _CompressionX + offset_x;
					}
					else if (_DistortX == 3)
					{
						float offset_x = _AmplitudeX * sin(_FrequencyX * x + _TimeScalingX * _Time.x);
						new_x = x + _CompressionX * offset_x;
					}
					else if (_DistortX == 4)
					{
						float offset_x = _AmplitudeX * cos(_FrequencyX * x + _TimeScalingX * _Time.x);
						new_x = x + _CompressionX * offset_x;
					}

					// =========== DISTORY Y ===========
					if (_DistortY == 1)
					{
						float offset_y = _AmplitudeY * sin(_FrequencyY * y + _TimeScalingY * _Time.x);
						new_y = y * _CompressionY + offset_y;
					}
					else if (_DistortY == 2)
					{
						float offset_y = _AmplitudeY * cos(_FrequencyY * y + _TimeScalingY * _Time.x);
						new_y = y * _CompressionY + offset_y;
					}
					else if (_DistortY == 3)
					{
						float offset_y = _AmplitudeY * sin(_FrequencyY * y + _TimeScalingY * _Time.x);
						new_y = y + _CompressionY * offset_y;
					}
					else if (_DistortY == 4)
					{
						float offset_y = _AmplitudeY * cos(_FrequencyY * y + _TimeScalingY * _Time.x);
						new_y = y + _CompressionY * offset_y;
					}

					// =========== FINISH ===========

					new_x = fmod(new_x / _TextureSize + 1.0, 1.0);
					new_y = fmod(new_y / _TextureSize + 1.0, 1.0);

					float2 pos = float2(new_x, new_y);

					// ================================= Blend =================================

					float4 MainColor = tex2D(_MainTex, pos).rgba * _MainTintColor;
					float4 SecondaryColor = tex2D(_BlendTex, pos).rgba * _BlendTintColor;

					float4 result = MainColor;

					if (_BlendMode == 1)
					{
						float blendTime = (cos(_Time.x * _BlendTimeScaling) + 1) / 2;
						result = lerp(MainColor, SecondaryColor, blendTime);
					}
					else if (_BlendMode == 2)
					{
						float blendTime = sin(_Time.x * _BlendTimeScaling) / 2 * (_BlendStay * 2 + 1) + 0.5;
						if (blendTime <= 0)
						{
							result = MainColor;
						}
						else if (blendTime >= 1)
						{
							result = SecondaryColor;
						}
						else
						{
							result = lerp(MainColor, SecondaryColor, blendTime);
						}
					}
					else if (_BlendMode >= 3 && _BlendMode <= 4)
					{
						float d;
						if (_BlendMode == 3)
						{
							d = fmod(sin(_Time.x * _BlendTimeScaling) * sqrt((new_x - 0.5)*(new_x - 0.5) + (new_y - 0.5)*(new_y - 0.5)) / sqrt(_BlendWarp) + 1, 1);
						}
						else if (_BlendMode == 4)
						{
							d = fmod(tan(_Time.x * _BlendTimeScaling) * sqrt((new_x - 0.5)*(new_x - 0.5) + (new_y - 0.5)*(new_y - 0.5)) / sqrt(_BlendWarp) + 1, 1);
						}

						if ((d > 0 && d < 0.1) ||
							(d > 0.2 && d < 0.3) ||
							(d > 0.4 && d < 0.5) ||
							(d > 0.6 && d < 0.7) ||
							(d > 0.8 && d < 0.9))
						{
							result = SecondaryColor;
						}
						else
						{
							result = MainColor;
						}
					}

					return result;
				}

				ENDCG
			}
		}
}
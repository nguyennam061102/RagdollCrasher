Shader "Hidden/FullScreenFX" {

	Properties {
		_MainTex ("MainTex", 2D) = "white" {}

		[Header(Pixelation)]
		[Toggle(USE_PIXELATION)]
		_UsePixelation("Use Pixelation", Float) = 0

		_pixels ("Resolution", int) = 512 
        _pw("Pixel Width", float) = 64
        _ph("Pixel Height", float) = 64

		[Header(Chromatic Aberration)]
		[Toggle(USE_CHROMATICABERRATION)]
		_UseChromaticAberration("Use Chromatic Aberration", Float) = 0

		_ChromaticAberration ("Chromatic Aberration", Range(0.0,1.0)) = 0.001
		_Center ("Center",Range(0.0,0.5)) = 0.0

        [Header(Effect Mask)]
		_MaskSize("Mask Size", Range(0.0,1.0)) = 0.5
		_MaskContrast("Mask Contrast", Float) = 0.5
		
		[Header(Speed Effect)]
		[Toggle(USE_SPEEDEFFECT)]
		_UseSpeedEffect("Use SpeedEffect", Float) = 0

		_WindColor("Wind Color", Color) = (1, 1, 1, 1)
		[Toggle(USE_WINDNOISETEXTURE)]
		_UseWindNoiseTexure("Use WindNoiseTex", Float) = 0

		_WindNoiseTex("WindNoiseTexture", 2D) = "white" {}
		_WindNoiseProceduralLerp("Wind Noise Procedural Lerp", Float) = -1
		_WindSpeed("WindSpeed", Float) = 1

		[Header(Burn Effect)]
		[Toggle(USE_BURNEFFECT)]
		_UseBurnEffect("Use BurnEffect", Float) = 0

		[HDR]_BurnColor("Burn Color", Color) = (2.302577, 0.2715303, 0.2715303, 1)
		_NoiseTex("NoiseTexture", 2D) = "white" {}
        _NoiseTiling("NoiseTiling", Vector) = (1, 1, 0, 0)
        _NoiseSpeed("NoiseSpeed", Vector) = (0, 0, 0, 0)
        _NoisePower("NoisePower", Float) = 1
		_VignetteIntensity("VignetteIntensity", Range(0, 1)) = 0.7
        _VignetteRadiusPower("VignetteRadiusPower", Float) = 3
        _DistortionAmount("DistortionAmount", Range(0, 0.3)) = 0
        _DistortionScale("DistortionScale", Float) = 30
        _DistortionSpeed("DistortionSpeed", Vector) = (0, 1, 0, 0)
	}

	SubShader {
		Cull off
		Blend srcAlpha OneMinusSrcAlpha

		Pass {
			Name "FullScreenShader"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma shader_feature USE_PIXELATION
			#pragma shader_feature USE_CHROMATICABERRATION
			#pragma shader_feature USE_SPEEDEFFECT
			#pragma shader_feature USE_WINDNOISETEXTURE
			#pragma shader_feature USE_BURNEFFECT
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};

			uniform sampler2D _MainTex;
			uniform sampler2D _NoiseTex;

			//Pixelation
			float _pixels; 
            float _pw;
            float _ph;
            float _dx;
            float _dy;

			//Chromatic
			fixed _ChromaticAberration;
			fixed _Center;

			//Effects Mask
			float _MaskSize;
			float _MaskContrast;

			//Speed 
			float4 _WindColor;
			uniform sampler2D _WindNoiseTex;
			float _WindNoiseProceduralLerp;
			float _WindSpeed;

			//BurnEffect
			float4 _BurnColor;
			float4 _MainTex_TexelSize;
			float4 _NoiseTexture_TexelSize;
			float2 _NoiseTiling;
			float2 _NoiseSpeed;
			float _NoisePower;
			float _VignetteIntensity;
			float _VignetteRadiusPower;
			float _DistortionAmount;
			float2 _DistortionSpeed;
			float _DistortionScale;

			inline float unity_noise_randomValue (float2 uv){
				return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
			}

			inline float unity_noise_interpolate (float a, float b, float t){
				return (1.0-t)*a + (t*b);
			}

			inline float unity_valueNoise (float2 uv){
				float2 i = floor(uv);
				float2 f = frac(uv);
				f = f * f * (3.0 - 2.0 * f);

				uv = abs(frac(uv) - 0.5);
				float2 c0 = i + float2(0.0, 0.0);
				float2 c1 = i + float2(1.0, 0.0);
				float2 c2 = i + float2(0.0, 1.0);
				float2 c3 = i + float2(1.0, 1.0);
				float r0 = unity_noise_randomValue(c0);
				float r1 = unity_noise_randomValue(c1);
				float r2 = unity_noise_randomValue(c2);
				float r3 = unity_noise_randomValue(c3);

				float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
				float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
				float t = unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
				return t;
			}

			float Unity_SimpleNoise_float(float2 UV, float Scale){
				float t = 0.0;

				float freq = pow(2.0, float(0));
				float amp = pow(0.5, float(3-0));
				t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

				freq = pow(2.0, float(1));
				amp = pow(0.5, float(3-1));
				t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

				freq = pow(2.0, float(2));
				amp = pow(0.5, float(3-2));
				t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

				return t;
			}

			float2 Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale){
				float2 delta = UV - Center;
				float radius = length(delta) * 2 * RadialScale;
				float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
				return float2(radius, angle);
			}

			float2 Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation){
				Rotation = Rotation * (3.1415926f/180.0f);
				UV -= Center;
				float s = sin(Rotation);
				float c = cos(Rotation);
				float2x2 rMatrix = float2x2(c, -s, s, c);
				rMatrix *= 0.5;
				rMatrix += 0.5;
				rMatrix = rMatrix * 2 - 1;
				UV.xy = mul(UV.xy, rMatrix);
				UV += Center;
				return UV;
			}

			float4 Unity_InverseLerp_float4(float4 A, float4 B, float4 T){
				return (T - A)/(B - A);
			}

			float4 Unity_Lerp_float4(float4 A, float4 B, float4 T){
				return lerp(A, B, T);
			}

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag (v2f i) : COLOR {
	
				float2 coord = i.uv;
	
				fixed4 col = tex2D(_MainTex, coord);

				//float maskSize = _MaskSize * 0.5;
				//float mask = saturate(Unity_InverseLerp_float4(maskSize, maskSize + _MaskContrast, distance(i.screenPos, float2(0.5, 0.5))));
				float maskPhone = pow(Unity_PolarCoordinates_float(coord, float2(0.5, 0.5), 1, 1).x * _VignetteIntensity, _VignetteRadiusPower);
				
				#ifdef USE_SPEEDEFFECT
					float2 windCoord = Unity_Rotate_Degrees_float(Unity_PolarCoordinates_float(i.uv, float2(0.5, 0.5), 1, 1), float2(0.5, 0.5), 90);
					float windCurrentTime = _Time.y * _WindSpeed;

					#ifdef USE_WINDNOISETEXTURE
						fixed4 windcol = tex2D(_WindNoiseTex, windCoord * float2(3, 0.2) + float2(windCurrentTime * 0.2, windCurrentTime)) * maskPhone;
					#else
						float windNoiseTex = Unity_SimpleNoise_float(windCoord * float2(5, 0.2) + float2(windCurrentTime * 0.2, windCurrentTime), 50);
						fixed4 windcol = saturate(Unity_Lerp_float4(maskPhone, windNoiseTex, _WindNoiseProceduralLerp));
					#endif

					windcol *= _WindColor;
					col += windcol;
				#endif
	

				return col;

}
			ENDCG
		}
	}
}

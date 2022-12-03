#ifndef GPU_INSTANCER_CROSSFADE_INCLUDED
#define GPU_INSTANCER_CROSSFADE_INCLUDED

#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED) && !defined(GPUI_MHT_COPY_TEXTURE) && !defined(GPUI_MHT_MATRIX_APPEND)

#ifdef UNITY_APPLY_DITHER_CROSSFADE
#undef UNITY_APPLY_DITHER_CROSSFADE
#endif

#if SHADER_API_GLCORE || SHADER_API_GLES3
#define UNITY_APPLY_DITHER_CROSSFADE(vpos)
#else
#ifdef LOD_FADE_CROSSFADE
#define UNITY_APPLY_DITHER_CROSSFADE(vpos) UnityApplyDitherCrossFadeGPUI(vpos)
#else
#define UNITY_APPLY_DITHER_CROSSFADE(vpos)
#endif
#endif

uniform sampler2D _DitherMaskLOD2D;
void UnityApplyDitherCrossFadeGPUI(float2 vpos)
{
    if (LODLevel >= 0)
    {
        uint4 lodData = gpuiInstanceLODData[gpui_InstanceID];

        if (lodData.w > 0)
        {
            float fadeLevel = floor(lodData.w * fadeLevelMultiplier);

            if (lodData.z == uint(LODLevel))
                fadeLevel = 15 - fadeLevel;

            if (fadeLevel > 0)
            {
                vpos /= 4; // the dither mask texture is 4x4
                vpos.y = (frac(vpos.y) + fadeLevel) * 0.0625 /* 1/16 */; // quantized lod fade by 16 levels
                clip(tex2D(_DitherMaskLOD2D, vpos).a - 0.5);
            }
        }
    }
}

#ifdef UNITY_RANDOM_INCLUDED
#ifdef LODDitheringTransition
#undef LODDitheringTransition
#endif

#define LODDitheringTransition(fadeMaskSeed, ditherFactor) LODDitheringTransitionGPUI(fadeMaskSeed, ditherFactor)

    void LODDitheringTransitionGPUI(uint2 fadeMaskSeed, float ditherFactor)
    {
	    if (LODLevel >= 0)
	    {
		    uint4 lodData = gpuiInstanceLODData[gpui_InstanceID];

		    if (lodData.w > 0)
		    {
			    float fadeLevel = floor(lodData.w * fadeLevelMultiplier);

			    if (fadeLevel > 0)
			    {
				    ditherFactor =  (frac(fadeMaskSeed.y) + fadeLevel) * 0.0625;
				    float p = GenerateHashedRandomFloat(fadeMaskSeed);
				    float f = ditherFactor - p;
				    if (lodData.z == uint(LODLevel))
					    f = -f;
				    clip(f);
			    }
		    }
	    }
    }
#endif // UNITY_RANDOM_INCLUDED

#endif // SHADER_API

#endif // GPU_INSTANCER_CROSSFADE_INCLUDED
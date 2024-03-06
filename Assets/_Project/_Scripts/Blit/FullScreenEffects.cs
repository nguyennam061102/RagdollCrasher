using Unity.VisualScripting;
using UnityEngine;
public class FullScreenEffects : Singleton<FullScreenEffects> {
	[SerializeField] Material materialPixelationChromatic;

	[Range(0.0f, 1.0f)]
	public float chromaticAberration = 1.0f;
    public bool onTheScreenEdges = true;
    bool isOn;

	public void OnRenderImage(RenderTexture inTexture, RenderTexture outTexture) {
        float _verlosity = GameManager.Ins.Velocity - 300;
        if(_verlosity > -290)
        {
            OnSpeedEf();
            materialPixelationChromatic.SetFloat("_VignetteIntensity", (_verlosity + 300) * (0.15f / 290) + 0.5f);
            materialPixelationChromatic.SetFloat("_VignetteRadiusPower", _verlosity * (-1.5f / 290) + 1.5f);
        }
        else
        {
            OffSpeedEf();
        }

        materialPixelationChromatic.SetFloat("_ChromaticAberration", 0.01f * chromaticAberration);

        if (onTheScreenEdges)
			materialPixelationChromatic.SetFloat("_Center", 0.5f);

        else
			materialPixelationChromatic.SetFloat("_Center", 0);

        Graphics.Blit(inTexture, outTexture, materialPixelationChromatic);
	}

	void OnSpeedEf()
	{
        if (!isOn)
        {
            materialPixelationChromatic.EnableKeyword("USE_SPEEDEFFECT");
            isOn = true;
        }
    }
    void OffSpeedEf()
    {
        if (isOn)
        {
            materialPixelationChromatic.DisableKeyword("USE_SPEEDEFFECT");
            isOn = false;
        }
    }
    void Start() => materialPixelationChromatic.DisableKeyword("USE_SPEEDEFFECT");
}


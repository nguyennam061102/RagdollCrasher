using Unity.VisualScripting;
using UnityEngine;
public class FullScreenEffects : Singleton<FullScreenEffects> {
	[SerializeField] Material materialPixelationChromatic;

    public bool isOn ;

	public void OnRenderImage(RenderTexture inTexture, RenderTexture outTexture) {
        float _verlosity = GameManager.Ins.Velocity - 300;
        if(_verlosity > -290)
        {
            OnSpeedEf();
            if (_verlosity > 0) { 
                _verlosity = 0; 
            }
            materialPixelationChromatic.SetFloat("_VignetteIntensity", (_verlosity + 300) * (0.15f / 290) + 0.5f);
            materialPixelationChromatic.SetFloat("_VignetteRadiusPower", _verlosity * (-1.5f / 290) + 1.5f);
        }
        else
        {
            OffSpeedEf();
        }
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
    public void OffSpeedEf()
    {
        if (isOn)
        {
            materialPixelationChromatic.DisableKeyword("USE_SPEEDEFFECT");
            isOn = false;
        }
    }
    public void Off()
    {
        isOn = true;
        //OffSpeedEf();
        //materialPixelationChromatic.DisableKeyword("USE_SPEEDEFFECT");
    }
}


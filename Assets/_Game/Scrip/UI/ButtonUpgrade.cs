using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    Engine,
    Jetpack,
    Gold,
    Level,
    Motor,
    Reward,
    Purchase
}
public class ButtonUpgrade : MonoBehaviour, IObserver
{
    [SerializeField] ButtonType buttonType;
    [SerializeField] Button buttonUpgrade; 
    [SerializeField] TextMeshProUGUI gold;
    [SerializeField] TextMeshProUGUI power;
    [SerializeField] GameObject textUpgrade;
    [SerializeField] GameObject textFree;
    [SerializeField] ParticleSystem xitLua;
    bool isUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        buttonUpgrade.onClick.AddListener(() =>
        {
            UpgradePow();
        });
        SetText();
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
        OnNotify();
    }

    void UpgradePow()
    {
        if (isUpgrade)
        {
            //inter
            //UnityEvent e = new UnityEvent();
            //e.AddListener(() =>
            //{
                switch (buttonType)
                {
                    case ButtonType.Engine:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.EnginePowCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.EnginePowCoinUp;
                            SaveLoadData.Ins.DataGame.EnginePow += SaveLoadData.Ins.DataGame.EnginePow * 0.02f;
                            SaveLoadData.Ins.DataGame.EnginePowCoinUp += 200;
                            SaveLoadData.Ins.DataGame.EnginePowLv++;
                            gold.text = SaveLoadData.Ins.DataGame.EnginePowCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.EnginePowLv.ToString();
                            xitLua.Play();
                            AudioManager.Ins.PlaySfx(Constants.SFX_UPGRADE_1);
                        }
                        break;
                    case ButtonType.Jetpack:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.JetpackPowCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.JetpackPowCoinUp;
                            SaveLoadData.Ins.DataGame.JetpackPow += SaveLoadData.Ins.DataGame.EnginePow * 0.02f;
                            SaveLoadData.Ins.DataGame.JetpackPowCoinUp += 200;
                            SaveLoadData.Ins.DataGame.JetpackLv++;
                            gold.text = SaveLoadData.Ins.DataGame.JetpackPowCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.JetpackLv.ToString();
                            xitLua.Play();
                            AudioManager.Ins.PlaySfx(Constants.SFX_UPGRADE_1);
                        }

                        break;
                    case ButtonType.Gold:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.CoinRewardCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.CoinRewardCoinUp;
                            SaveLoadData.Ins.DataGame.CoinReward += SaveLoadData.Ins.DataGame.CoinReward * 0.05f;
                            SaveLoadData.Ins.DataGame.CoinRewardCoinUp += 160;
                            SaveLoadData.Ins.DataGame.CoinRewardLv++;
                            gold.text = SaveLoadData.Ins.DataGame.CoinRewardCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.CoinRewardLv.ToString();
                            xitLua.Play();
                            AudioManager.Ins.PlaySfx(Constants.SFX_UPGRADE_1);
                        }
                        break;
                }
            //});
            //bool showad = SkygoBridge.instance.ShowInterstitial(e);

            //ApplovinBridge.instance.ShowInterAdsApplovin(null);

        }
        else
        {
            // UnityEvent e = new UnityEvent();
            // e.AddListener(() =>
            // {
                switch (buttonType)
                {
                    case ButtonType.Engine:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.EnginePowCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.EnginePowCoinUp;
                            SaveLoadData.Ins.DataGame.EnginePow += SaveLoadData.Ins.DataGame.EnginePow * 0.02f;
                            SaveLoadData.Ins.DataGame.EnginePowCoinUp += 200;
                            SaveLoadData.Ins.DataGame.EnginePowLv++;
                            gold.text = SaveLoadData.Ins.DataGame.EnginePowCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.EnginePowLv.ToString();
                        }
                        break;
                    case ButtonType.Jetpack:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.JetpackPowCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.JetpackPowCoinUp;
                            SaveLoadData.Ins.DataGame.JetpackPow += SaveLoadData.Ins.DataGame.EnginePow * 0.02f;
                            SaveLoadData.Ins.DataGame.JetpackPowCoinUp += 200;
                            SaveLoadData.Ins.DataGame.JetpackLv++;
                            gold.text = SaveLoadData.Ins.DataGame.JetpackPowCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.JetpackLv.ToString();
                        }

                        break;
                    case ButtonType.Gold:
                        if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.CoinRewardCoinUp)
                        {
                            SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.CoinRewardCoinUp;
                            SaveLoadData.Ins.DataGame.CoinReward += SaveLoadData.Ins.DataGame.CoinReward * 0.005f;
                            SaveLoadData.Ins.DataGame.CoinRewardCoinUp += 160;
                            SaveLoadData.Ins.DataGame.CoinRewardLv++;
                            gold.text = SaveLoadData.Ins.DataGame.CoinRewardCoinUp.ToString();
                            power.text = SaveLoadData.Ins.DataGame.CoinRewardLv.ToString();
                        }
                        break;
                }
            // });
            // //SkygoBridge.instance.ShowRewarded(e, null);
            // //reward
            // ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);

        }
    }
    void SetText()
    {
        switch (buttonType)
        {
            case ButtonType.Engine:
                gold.text = SaveLoadData.Ins.DataGame.EnginePowCoinUp.ToString();
                power.text = SaveLoadData.Ins.DataGame.EnginePowLv.ToString();
                break;
            case ButtonType.Jetpack:
                gold.text = SaveLoadData.Ins.DataGame.JetpackPowCoinUp.ToString();
                power.text = SaveLoadData.Ins.DataGame.JetpackLv.ToString();
                break;
            case ButtonType.Gold:
                gold.text = SaveLoadData.Ins.DataGame.CoinRewardCoinUp.ToString();
                power.text = SaveLoadData.Ins.DataGame.CoinRewardLv.ToString();
                break;
        }
    }

    public void OnNotify()
    {
        switch (buttonType)
        {
            case ButtonType.Engine:
                if (SaveLoadData.Ins.DataGame.Coin < SaveLoadData.Ins.DataGame.EnginePowCoinUp)
                {
                    textUpgrade.SetActive(false);
                    textFree.SetActive(true);
                    isUpgrade = false;
                }
                else
                {
                    textUpgrade.SetActive(true);
                    textFree.SetActive(false);
                    isUpgrade = true;
                }
                break;
            case ButtonType.Jetpack:
                if (SaveLoadData.Ins.DataGame.Coin < SaveLoadData.Ins.DataGame.JetpackPowCoinUp)
                {
                    textUpgrade.SetActive(false);
                    textFree.SetActive(true);
                    isUpgrade = false;
                }
                else
                {
                    textUpgrade.SetActive(true);
                    textFree.SetActive(false);
                    isUpgrade = true;
                }
                break;
            case ButtonType.Gold:
                if (SaveLoadData.Ins.DataGame.Coin < SaveLoadData.Ins.DataGame.CoinRewardCoinUp)
                {
                    textUpgrade.SetActive(false);
                    textFree.SetActive(true);
                    isUpgrade = false;
                }
                else
                {
                    textUpgrade.SetActive(true);
                    textFree.SetActive(false);
                    isUpgrade = true;
                }
                break;
        }
    }
}

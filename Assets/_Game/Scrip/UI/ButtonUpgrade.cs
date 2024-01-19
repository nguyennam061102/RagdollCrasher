using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
public class ButtonUpgrade : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;
    [SerializeField] Button buttonUpgrade; 
    [SerializeField] TextMeshProUGUI gold;
    [SerializeField] TextMeshProUGUI power;
    // Start is called before the first frame update
    void Start()
    {
        buttonUpgrade.onClick.AddListener(() =>
        {
            UpgradePow();
        });
        SetText();
    }

    void UpgradePow()
    {
        switch (buttonType)
        {
            case ButtonType.Engine:
                if(SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.EnginePowCoinUp)
                {
                    SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.EnginePowCoinUp;
                    SaveLoadData.Ins.DataGame.EnginePow += SaveLoadData.Ins.DataGame.EnginePow * 0.02f;
                    SaveLoadData.Ins.DataGame.EnginePowCoinUp += 150;
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
                    SaveLoadData.Ins.DataGame.JetpackPowCoinUp += 150;
                    SaveLoadData.Ins.DataGame.JetpackLv++;
                    gold.text = SaveLoadData.Ins.DataGame.JetpackPowCoinUp.ToString();
                    power.text = SaveLoadData.Ins.DataGame.JetpackLv.ToString();
                }
                
                break;
            case ButtonType.Gold:
                if (SaveLoadData.Ins.DataGame.Coin >= SaveLoadData.Ins.DataGame.CoinRewardCoinUp)
                {
                    SaveLoadData.Ins.DataGame.Coin -= SaveLoadData.Ins.DataGame.CoinRewardCoinUp;
                    SaveLoadData.Ins.DataGame.CoinReward += SaveLoadData.Ins.DataGame.CoinReward * 0.1f;
                    SaveLoadData.Ins.DataGame.CoinRewardCoinUp += 150;
                    SaveLoadData.Ins.DataGame.CoinRewardLv++;
                    gold.text = SaveLoadData.Ins.DataGame.CoinRewardCoinUp.ToString();
                    power.text = SaveLoadData.Ins.DataGame.CoinRewardLv.ToString();
                }
                break;
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
}

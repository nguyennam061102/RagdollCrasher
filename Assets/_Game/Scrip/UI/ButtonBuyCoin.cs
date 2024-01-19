using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuyCoin : MonoBehaviour
{
    [SerializeField] ButtonType type;
    [SerializeField] float coin;
    [SerializeField] float money;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textMoney;

    // Start is called before the first frame update
    void Start()
    {
        textCoin.text = coin.ToString();
        if(type == ButtonType.Purchase)
        {
            textMoney.text = money.ToString() + "$";
        }
        button.onClick.AddListener(() =>
        {
            if(type == ButtonType.Reward)
            {
                GetCoinReward();
            }else if(type == ButtonType.Purchase) 
            { 
                GetCoinPurchase();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetCoinReward()
    {
        SaveLoadData.Ins.DataGame.coin += (int)coin;
    }
    void GetCoinPurchase()
    {
        SaveLoadData.Ins.DataGame.coin += (int)coin;
    }
}

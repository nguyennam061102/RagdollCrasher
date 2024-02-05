using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBuyCoin : MonoBehaviour, IObserver
{
    [SerializeField] ButtonType type;
    [SerializeField] int coin;
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
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetCoinReward()
    {
        // UnityEvent e = new UnityEvent();
        // e.AddListener(() =>
        // {
            SaveLoadData.Ins.DataGame.coin += coin;
            SaveLoadData.Ins.Save();
        // });
        // //SkygoBridge.instance.ShowRewarded(e, null);
        // //reward
        // ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);

    }
    void GetCoinPurchase()
    {
        //purchase
        //Buy in game, price is money
        // string sku = "";
        // Debug.Log(price + " : " + claimValue);
        // sku = "fight_dynasty_cash_" + price.ToString();
        // Debug.Log(sku);
        // UnityEvent e = new UnityEvent();
        // e.AddListener(() =>
        // {
            SaveLoadData.Ins.DataGame.coin += coin;

        //noAdsBtn.SetActive(false);
        // });

        // SkygoBridge.instance.PurchaseIAP(sku, e);

    }

    public void OnNotify()
    {
        
    }
}

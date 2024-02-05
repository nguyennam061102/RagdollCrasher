using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonChose : MonoBehaviour,IObserver
{
    [SerializeField] Button button;
    [SerializeField] ButtonType type;
    [SerializeField] int lvChose;
    [SerializeField] MotorType motorType;
    [SerializeField] GameObject imageLock;
    [SerializeField] bool isPurchase;
    [SerializeField] float money;
    [SerializeField] TextMeshProUGUI moneyText;
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            Chose();
        });
        isPurchase = SaveLoadData.Ins.MotorPurchase[motorType];
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
        if(type == ButtonType.Level)
        {
            if(lvChose <= SaveLoadData.Ins.DataGame.Lv)
            {
                button.interactable = true;
                imageLock.SetActive(false);
            }
            else
            {
                button.interactable = false;
                imageLock.SetActive(true);
            }
        }
        else
        {
            if (isPurchase)
            {
                moneyText.text = "$" + money.ToString();
            }
        }
    }
    void Chose()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            if (type == ButtonType.Level)
            {
                SaveLoadData.Ins.DataGame.CurrenLv = lvChose;
                LevelManager.Ins.ChaneMap();
                UIManager.Ins.GetUI<UIChoseLv>().OpenNewUI<UIStart>();
            }
            else if (type == ButtonType.Motor)
            {
                if (isPurchase)
                {
                //purchase
                //purchase
                //Buy in game, price is money
                // string sku = "";
                // Debug.Log(price + " : " + claimValue);
                // sku = "fight_dynasty_cash_" + price.ToString();
                // Debug.Log(sku);
                // UnityEvent e = new UnityEvent();
                // e.AddListener(() =>
                // {
                isPurchase = false;
                SaveLoadData.Ins.MotorPurchase[motorType] = false;
                SaveLoadData.Ins.Save();

                //noAdsBtn.SetActive(false);
                // });

                // SkygoBridge.instance.PurchaseIAP(sku, e);
            }
            else
                {
                    SaveLoadData.Ins.DataGame.CurrenMotor = motorType;
                    LevelManager.Ins.ChaneMotor();
                    UIManager.Ins.GetUI<UIChoseMotor>().OpenNewUI<UIStart>();

                }
            }
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //ApplovinBridge.instance.ShowInterAdsApplovin(null);
    }

    public void OnNotify()
    {
        if (type == ButtonType.Level)
        {
            if (lvChose <= SaveLoadData.Ins.DataGame.Lv)
            {
                button.interactable = true;
                imageLock.SetActive(false);
            }
            else
            {
                button.interactable = false;
                imageLock.SetActive(true);
            }
        }
    }
}

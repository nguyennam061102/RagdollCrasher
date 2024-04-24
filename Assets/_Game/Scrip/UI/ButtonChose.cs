using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonChose : MonoBehaviour, IObserver
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
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        });
        isPurchase = SaveLoadData.Ins.MotorPurchase[motorType];
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
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
                //Buy in game, price is money
                string sku = "";
                string str = "";
                Debug.Log(" motorType: " + motorType);
                if (motorType == MotorType.Taiowa) str = "499";
                else if (motorType == MotorType.Makhai) str = "299";
                else if (motorType == MotorType.Emma) str = "099";
                sku = "ragdoll_crasher_moto_" + str;
                Debug.Log(sku);
                UnityEvent e = new UnityEvent();
                e.AddListener(() =>
                {
                    isPurchase = false;
                    SaveLoadData.Ins.MotorPurchase[motorType] = false;
                    SaveLoadData.Ins.Save();
                });
                //SkygoBridge.instance.PurchaseIAP(sku, e);
            }
            else
            {
                SaveLoadData.Ins.DataGame.CurrenMotor = motorType;
                LevelManager.Ins.ChaneMotor();
                UIManager.Ins.GetUI<UIChoseMotor>().OpenNewUI<UIStart>();

            }
        }
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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStart : UICanvas, IObserver
{
    [SerializeField] Button buttonChoseLv;
    [SerializeField] Button buttonChoseMotor;
    [SerializeField] Button buttonSetting;
    [SerializeField] Button buttonGold;
    [SerializeField] Button buttonNoADS;
    [SerializeField] Button buttonOnlineGift;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI countOnlGiftText;
    [SerializeField] Image fade;
    protected override void Start()
    {
        base.Start();
        SetGold();
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
        fade.DOFade(0f, 1.5f);
        countOnlGiftText.text = SaveLoadData.Ins.DataGame.CountOnlineGift.ToString() + "/3";
        buttonChoseLv.onClick.AddListener(() =>
        {
            ButtonChoseLv();
        });
        buttonChoseMotor.onClick.AddListener(() =>
        {
            ButtonChoseMotor();
        });
        buttonSetting.onClick.AddListener(() =>
        {
            ButtonSetting();
        });
        buttonGold.onClick.AddListener(() =>
        {
            ButtonGold();
        });
        buttonNoADS.onClick.AddListener(() =>
        {
            ButtonNoADS();
        });
        buttonOnlineGift.onClick.AddListener(() =>
        {
            ButtonOnlGift();
        });
    }
    protected override void OnInit()
    {
        base.OnInit();
        SetGold();
    }
    public void SetGold()
    {
        goldText.text = SaveLoadData.Ins.DataGame.Coin.ToString();
    }
    void ButtonChoseLv()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIChoseLv>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonChoseMotor()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIChoseMotor>();
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonSetting()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UISetting>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonGold()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIBuyCoin>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);
        //ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonNoADS()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
    }
    void ButtonOnlGift()
    {
        // UnityEvent e = new UnityEvent();
        // e.AddListener(() =>
        // {
            if (SaveLoadData.Ins.DataGame.CountOnlineGift < 3)
            {
                SaveLoadData.Ins.DataGame.CountOnlineGift++;
                countOnlGiftText.text = SaveLoadData.Ins.DataGame.CountOnlineGift.ToString() + "/3";
                AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
            }
            else
            {
                SaveLoadData.Ins.DataGame.CountOnlineGift = 1;
                SaveLoadData.Ins.DataGame.Coin += 500;
                countOnlGiftText.text = SaveLoadData.Ins.DataGame.CountOnlineGift.ToString() + "/3";
                AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
            }
        // });
        // //SkygoBridge.instance.ShowRewarded(e, null);
        //reward
        // ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);

    }
    public void OnNotify()
    {
        SetGold();
    }
}

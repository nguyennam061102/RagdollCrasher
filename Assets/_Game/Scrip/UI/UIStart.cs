using DG.Tweening;
using InfinityCode.UltimateEditorEnhancer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] Transform logo;
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
        logo.DOScale(1, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void SetGold()
    {
        goldText.text = SaveLoadData.Ins.DataGame.Coin.ToString();
    }
    void ButtonChoseLv()
    {
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIChoseLv>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //inter
        if(SkygoBridge.instance.inter_menu_game == 1) ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonChoseMotor()
    {
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIChoseMotor>();
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //inter
        if (SkygoBridge.instance.inter_menu_game == 1) ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonSetting()
    {
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UISetting>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //inter
        if (SkygoBridge.instance.inter_menu_game == 1) ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonGold()
    {
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            OpenNewUI<UIBuyCoin>();
            AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);

        //inter
        if (SkygoBridge.instance.inter_menu_game == 1) ApplovinBridge.instance.ShowInterAdsApplovin(null);

    }
    void ButtonNoADS()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
        UnityEvent e = new UnityEvent();
        e.AddListener(() =>
        {
            SkygoBridge.instance.CanShowAd = 0;
        });
        SkygoBridge.instance.PurchaseIAP("ragdoll_crasher_noads_099", e);
    }
    void ButtonOnlGift()
    {
        UnityEvent e = new UnityEvent();
        e.AddListener(() =>
        {
            if (SaveLoadData.Ins.DataGame.CountOnlineGift < 2)
            {
                SaveLoadData.Ins.DataGame.CountOnlineGift++;
                countOnlGiftText.text = SaveLoadData.Ins.DataGame.CountOnlineGift.ToString() + "/3";
                AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
            }
            else
            {
                UIManager.Ins.SpawnCoin(buttonOnlineGift.GetComponent<RectTransform>(), false);
                SaveLoadData.Ins.DataGame.CountOnlineGift = 0;
                SaveLoadData.Ins.DataGame.Coin += 5000;
                countOnlGiftText.text = SaveLoadData.Ins.DataGame.CountOnlineGift.ToString() + "/3";
                AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
            }
            //logevent
            SkygoBridge.instance.LogEvent("reward_onlGift");
        });
        //reward
        ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);

    }
    public void OnNotify()
    {
        SetGold();
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : UICanvas
{
    [SerializeField] Button buttonChoseLv;
    [SerializeField] Button buttonChoseMotor;
    [SerializeField] Button buttonSetting;
    [SerializeField] Button buttonGold;
    [SerializeField] TextMeshProUGUI goldText;
    protected override void Start()
    {
        base.Start();
        SetGold();
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
    }
    void Update()
    {
        
    }
    public void SetGold()
    {
        goldText.text = SaveLoadData.Ins.DataGame.Coin.ToString();
    }
    void ButtonChoseLv()
    {
        OpenNewUI<UIChoseLv>();
    }
    void ButtonChoseMotor()
    {
        OpenNewUI<UIChoseMotor>();
    }
    void ButtonSetting()
    {
        OpenNewUI<UISetting>();
    }
    void ButtonGold()
    {
        OpenNewUI<UIBuyCoin>();
    }
}

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
}

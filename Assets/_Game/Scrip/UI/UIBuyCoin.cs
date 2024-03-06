using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyCoin : UICanvas,IObserver
{
    [SerializeField] Button buttonBack;
    [SerializeField] TextMeshProUGUI textCoin;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buttonBack.onClick.AddListener(() => {
            ButtonQuit();
        });
        SaveLoadData.Ins.DataGame.RegisterObserver(this);
        textCoin.text = SaveLoadData.Ins.DataGame.Coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ButtonQuit()
    {
        OpenNewUI<UIStart>();
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
    }

    public void OnNotify()
    {
        textCoin.text = SaveLoadData.Ins.DataGame.Coin.ToString();
    }
}

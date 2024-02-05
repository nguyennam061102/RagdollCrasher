using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISetting : UICanvas
{
    [SerializeField] Button sfx;
    [SerializeField] Button music;
    [SerializeField] Button buttonHome;
    [SerializeField] Image musicImg;
    [SerializeField] Image soundImg;
    [SerializeField] Image musicToggle;
    [SerializeField] Image soundToggle;


    [SerializeField] Sprite On;
    [SerializeField] Sprite Off;

    [SerializeField] Sprite OnVibrate;
    [SerializeField] Sprite OffVibrate;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sfx.onClick.AddListener(() => {
            ToggleVibration(soundToggle, soundImg);
        });
        music.onClick.AddListener(() => {
            ToggleVibration(musicToggle, musicImg);
        });
        buttonHome.onClick.AddListener(() => {
            ButtonQuit();
        });
    }

   

    void ToggleVibration(Image toggle, Image image)
    {
        if (SaveLoadData.Ins.DataGame.VibrateData == 1)
        {
            SaveLoadData.Ins.DataGame.VibrateData = 0;
            // turn off music
            toggle.sprite = Off;
            image.sprite = OffVibrate;
            toggle.rectTransform.DOAnchorPosX(-51f, .1f);

        }
        else if (SaveLoadData.Ins.DataGame.VibrateData == 0)
        {
            SaveLoadData.Ins.DataGame.VibrateData = 1;
            //turn on music
            toggle.sprite = On;
            image.sprite = OnVibrate;
            toggle.rectTransform.DOAnchorPosX(51f, .1f);

        }

    }

    void ButtonQuit()
    {
        OpenNewUI<UIStart>();
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
    }
}

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
        ToggleVibration(soundToggle, soundImg);
        ToggleVibration(musicToggle, musicImg);
        sfx.onClick.AddListener(() => {
            ToggleSfx(soundToggle, soundImg);
            AudioManager.Ins.ToggleSfx();
        });
        music.onClick.AddListener(() => {
            ToggleMusic(musicToggle, musicImg);
            AudioManager.Ins.ToggleMusic();
        });
        buttonHome.onClick.AddListener(() => {
            ButtonQuit();
        });
    }

   void ToggleMusic(Image toggle, Image image)
    {
        if (SaveLoadData.Ins.DataGame.MusicOn)
        {
            SaveLoadData.Ins.DataGame.MusicOn = false;
            // turn off music
            toggle.sprite = Off;
            image.sprite = OffVibrate;
            toggle.rectTransform.DOAnchorPosX(-51f, .1f);
        }
        else
        {
            SaveLoadData.Ins.DataGame.MusicOn = true;
            //turn on music
            toggle.sprite = On;
            image.sprite = OnVibrate;
            toggle.rectTransform.DOAnchorPosX(51f, .1f);
        }
    }
    void ToggleSfx(Image toggle, Image image)
    {
        if (SaveLoadData.Ins.DataGame.SfxOn)
        {
            SaveLoadData.Ins.DataGame.SfxOn = false;
            // turn off music
            toggle.sprite = Off;
            image.sprite = OffVibrate;
            toggle.rectTransform.DOAnchorPosX(-51f, .1f);
        }
        else
        {
            SaveLoadData.Ins.DataGame.SfxOn = true;
            //turn on music
            toggle.sprite = On;
            image.sprite = OnVibrate;
            toggle.rectTransform.DOAnchorPosX(51f, .1f);
        }
    }
    void ToggleVibration(Image toggle, Image image)
    {
        if(toggle == musicToggle)
        {
            if (!SaveLoadData.Ins.DataGame.MusicOn)
            {
                // turn off music
                toggle.sprite = Off;
                image.sprite = OffVibrate;
                toggle.rectTransform.DOAnchorPosX(-51f, .1f);
            }
            else
            { 
                //turn on music
                toggle.sprite = On;
                image.sprite = OnVibrate;
                toggle.rectTransform.DOAnchorPosX(51f, .1f);
            }
        }
        else
        {
            if (!SaveLoadData.Ins.DataGame.SfxOn)
            {
                // turn off music
                toggle.sprite = Off;
                image.sprite = OffVibrate;
                toggle.rectTransform.DOAnchorPosX(-51f, .1f);
            }
            else
            {
                //turn on music
                toggle.sprite = On;
                image.sprite = OnVibrate;
                toggle.rectTransform.DOAnchorPosX(51f, .1f);
            }
        }

    }

    void ButtonQuit()
    {
        OpenNewUI<UIStart>();
        AudioManager.Ins.PlaySfx(Constants.SFX_CLICK_UI);
    }
}

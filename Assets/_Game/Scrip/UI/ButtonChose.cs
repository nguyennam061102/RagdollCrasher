using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChose : MonoBehaviour,IObserver
{
    [SerializeField] Button button;
    [SerializeField] ButtonType type;
    [SerializeField] int lvChose;
    [SerializeField] MotorType motorType;
    [SerializeField] GameObject imageLock;
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            Chose();
        });
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
    }
    void Chose()
    {
        if(type == ButtonType.Level)
        {
            SaveLoadData.Ins.DataGame.CurrenLv = lvChose;
            LevelManager.Ins.ChaneMap();
            UIManager.Ins.GetUI<UIChoseLv>().OpenNewUI<UIStart>();
        }
        else if(type == ButtonType.Motor)
        {
            SaveLoadData.Ins.DataGame.CurrenMotor = motorType;
            LevelManager.Ins.ChaneMotor();
            UIManager.Ins.GetUI<UIChoseMotor>().OpenNewUI<UIStart>();
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

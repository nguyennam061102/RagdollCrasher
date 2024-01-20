using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChose : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] ButtonType type;
    [SerializeField] int lvChose;
    [SerializeField] MotorType motorType;
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            Chose();
        });
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
}

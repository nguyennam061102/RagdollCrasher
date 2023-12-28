using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoseMotor : UICanvas    
{
    [SerializeField] Button buttonQuit;
    protected override void Start()
    {
        base.Start();
        buttonQuit.onClick.AddListener(() =>
        {
            ButtonQuit();
        });
    }
    void ButtonQuit()
    {
        OpenNewUI<UIStart>();
    }
}

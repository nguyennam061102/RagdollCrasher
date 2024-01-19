using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyCoin : UICanvas
{
    [SerializeField] Button buttonBack;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buttonBack.onClick.AddListener(() => {
            ButtonQuit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ButtonQuit()
    {
        OpenNewUI<UIStart>();
    }
}

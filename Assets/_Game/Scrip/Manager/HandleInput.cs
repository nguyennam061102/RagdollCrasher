using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleInput : Singleton<HandleInput>
{
    [SerializeField] EventTrigger eventTrigger;
    [SerializeField] Button buttonPress;
    public EventTrigger EventTrigger { get => eventTrigger; set => eventTrigger = value; }
    public Button ButtonPress { get => buttonPress; set => buttonPress = value; }

    private void Start()
    {
       
    }
}

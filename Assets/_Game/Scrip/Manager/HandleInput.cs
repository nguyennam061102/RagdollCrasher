using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleInput : Singleton<HandleInput>
{
    [SerializeField] EventTrigger eventTrigger;

    public EventTrigger EventTrigger { get => eventTrigger; set => eventTrigger = value; }
    private void Start()
    {
       
    }
}

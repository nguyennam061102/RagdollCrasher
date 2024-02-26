using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVar : MonoBehaviour
{
    bool isVar;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(UIManager.Ins.IsOpened<UIEnd>());
        if (!isVar && !UIManager.Ins.IsOpened<UIEnd>())
        {
            AudioManager.Ins.PlaySfx(Constants.SFX_VAR);
            AudioManager.Ins.PlaySfx(Constants.SFX_FALL);
            isVar = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isVar = false;
    }
}

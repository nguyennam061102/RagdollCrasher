using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVar : MonoBehaviour
{
    bool isVar;
    private void OnTriggerEnter(Collider other)
    {
        if (!isVar)
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

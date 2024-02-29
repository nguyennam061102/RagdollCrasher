using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBreakGame : UICanvas
{
    public void OnStart()
    {
        StartCoroutine(PopupBreak());

    }

    IEnumerator PopupBreak()
    {
        yield return new WaitForSeconds(2);
        transform.DOScale(1f, 1f);
    }
}

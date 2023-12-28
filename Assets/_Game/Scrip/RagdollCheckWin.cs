using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCheckWin : MonoBehaviour
{
    bool isWin;
    private void OnTriggerEnter(Collider other)
    {
        if (!isWin)
        {
            SaveLoadData.Ins.DataGame.CurrenLv++;
            SaveLoadData.Ins.Save();
            isWin = true;
        }
    }
}

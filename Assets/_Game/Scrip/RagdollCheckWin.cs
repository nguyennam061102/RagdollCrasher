using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCheckWin : MonoBehaviour
{
    bool isWin;
    [SerializeField] List<Rigidbody> rbs;
    private void OnTriggerEnter(Collider other)
    {
        if (!isWin)
        {
            if (SaveLoadData.Ins.DataGame.Lv < LevelManager.Ins.Maps.Count - 1)
            {
                SaveLoadData.Ins.DataGame.Lv++;
                SaveLoadData.Ins.Save();
            }
            if(SaveLoadData.Ins.DataGame.CurrenLv < SaveLoadData.Ins.DataGame.Lv)
            {
                SaveLoadData.Ins.DataGame.CurrenLv++;
                SaveLoadData.Ins.Save();
            }
            else
            {
                UIManager.Ins.OpenUI<UIBreakGame>();
                UIManager.Ins.GetUI<UIBreakGame>().OnStart();
            }
            isWin = true;
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }
            
        }
    }
}

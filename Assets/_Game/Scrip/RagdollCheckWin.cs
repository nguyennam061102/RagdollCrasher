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
            SaveLoadData.Ins.DataGame.CurrenLv++;
            SaveLoadData.Ins.Save();
            isWin = true;
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewQC : MonoBehaviour
{
    [SerializeField] int viewCount;
    [SerializeField] GameObject imgLock;
    [SerializeField] Button buttonView;
    [SerializeField] MotorType motorType;
    [SerializeField] TextMeshProUGUI textCountView;
    // Start is called before the first frame update
    void Start()
    {
        buttonView.onClick.AddListener(()=>{
            ButtonViewQC();
        });
        CheckCountQC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ButtonViewQC()
    {
        // UnityEvent e = new UnityEvent();
        // e.AddListener(() =>
        // {
        SaveLoadData.Ins.MotorReward[motorType]++;
        CheckCountQC();
        // });
        // //SkygoBridge.instance.ShowRewarded(e, null);
        //reward
        // ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);
    }
    void CheckCountQC()
    {
        if (SaveLoadData.Ins.MotorReward[motorType] == viewCount)
        {
            imgLock.SetActive(false);
            buttonView.gameObject.SetActive(false);
        }
        else
        {
            imgLock.SetActive(true);
            buttonView.gameObject.SetActive(true);
            textCountView.text = "  " + SaveLoadData.Ins.MotorReward[motorType].ToString() + "/" + viewCount.ToString();
        }
    }
}

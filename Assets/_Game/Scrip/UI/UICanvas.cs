using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;
    public bool IsDestroyOnClose = false;
    [SerializeField] protected List<RectTransform> obj;
    [SerializeField] protected List<Vector3> curPosObj;
    protected RectTransform m_RectTransform;
    private Animator m_Animator;
    // private float m_OffsetY = 0;

    protected virtual void Start()
    {
        OnInit();
    }

    //Init default Canvas
    //khoi tao gia tri canvas
    protected virtual void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();
        foreach (Transform t in obj)
        {
            curPosObj.Add(t.localPosition);
            t.DOLocalMove(new Vector3(0, 0, 0), 1.5f);
        }
        // xu ly tai tho
        // float ratio = (float)Screen.height / (float)Screen.width;
        // if (ratio > 2.1f)
        // {
        //     Vector2 leftBottom = m_RectTransform.offsetMin;
        //     Vector2 rightTop = m_RectTransform.offsetMax;
        //     rightTop.y = -100f;
        //     m_RectTransform.offsetMax = rightTop;
        //     leftBottom.y = 0f;
        //     m_RectTransform.offsetMin = leftBottom;
        //     m_OffsetY = 100f;
        // }
    }
    public void OpenNewUI<T>() where T : UICanvas
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].DOLocalMove(curPosObj[i], 1.5f);
        }
        UIManager.Ins.OpenUI<T>();
        UIManager.Ins.GetUI<T>().OnInit();
    }
    //Setup canvas to avoid flash UI
    //set up mac dinh cho UI de tranh truong hop bi nhay' hinh
    public virtual void Setup()
    {
        UIManager.Ins.AddBackUI(this);
        UIManager.Ins.PushBackAction(this, BackKey);
    }


    //back key in android device
    //back key danh cho android
    public virtual void BackKey()
    {

    }

    //Open canvas
    //mo canvas
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //close canvas directly
    //dong truc tiep, ngay lap tuc
    public virtual void CloseDirectly()
    {
        UIManager.Ins.RemoveBackUI(this);
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }

    }

    //close canvas with delay time, used to anim UI action
    //dong canvas sau mot khoang thoi gian delay
    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

}

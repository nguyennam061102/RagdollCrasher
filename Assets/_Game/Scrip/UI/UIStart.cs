using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : UICanvas
{
    [SerializeField] List<RectTransform> obj;
    [SerializeField] List<Vector3> curPosObj;
    // Start is called before the first frame update
    void Start()
    {
        foreach (RectTransform t in obj)
        {
            curPosObj.Add(t.localPosition);
            t.DOLocalMove(new Vector3(0, 0, 0), 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenNewUI<T>() where T : UICanvas
    {
        for(int i = 0; i < obj.Count; i++)
        {
            obj[i].DOLocalMove(curPosObj[i], 1.5f);
        }
        UIManager.Ins.OpenUI<T>();
    }
}

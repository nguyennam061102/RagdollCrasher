using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotate : MonoBehaviour
{
    [SerializeField] List<RectTransform> objs; 
    // Start is called before the first frame update
    void Start()
    {
        //foreach (var obj in objs)
        //{
        //    obj.DORotate(new Vector3(0, 0, 10000), 500, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in objs)
        {
            obj.Rotate(new Vector3(0, 0, 1));
        }
    }
}

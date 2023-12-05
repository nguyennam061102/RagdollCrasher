using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    [SerializeField] List<RectTransform> obj;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in obj)
        {
            t.DOLocalMove(new Vector3(0, 0, 0), 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

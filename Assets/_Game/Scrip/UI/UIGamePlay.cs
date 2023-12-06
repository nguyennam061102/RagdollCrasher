using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIGamePlay : UICanvas
{
    [SerializeField] List<RectTransform> obj;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI velocityText;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in obj)
        {
            t.DOLocalMoveY(0, 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVelocity(float velocity)
    {

        velocityText.text = Mathf.CeilToInt(velocity).ToString();
    }
    public void SetDistance(float distance)
    {

        distanceText.text = Mathf.CeilToInt(distance).ToString();
    }
}

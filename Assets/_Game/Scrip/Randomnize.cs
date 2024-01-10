using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Randomnize : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private Tween arrowTween;
    [SerializeField] float reward;
    [SerializeField] float rotate;

    public float Reward { get => reward; set => reward = value; }

    private void Start()
    {
        Ran();
    }
    private void Update()
    {
        CheckReward();
        
    }
    [Button]
    public void Ran()
    {
        //arrow.localPosition = Vector3.zero;
        //arrowTween = arrow.DOAnchorPosX(distancePosX, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        arrowTween = arrow.DORotate(new Vector3(0,0, rotate), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void StopTween()
    {
        arrowTween.Kill();
    }
    void CheckReward()
    {
        int numReward = Mathf.CeilToInt(arrow.localRotation.eulerAngles.z / (rotate / 7)) + 1;
        if(numReward > 5)
        {
            reward = 10 - numReward;
        }
        else
        {
            reward = numReward;
        }
    }
}

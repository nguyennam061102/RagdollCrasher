using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class coinReward : MonoBehaviour
{
    [SerializeField] private List<RectTransform> coins;

    [SerializeField] List<MeshFilter> mesh;
    [SerializeField] GameObject newmesh;
    [SerializeField] private Transform oldmesh;
    [Button]
    public void SetMesh()
    {
        foreach(MeshFilter filter in mesh)
        {
            int rd = Random.Range(30, 70);
            filter.transform.localScale = new Vector3 (rd, rd, rd);
        }
    }
    public void CountCoins(RectTransform tf)
    {
        var delay = 0f;
        Debug.Log(tf.localPosition);
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            coins[i].DOMove(tf.position, 1.3f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
            coins[i].DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);
            coins[i].DOScale(0f, 0.3f).SetDelay(delay + 1.8f).SetEase(Ease.OutBack);

            delay += 0.1f; 
        }
        StartCoroutine(CountDollars());
    }
    IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
}

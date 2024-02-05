using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEnd : UICanvas
{
    [SerializeField] Button getCoin;
    [SerializeField] Button getRewardCoin;
    [SerializeField] Slider pathMoveSlider;
    [SerializeField] TextMeshProUGUI pathMoveText;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI coinReward;
    [SerializeField] TextMeshProUGUI coinRewardQC;
    [SerializeField] Image interacableImage;
    [SerializeField] int coin;
    [SerializeField] Transform objCoinRwQC;

    [SerializeField] Randomnize randomnize;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        interacableImage.gameObject.SetActive(false);
        getCoin.onClick.AddListener(() =>
        {
            GetCoinReward();
        });

        getRewardCoin.onClick.AddListener(() =>
        {
            GetCoinRewardQC();
        });
    }
    private void Update()
    {
        coinRewardQC.text = (randomnize.Reward * coin).ToString();
    }
    void GetCoinReward()
    {
        //inter
        //UnityEvent e = new UnityEvent();
        //e.AddListener(() =>
        //{
            SaveLoadData.Ins.DataGame.Coin += coin;
            SaveLoadData.Ins.Save();
            SceneManager.LoadScene("GamePlay");
        //});
        //bool showad = SkygoBridge.instance.ShowInterstitial(e);
        
        //ApplovinBridge.instance.ShowInterAdsApplovin(null);
    }
    void GetCoinRewardQC()
    {
        interacableImage.gameObject.SetActive(true);
        randomnize.StopTween();
        objCoinRwQC.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        StartCoroutine(SetRewardQCCoin());
    }
    IEnumerator SetRewardQCCoin()
    {
        yield return new WaitForSeconds(2);
        // UnityEvent e = new UnityEvent();
        // e.AddListener(() =>
        // {
            SaveLoadData.Ins.DataGame.Coin += (int)(randomnize.Reward * coin);
            SaveLoadData.Ins.Save();
            SceneManager.LoadScene("GamePlay");
        // });
        // //SkygoBridge.instance.ShowRewarded(e, null);
        //reward
        // ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);
    }
    public void SetEnd(float distance, float maxPath)
    {
        pathMoveText.text = Mathf.CeilToInt(distance).ToString() + " M";
        pathMoveSlider.maxValue = maxPath;
        pathMoveSlider.value = distance;
        textCoin.text = SaveLoadData.Ins.DataGame.Coin.ToString();
        coinReward.text = Mathf.CeilToInt(distance + SaveLoadData.Ins.DataGame.CoinRewardCoinUp).ToString();
        coin = Mathf.CeilToInt(distance + SaveLoadData.Ins.DataGame.CoinRewardCoinUp);
    }
}

using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
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
        SaveLoadData.Ins.DataGame.Coin += coin;
        SaveLoadData.Ins.Save();
        LevelManager.Ins.OnInit(SaveLoadData.Ins.DataGame.CurrenMotor, SaveLoadData.Ins.DataGame.CurrenLv);
        SceneManager.LoadScene("GamePlay");
    }
    void GetCoinRewardQC()
    {
        interacableImage.gameObject.SetActive(true);
        randomnize.StopTween();
        SaveLoadData.Ins.DataGame.Coin += (int)(randomnize.Reward * coin);
        SaveLoadData.Ins.Save();
        //LevelManager.Ins.OnInit(SaveLoadData.Ins.DataGame.CurrenMotor, SaveLoadData.Ins.DataGame.CurrenLv);
        objCoinRwQC.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        StartCoroutine(SetRewardQCCoin());
    }
    IEnumerator SetRewardQCCoin()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GamePlay");
    }
    public void SetEnd(float distance, float maxPath)
    {
        pathMoveText.text = Mathf.CeilToInt(distance).ToString() + " M";
        pathMoveSlider.maxValue = maxPath;
        pathMoveSlider.value = distance;
        textCoin.text = SaveLoadData.Ins.DataGame.Coin.ToString();
        coinReward.text = Mathf.CeilToInt(distance).ToString();
        coin = Mathf.CeilToInt(distance);
    }
}

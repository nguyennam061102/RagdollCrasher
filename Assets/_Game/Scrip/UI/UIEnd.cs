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
    [SerializeField] int coin;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        getCoin.onClick.AddListener(() =>
        {
            GetCoin();
        });

    }

    void GetCoin()
    {
        SaveLoadData.Ins.DataGame.Coin += coin;
        SaveLoadData.Ins.Save();
        LevelManager.Ins.OnInit(SaveLoadData.Ins.DataGame.CurrenMotor, SaveLoadData.Ins.DataGame.CurrenLv);
        //OpenNewUI<UIStart>();
        SceneManager.LoadScene("GamePlay");
    }
    public void SetEnd(float distance, float maxPath)
    {
        pathMoveText.text = Mathf.CeilToInt(distance).ToString();
        pathMoveSlider.maxValue = maxPath;
        pathMoveSlider.value = distance;
        textCoin.text = SaveLoadData.Ins.DataGame.Coin.ToString();
        coinReward.text = Mathf.CeilToInt(distance).ToString();
        coin = Mathf.CeilToInt(distance);
    }
}

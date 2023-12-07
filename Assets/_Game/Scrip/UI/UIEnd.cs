using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
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
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        getCoin.onClick.AddListener(() =>
        {
            GetCoin();
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetCoin()
    {
        SceneManager.LoadScene(0);
    }
}

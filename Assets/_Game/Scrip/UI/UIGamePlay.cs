
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] Slider slider;
    [SerializeField] Button skip;
    [SerializeField] TextMeshProUGUI textTutorial;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        skip.onClick.AddListener(() =>
        {
            ButtonSkip();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVelocity(float velocity)
    {
        velocityText.text = Mathf.CeilToInt(velocity).ToString() + " Km";
    }
    public void SetDistance(float distance)
    {
        distanceText.text = Mathf.CeilToInt(distance).ToString() + " m";
    }
    public void SetSlider(float value) 
    {
        slider.value = value;
    }
    void ButtonSkip()
    {
        LevelManager.Ins.CurrentMotor.RagdollController.SetSkip();
    }
    public void SetTutorial(bool tf)
    {
        if (tf)
        {
            textTutorial.text = "Tap And Hold To Move";
            textTutorial.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
            
        }
        else
        {
            textTutorial.text = "Tap TO Jump";
            textTutorial.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        }
        StartCoroutine(OffText());
    }
    IEnumerator OffText()
    {
        yield return new WaitForSeconds(2f);
        textTutorial.transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
    }
}

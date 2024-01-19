
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] Slider slider;
    [SerializeField] Button skip;
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
}

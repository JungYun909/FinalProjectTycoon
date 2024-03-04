using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopShopStatusWindow : UIBase
{
    // public TemporaryStat shopStat;
    // private string shopName;
    // private int goldOwned;
    // private int reputeOwned;
    // private float curTime;
    // private int curDate;

    [SerializeField] private TextMeshProUGUI shopTitle;
    [SerializeField] private TextMeshProUGUI curGold;
    [SerializeField] private TextMeshProUGUI curRepute;
    [SerializeField] private TextMeshProUGUI curDateText;
    [SerializeField] private Slider timeSlider;

    public override void Initialize()
    {
        shopTitle.text = GameManager.instance.dataManager.playerData.shopName;
        curGold.text = GameManager.instance.dataManager.playerData.money.ToString();
        curRepute.text = GameManager.instance.dataManager.playerData.fame.ToString();
        curDateText.text = GameManager.instance.dataManager.playerData.day.ToString();
        timeSlider.value = 1 - (GameManager.instance.dataManager.playerTimeData.time / GameManager.instance.statManager.dayTime);
    }
    private void Update()
    {
        timeSlider.value = 1 - (GameManager.instance.dataManager.playerTimeData.time / GameManager.instance.statManager.dayTime);
    }
    private void OnEnable()
    {
        Initialize();
        GameManager.instance.statManager.onStatChanged += UpdateUI;
        GameManager.instance.statManager.onDateChanged += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.instance.statManager.onStatChanged -= UpdateUI;
        GameManager.instance.statManager.onDateChanged -= UpdateUI;
    }
    public override void UpdateUI()
    {
	    curGold.text = GameManager.instance.dataManager.playerData.money.ToString();
        curRepute.text = GameManager.instance.dataManager.playerData.fame.ToString();
        curDateText.text = (GameManager.instance.dataManager.playerData.day).ToString();
    }
}

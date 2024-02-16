using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopShopStatusWindow : UIBase
{
    public TemporaryStat shopStat;
    private string shopName;
    private int goldOwned;
    private int reputeOwned;
    private float curTime;
    private int curDate;

    [SerializeField] private TextMeshProUGUI shopTitle;
    [SerializeField] private TextMeshProUGUI curGold;
    [SerializeField] private TextMeshProUGUI curRepute;
    [SerializeField] private TextMeshProUGUI curDateText;
    [SerializeField] private Slider timeSlider;

    public override void Initialize()
    {
        shopStat = FindObjectOfType<TemporaryStat>();
        shopName = shopStat.shopName;
        shopTitle.text = shopName;
        goldOwned = shopStat.gold;
        curTime = shopStat.playerTime;

        curGold.text = goldOwned.ToString();
        curRepute.text = reputeOwned.ToString();
        curDate = GameManager.instance.dataManager.playerData.day;
        curDateText.text = curDate.ToString();

        timeSlider.value = curTime;
    }
    private void Update()
    {
        timeSlider.value = GameManager.instance.statManager.maxTime;
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
        goldOwned = shopStat.gold;
	    curGold.text = goldOwned.ToString();
        curRepute.text = reputeOwned.ToString();
        curDateText.text = (GameManager.instance.dataManager.playerData.day - 1).ToString();
    }
}

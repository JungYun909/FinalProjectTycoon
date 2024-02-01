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

    [SerializeField] private TextMeshProUGUI shopTitle;
    [SerializeField] private TextMeshProUGUI curGold;
    [SerializeField] private TextMeshProUGUI curRepute;
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

        timeSlider.value = curTime;

    }

    public override void UpdateUI()
    {
        goldOwned = shopStat.gold;
	    curGold.text = goldOwned.ToString();
        curRepute.text = reputeOwned.ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TopShopStatusWindow : UIBase
{
    public TemporaryStat shopStat;
    private string shopName;
    private int goldOwned;

    [SerializeField] private TextMeshProUGUI shopTitle;
    [SerializeField] private TextMeshProUGUI curGold;

    public override void Initialize()
    {
        shopStat = FindObjectOfType<TemporaryStat>();
        shopName = shopStat.shopName;
        shopTitle.text = shopName;
        goldOwned = shopStat.gold;
        curGold.text = goldOwned.ToString();
    }

    public override void UpdateUI()
    {
        shopStat.gold -= shopStat.gold - shopStat.goldUsed;
        goldOwned = shopStat.gold;
        curGold.text = goldOwned.ToString();
    }
}

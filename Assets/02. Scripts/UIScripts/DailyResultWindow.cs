using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyResultWindow : UIBase
{
    public TextMeshProUGUI shopFameResultText;
    public TextMeshProUGUI financeScoreResultText;
    public TextMeshProUGUI goldOwned;
    public TextMeshProUGUI goldUsed;
    //public TextMeshProUGUI curGold;
    //public TextMeshProUGUI totalScore;

    public UIManager uiManager;
    public TemporaryStat shopStat;

    public override void Initialize()
    {
        shopStat = FindObjectOfType<TemporaryStat>(); 
	    shopFameResultText.text = shopStat.fame.ToString();
        financeScoreResultText.text = shopStat.financialScore.ToString();
        goldOwned.text = shopStat.gold.ToString();
        goldUsed.text = shopStat.goldUsed.ToString();
    }

    public override void UpdateUI()
    {
        shopStat = FindObjectOfType<TemporaryStat>();
        shopFameResultText.text = shopStat.fame.ToString();
        financeScoreResultText.text = shopStat.financialScore.ToString();
        goldOwned.text = shopStat.gold.ToString();
        goldUsed.text = shopStat.goldUsed.ToString();
    }

}

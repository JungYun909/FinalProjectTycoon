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

    
    
    private void OnEnable()
    {
        UpdateUI();
        Initialize();
    }
	
    public override void Initialize()
    {
        uiManager = FindObjectOfType<UIManager>();
        var shopStat = GameManager.instance.dataManager.playerData;
    }

    public override void UpdateUI()
    {
        //shopFameResultText.text = shopStat.fame.ToString();
        //financeScoreResultText.text = shopStat.warningCount.ToString();
        //goldOwned.text = shopStat.money.ToString();
        //goldUsed.text = shopStat.debt.ToString();
    }
    
    public void CloseWindow()
    {
        uiManager.DestroyUIObject(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyResultWindow : UIBase
{
    public TextMeshProUGUI goldCurrentlyInHand;
    public TextMeshProUGUI shopLevel;
    public TextMeshProUGUI earnedToday;
    public TextMeshProUGUI spentToday;
    public TextMeshProUGUI profit;
    public TextMeshProUGUI totalDebt;
    public TextMeshProUGUI repayment;
    public TextMeshProUGUI debtLeft;
    public TextMeshProUGUI warning;
    public TextMeshProUGUI comment;

    private PlayerData stat;
    
    private void OnEnable()
    {
        Initialize();
        UpdateUI();
    }
	
    public override void Initialize()
    {
        stat = GameManager.instance.dataManager.playerData;
    }

    public override void UpdateUI()
    {
        goldCurrentlyInHand.text = stat.money.ToString();
        shopLevel.text = stat.level.ToString();
        earnedToday.text = stat.goldEarnedToday.ToString();
        if(GameManager.instance.logicManager.paidAmount !=0)
            spentToday.text = stat.goldSpentToday.ToString() + $"(+{GameManager.instance.logicManager.paidAmount.ToString()})";
        else
            spentToday.text = stat.goldSpentToday.ToString();
        int netProfit = stat.goldEarnedToday - stat.goldSpentToday - GameManager.instance.logicManager.paidAmount;
        if (stat.goldEarnedToday - stat.goldSpentToday < 0)
            profit.text = $"<color=red>{netProfit}</color>";
        else
            profit.text = netProfit.ToString();
        totalDebt.text = (stat.debt + GameManager.instance.logicManager.paidAmount).ToString();
        repayment.text = GameManager.instance.logicManager.paidAmount.ToString();
        debtLeft.text = stat.debt.ToString();
        switch (stat.warningCount)
        {
            case -1:
                warning.text = $"<color=yellow>경고!</color>";
                comment.text = "이번엔 좀더 아끼는게 좋겠어요.";
                break;
            case -2:
                warning.text = $"<color=red>위험!</color>";
                comment.text = "혹시 어떤 문제가 있나요...?";
                break;
            default:
                warning.text = $"<color=green>양호</color>";
                comment.text = "잘 하고 있어요! GOOD!";
                break;
        }

        stat.goldEarnedToday = 0;
        stat.goldSpentToday = 0;
    }
    
    public void CloseWindow()
    {
        GameManager.instance.uiManager.DestroyUIObject(gameObject);
    }
}

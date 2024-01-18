using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryTestButtons : MonoBehaviour
{
    [SerializeField] public TemporaryStat shopStat;
    [SerializeField] public StatManager statManager;

    public void ClickButtonAndEarnMoney()
    {
        statManager.EarnGold(50);
        Debug.Log("Earned 50g");
    }

    public void ClickButtonAndSpendMoney()
    {
        if(shopStat.gold <=0)
        {
            Debug.Log("Unable to spend money!"); 
	    }
        statManager.SpendGold(50);
        Debug.Log("Spent 50g");

    }
}

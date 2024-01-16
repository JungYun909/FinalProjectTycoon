using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyResultWindow : UIBase
{
    public TextMeshPro shopFameResultText;
    public TextMeshPro financeScoreResultText;
    public TextMeshPro totalScore;

    public UIManager uiManager;

    private void Awake()
    {
        uiManager = new UIManager();
    }
    public override void Initialize()
    {
        //shopFameResultText =  // TODO; 저장/계산된 점수 불러오기 
    }

    public override void Show()
    {
        
    }

    public override void UpdateUI()
    {
        throw new System.NotImplementedException();
    }

        public override void Hide()
    {
        throw new System.NotImplementedException();
    }
}

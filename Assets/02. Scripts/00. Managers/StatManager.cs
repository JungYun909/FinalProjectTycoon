using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour            // 플레이어 (가게) 정보의 업데이트
{
    public TemporaryStat shopStat;
    
    public int shopLevel;     //가게 레벨. 가게 레벨에 따라 레시피/시설 해금 등이 필요하다면. 명성치, 재정현황 등의 지표가 특정 수준 이상일 때 ++ // 수식으로 관리
    public int shopFame;    //명성치, 가게 수준.
    public int interiorScore;   //인테리어 점수.
    public int financeScore;  //재정상태. 적자일수를 계산하여 초반에 -3처럼 특정 음수값이 되면 fail.
    public int currentGold;    //소지금. 변경가능.

    public int modFame;        //명성치의 변경값. 
    public int modInterior;     //인테리어 수치의 변경값. 가구/인테리어 요소의 점수를 받아와 합산/차감하는 식으로 이루어짐.
    public int modFinance;    //재정상태 수치의 변경값. 흑자일 때, 적자일 때 점수를 매김. 적자시에는 무조건 -1, 흑자일때는 +1을 반환하는 로직을 주고 
    public int modGold;

    public int curNpc;

    public int maxShopLevel;
    public int maxNpc;
    
    public delegate void OnStatChanged();       //스탯 변경시 관련 UI들이 업데이트 로직을 불러오기 위한 대리자 생성
    public event OnStatChanged onStatChanged;   //이벤트 선언


    public static StatManager instance;
    private void Awake()	    //TODO
    {
        instance = this;
        Initialize();
    }

    private void Initialize()
    {
        shopStat = GetComponent<TemporaryStat>();//TODO 현재는 같은 오브젝트에 붙여 사용하므로 GetComponent사용. 이후 StatManager가 MonoBehaviour가 아닐 때를 고민할 필요가 있음.   
        shopFame = shopStat.fame;
        financeScore = shopStat.financialScore;
        currentGold = shopStat.gold;
        modGold = shopStat.goldUsed;
    }

    // private void OnEnable()
    // {
    //     FindObjectOfType<UIManager>().OnDailyWindowOpen += UpdateDailyResult;     //이벤트 구독. TODO 현재는 싱글톤 게임매니저가 없으므로 씬에서 UIManager 찾아서 사용하도록 하였으나 추후 수정 필요
    // }
    //
    // private void OnDisable()
    // {
    //     UIManager uiManager = FindObjectOfType<UIManager>();     //이벤트 해지. TODO 싱글톤 게임매니저 제작 후 FindObjectOfType 대	
    //     
	   //  if(uiManager!=null)
    //     {
    //         uiManager.OnDailyWindowOpen -= UpdateDailyResult;
    //     }
    // }
    
    
    private void SaveStat()
    {
        //현재 스탯 정보를 json 파일로 저장하는 로직. 추후 정보저장용 스크립트로 따로 분리 필요.
    }

    private void UpdateStat()
    {
        // 현재 저장되어 있는 스탯을 UI로 보여주기 위한 로직 > 추후 StatusUIManager등으로 정리할 수 있음.
    }

    private void IncreaseInteriorScore()   // 매개변수는 itemSO나 스크립트, 메서드 등으로 인해 발생한 변경값. 설치시 증가하는 인테리어 점수. 클래스 명을 Furniture, InteriorFactor 등으로 정리하고 안에 .interiorScore등을 넣으면 될듯함.
    {
        //interiorScore += modInterior;
    }

    private void DecreaseInteriorScore()  // 매개변수는 itemSO나 스크립트, 메서드 등으로 인해 발생한 변경값. 설치된 아이템 회수에 따라 감소해야 하는 인테리어 점수. 클래스 명을 Furniture, InteriorFactor 등으로 정리하고 안에 .interiorScore등을 넣으면 될듯함.
    {

    }

    private int CalculateFame(int modFame)  // 매개변수는 명성치의 변경을 계산하는 메서드를 딜리게이트로 넣으면 될 듯함.
    {
        shopFame += modFame;
        shopStat.fame = shopFame;
        return shopFame;

    }

    private int CalculateFinanceScore(int modFinance)  // 재정점수 계산기. 재정점수는 
    {
        financeScore += modFinance;
        shopStat.financialScore = financeScore;
        //onStatChanged?.Invoke(); // 스탯이 변경될 때 이벤트 발생
        return financeScore;
    }
    public int EarnGold(int modGold)  // 돈을 벌었을 때 호출할 메서드. 매개변수는 판매가.    itemSO나 json 스크립트 내의 가격 정보를 받아오도록 함. 
    {
        currentGold += modGold;
        shopStat.gold = currentGold;
        //onStatChanged?.Invoke(); // 스탯이 변경될 때 이벤트 발생
        return currentGold;
    }

    public int SpendGold(int modGold)  // 돈을 쓸 때 호출한 메서드. 매개변수는 구매하는 아이템의 가격, 일 영업비용, 파견을 위한 종업원의 일급, 등. 
    {
        currentGold -= modGold;
        shopStat.gold = currentGold;
        //onStatChanged?.Invoke(); // 스탯이 변경될 때 이벤트 발생
        return currentGold;
    }

    private void UpdateDailyResult()
    {
        SpendGold(modGold);
    }
}

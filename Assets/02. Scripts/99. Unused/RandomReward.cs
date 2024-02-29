using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomReward : MonoBehaviour
{
    [SerializeField] private Image errorImage;
    [SerializeField] private Image rewardImage;
    [SerializeField] private TMP_Text rewardTxt;
    [SerializeField] private TMP_Text spendGoldTxt;
    [SerializeField] private Button timerBtn;
    [SerializeField] private Button originBtn;
    [SerializeField] private Image deliveryImage;
    private int rewardGold;
    private int deliveryPrice = 1000;

    private List<ItemSO> rewardItems = new List<ItemSO>();

    private void Start()
    {
        foreach (ItemSO item in GameManager.instance.inventoryManager.itemDatabase.itemDataList)
        {
            if(item.type == 1)
            {
                rewardItems.Add(item);
            }
        }

        if(GameManager.instance.dataManager.playerData.deliveryStart == true)
        {
            timerBtn.gameObject.SetActive(true);
            originBtn.gameObject.SetActive(false);
        }
    }

    public void GoldReward()// 골드 리워드 선택시 랜덤골드 증정
    {
        rewardGold = Random.Range(GameManager.instance.dataManager.playerData.level* deliveryPrice / 2, GameManager.instance.dataManager.playerData.level * deliveryPrice  * 3);
        rewardTxt.text = rewardGold.ToString() + "냥";
        GameManager.instance.statManager.EarnGold(rewardGold);
        rewardImage.sprite = Resources.Load<Sprite>(ResourcePath.goldImage + "coin");//골드이미지 추가
    }

    public void RareItemReward()
    {
        //희귀아이템 나오게하기
    }

    public void ingredientReward()
    {
        int itemID = Random.Range(0, rewardItems.Count - 1);
        ItemSO itemSO = rewardItems[itemID];
        //기본재료중 랜덤으로 하나 나오게 하기
        GameManager.instance.inventoryManager.AddItemToInventory(1000, itemSO, 1);
        
        rewardImage.sprite = itemSO.sprite;
        rewardTxt.text = itemSO.itemName;
    }

    //버튼 3개정도 더만들어서 더다양한 보상 나오게 할래 그때 추가해

    public void BuyReward()// 배달시스템이용시 드는 돈 마이너스
    {
        if (GameManager.instance.dataManager.playerData.money < GameManager.instance.dataManager.playerData.level * deliveryPrice)
        {
            errorImage.gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.statManager.SpendGold(GameManager.instance.dataManager.playerData.level * deliveryPrice);
            originBtn.gameObject.SetActive(false);
            timerBtn.gameObject.SetActive(true);
            deliveryImage.gameObject.SetActive(false);
            GameManager.instance.dataManager.playerData.deliveryStart = true;
            GameManager.instance.dataManager.SaveData();
        }
    }

    public void UpdateGoldTxt()
    {
        spendGoldTxt.text = (GameManager.instance.dataManager.playerData.level * deliveryPrice).ToString() +"냥";
    }

    public void DeliveryInitSet()
    {
        GameManager.instance.dataManager.playerData.deliveryClear = false;
        GameManager.instance.dataManager.SaveData();
    }
}

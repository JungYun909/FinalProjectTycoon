using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestSA3 : MonoBehaviour
{
    [SerializeField] private Image rewardImage;
    [SerializeField] private TMP_Text rewardTxt;
    private int rewardGold;

    public void GoldReward()// 골드 리워드 선택시 랜덤골드 증정
    {
        rewardGold = Random.Range(1000, 10000);
        rewardTxt.text = rewardGold.ToString() + "골드";
        GameManager.instance.statManager.EarnGold(rewardGold);
        rewardImage.sprite = Resources.Load<Sprite>(ResourcePath.goldImage + "c39");//골드이미지 추가
    }

    public void RareItemReward()
    {
        //희귀아이템 나오게하기
    }

    public void ingredientReward()
    {
        int itemID = Random.Range(1, 9);
        ItemSO itemSO = Resources.Load<ItemSO>(ResourcePath.ItemSo + itemID);
        //기본재료중 랜덤으로 하나 나오게 하기
        GameManager.instance.inventoryManager.AddItemToInventory(itemID, itemSO, 1);
        GameObject.Find("PlayerInventoryUI(Clone)").GetComponent<PlayerInventoryUI>().UpdateUI();
        rewardImage.sprite = itemSO.sprite;
        rewardTxt.text = itemSO.itemName;
    }

    //버튼 3개정도 더만들어서 더다양한 보상 나오게 할래 그때 추가해

    public void BuyReward(int gold)// 배달시스템이용시 드는 돈 마이너스
    {
        GameManager.instance.statManager.SpendGold(gold);
    }
}

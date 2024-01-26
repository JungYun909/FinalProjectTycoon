using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSetting : MonoBehaviour
{
    [SerializeField] private NpcDatabaseSO npcDatabaseSO;
    [SerializeField] private System.Random random;

    public NpcSO npcSo;
    [SerializeField] int npcDatanum;
    [SerializeField] private List<int> favoriteFood;
    public int selectedFavoriteFoodID; // 최종으로 결정된 선택 음식

    void Awake()
    {
        random = new System.Random();

        // 이하 차후 onEnalbe()로 옮길 것
        npcDatanum = random.Next(0, npcDatabaseSO.npcDataList.Count); ;

        npcSo = npcDatabaseSO.npcDataList[npcDatanum];

        SelectFaveoriteFood();

    }
    void SelectFaveoriteFood()
    {
        int num;
        favoriteFood = npcSo.favoriteFood;

        if (favoriteFood.Count > 1)
        {
            num = random.Next(0, favoriteFood.Count - 1);
        }

        else
        {
            num = 0;
        }

        selectedFavoriteFoodID = favoriteFood[num];

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectBtn : MonoBehaviour
{
    public Tilemap originTilemap;
    //public GameObject exPrefab;
    // selectBtn활성화 시 연결해줄 함수
    public void OffUI()
    {
        gameObject.SetActive(false);
    }

    public void InstallExObj(Sprite sprite)
    {
        //GameObject exObj = Instantiate(exPrefab, new Vector3(0, 0), Quaternion.identity);
        //exObj.GetComponent<MachineMoveController>().tilemap = originTilemap;
        UIManagerTemp.instance.OnUI();
        UIManagerTemp.instance.UIImageChange(sprite);
    }
}

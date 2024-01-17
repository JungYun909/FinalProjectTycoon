using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Installation : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    
    [SerializeField] private GameObject prefab;
    public GameObject test;
    //private Vector2 mousePosition = Vector2.zero;
    //private bool isBuild;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        //InputManager.instance.OnClickEvent += Sooah;
        //InputManager.instance.OffClickEvent += IsBuildToggle;
    }

    // Update is called once per frame
    private void Update()
    {
        //if (test == null)
        //{
        //    return;
        //}
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //test.transform.position = tilemap.WorldToCell(mousePosition);
    }

    public void Install()
    {
        test = Instantiate(prefab);
        test.transform.position = new Vector3(0, 0);
        test.layer = 0;
        test.GetComponent<ObjectInstallation>().tilemap = tilemap;
        
    }

    

    //public void Delete(GameObject gameObject)
    //{
    //    Destroy(gameObject);
    //}

    //public void Sooah()
    //{
    //    if(isBuild == false)
    //    {
    //        isBuild = true;
    //        if (test == null)
    //        {
    //            return;
    //        }
    //        //// 레이캐스트를 통해 충돌 정보 가져오기
    //        //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, targetLayer);
    //        //if (hit.collider != null)
    //        //{
    //        //    Delete(hit.collider.gameObject);
    //        //    return;
    //        //}
    //        test.transform.position = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //        Instantiate(prefab, test.transform.position, Quaternion.identity);
    //    }
    //}

    //public void IsBuildToggle()
    //{
    //    if(isBuild)
    //    {
    //        isBuild = false;
    //    }
    //}

    //public void Delete2()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, targetLayer);
    //    if (hit.collider != null)
    //    {
    //        Delete(hit.collider.gameObject);
    //        return;
    //    }
    //    Delete(prefab);
    //}

    
}

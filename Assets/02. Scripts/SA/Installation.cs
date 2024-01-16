using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Installation : MonoBehaviour
{
    //설치할 설치물을 클릭했을때 놓을수 있는 영역 표시, 놓을수 있는 영역에 클릭하면 설치되게하기
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject prefab;
    private GameObject test;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test == null)
        {
            return;
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        test.transform.position = tilemap.WorldToCell(mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치로 레이를 발사
            
            Debug.Log("1");

            // 레이캐스트를 통해 충돌 정보 가져오기
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, targetLayer);
            Debug.Log("2");
            //충돌한 오브젝트의 정보 출력
            
            Debug.Log("4");
            if (hit.collider != null)
            {
                Debug.Log("3");
                return;
            }
            test.transform.position = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Instantiate(prefab, test.transform.position, Quaternion.identity);
        }
    }

    public void Install()
    {
        test = Instantiate(prefab);
        test.layer = 0;
    }
}

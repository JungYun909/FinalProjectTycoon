using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class ObjectInstallation : MonoBehaviour
{
    public Tilemap tilemap;
    private LayerMask targetLayer;
    [SerializeField] private GameObject RealPrefab;
   
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
        
    }

    public void CreateObj()
    {

        Instantiate(RealPrefab, gameObject.transform.position, Quaternion.identity);
    }

    public void DeleteObj()
    {
        
    }

    public void MoveObj()
    {

    }

    

    

    
}

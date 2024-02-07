using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestSA2 : MonoBehaviour
{
    private TMP_Text timeTxt;
    private float time = 10f;
    [SerializeField] private GameObject rewardBtn;

    private void Awake()
    {
        timeTxt = GetComponentInChildren<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf && time > 0)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
        if (time <= 0)
        {
            timeTxt.text = "00:00";
            gameObject.SetActive(false);
            rewardBtn.SetActive(true);
            return;
        }
    }
}

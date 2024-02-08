using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestSA2 : MonoBehaviour
{
    private TMP_Text timeTxt;
    private float sec = 10;
    private int min = 0;

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
        Timer();
    }

    private void Timer()
    {
        if (gameObject.activeSelf)
        {
            sec -= Time.deltaTime;
            
            timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
            if((int)sec < 0)
            {
                min--;
                sec = 59;
            }
        }
        if (min <= 0 && sec <= 0)
        {
            timeTxt.text = "00:00";
            gameObject.SetActive(false);
            rewardBtn.SetActive(true);
            sec = 10;
            return;
        }
    }
}

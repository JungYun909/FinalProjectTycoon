using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSceneData : MonoBehaviour
{
    public TextMeshProUGUI day;
    public TextMeshProUGUI totalEarned;
    public TextMeshProUGUI level;
    public TextMeshProUGUI deptLeft;
    private PlayerData stat;
    
    // Start is called before the first frame update
    void Start()
    {
        stat = GameManager.instance.dataManager.playerData;
        day.text = stat.day.ToString();
        totalEarned.text = stat.totalGoldEarned.ToString();
        level.text = stat.level.ToString();
        deptLeft.text = stat.debt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

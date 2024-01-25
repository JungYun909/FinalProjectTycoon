using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MachineData00", menuName = "SO by BW/Machine", order = 2)]
public class MachineSO : ScriptableObject
{
    public int id; //machine ID (호출 넘버)
    public string machinename; //출력할 기계 이름
    public string description; //출력할 기계 설명
    public int slot; // 가지고 있는 슬롯 수
}

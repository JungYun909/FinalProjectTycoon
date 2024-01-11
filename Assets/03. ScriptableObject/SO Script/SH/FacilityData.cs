using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MachineType
{
    Production,
    Sale
}
public class FacilityData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public MachineType type;
    public Sprite icon;
}

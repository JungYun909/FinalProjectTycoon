using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSpawnBtnController : MonoBehaviour
{
    public void OnSpawnBtn(GameObject prefab)
    {
        Instantiate(prefab, Vector2.zero, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DestinationData
{
    public int controllerID;
    public int connectedControllerID;

    public DestinationData(int fromId, int toId)
    {
        controllerID = fromId;
        connectedControllerID = toId;
    }
}

[System.Serializable]
public class DestinationWrapper
{
    public List<DestinationData> destinations;
}

public class DestinationManager : MonoBehaviour
{
    private int destinationControllerID = 1;
    public Dictionary<int, InstallationController> destinationDictionary = new Dictionary<int, InstallationController>();
    public List<DestinationData> destinationInfo = new List<DestinationData>();
    public int RegisterInstallationDestinationController(InstallationController controller)
    {
        int destinationID;
        destinationID = destinationControllerID++;
        destinationDictionary[destinationID] = controller;
        return destinationID;
    }

    private void Start()
    {
        LoadDestinationsData();
        StartCoroutine(SaveAllDestinationsRoutine());
    }

    public void RegisterDestinationInfo(int fromID, int toID)
    {
        DeleteDestinationInfo(fromID);
        DestinationData data = new DestinationData(fromID, toID);
        destinationInfo.Add(data);
    }

    public void DeleteDestinationInfo(int fromID)
    {
        List<DestinationData> toDelete = new List<DestinationData>();
        foreach (var item in destinationInfo)
        {
            if(item.controllerID == fromID)
            {
                toDelete.Add(item);
                break;
            }
        }
        foreach (var destinationToDelete in toDelete)
        {
            destinationInfo.Remove(destinationToDelete);
        }
    }

    public InstallationController GetDestinationGameObject(int id)
    {
        if (destinationDictionary.TryGetValue(id, out InstallationController controller))
        {
            return controller;
        }
        else
        {
            return null;
        }
    }

    IEnumerator SaveAllDestinationsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            GameManager.instance.dataManager.SaveAllDestinationData(destinationInfo);
        }
    }

    private void LoadDestinationsData()
    {
        List<DestinationData> loadedDestinations = GameManager.instance.dataManager.LoadAllDestinationData();
        if (loadedDestinations != null && loadedDestinations.Count > 0)
        {
            destinationInfo = loadedDestinations;
        }
        else
            return;
    }

}

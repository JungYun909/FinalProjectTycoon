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

    public void RegisterDestinationInfo(int fromID, int toID)
    {
        Debug.Log("RegisterDestinationInfo Called");
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
                return;
            }
        }
        foreach (var destinationToDelete in toDelete)
        {
            destinationInfo.Remove(destinationToDelete);
        }
        Debug.Log("Info deleted");
    }

    public InstallationController GetDestinationGameObject(int id)
    {
        if (destinationDictionary.TryGetValue(id, out InstallationController controller))
        {
            return controller;
        }
        else
        {
            Debug.LogError("Destination GameObject not found.");
            return null;
        }
    }
}

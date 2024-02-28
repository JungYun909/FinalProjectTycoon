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
    public int nextDestinationID;
    public List<DestinationData> destinations;
}

public class DestinationManager : MonoBehaviour
{
    private int destinationControllerID;
    public Dictionary<int, InstallationController> destinationDictionary = new Dictionary<int, InstallationController>();
    public List<DestinationData> destinationInfo = new List<DestinationData>();
    
    public int RegisterDestinationID(InstallationController controller)
    {
        int destinationID;
        if (controller._installationData.id == 0)    // 문
            destinationID = controller.destinationID = 1;
        else if (controller._installationData.id == 5)
            destinationID = controller.destinationID - 1;
        else if (controller.destinationID != 0)      // 이미 설치되어 destinationID가 부여받은 상태일 때 > 즉, 설치물 로드가 완료된 시점에서 RegisterDestinationID를 실행해야 함.
            destinationID = controller.destinationID;
        else
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
            yield return new WaitForSeconds(10f);
            GameManager.instance.dataManager.SaveAllDestinationData(destinationControllerID, destinationInfo);
        }
    }

    private void LoadDestinationsData()
    {
        Debug.Log("Destination Loaded");
        DestinationWrapper destinationWrapper = GameManager.instance.dataManager.LoadAllDestinationData();
        if (destinationWrapper != null)
        {
            destinationControllerID = destinationWrapper.nextDestinationID;
            destinationInfo = destinationWrapper.destinations;
        }
        else
            return;
    }

}

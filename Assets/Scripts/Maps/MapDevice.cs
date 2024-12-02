using UnityEngine;
/// <summary>
/// This class will allow maps to be placed inside of it and allow the player to activate the map device to open a map.
/// </summary>
public class MapDevice : MonoBehaviour
{
    private static GameManager Instance;
    private Map map;

    private void Start()
    {
        Instance = GameManager.Instance;
    }

    private void OpenMapDevice() // This method will be called when the player interacts with the map device
    {
        // Open the map device UI
    }

    private void CloseMapDevice() // This method will be called when the player closes the map device UI
    {
        // Close the map device UI
    }

    private void InsertMap(Map newMap) // This method will be called when the player inserts a map into the map device
    {
        map = newMap;
    }

    private void RemoveMap() // This method will be called when the player removes a map from the map device
    {
        map = null;
    }

    private void ActivateMapDevice() // This method will be called when the player activates the map device
    {
        if (map == null)
        {
            Debug.Log("No map inserted");
            return;
        }
        // enter the mapGenerator method but with the correct parameters
        Instance.EnterMap(map);
    }

}

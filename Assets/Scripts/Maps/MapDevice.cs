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

    private void OpenMapDevice()
    {

    }

    private void CloseMapDevice()
    {

    }
  
    private void InsertMap()
    {

    }

    private void RemoveMap()
    {

    }

    private void ActivateMapDevice()
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

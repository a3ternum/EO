using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// This class will allow maps to be placed inside of it and allow the player to activate the map device to open a map.
/// </summary>
public class MapDevice : MonoBehaviour
{
    private static GameManager Instance;
    private Map map;
    private MapDeviceUI mapDeviceUI;
    private Player player;
    private Tilemap tilemap;

    private Vector2 mapDevicePosition;
    private void Start()
    {
        Instance = GameManager.Instance;
        mapDeviceUI = FindFirstObjectByType<MapDeviceUI>();

        Invoke("FindPlayer", 0.1f);

        // convert tile position to world position
        tilemap = GetComponent<Tilemap>();

        Vector3Int cellPosition = FindActiveTilePosition();
        if (cellPosition != Vector3Int.zero)
        {
            mapDevicePosition = tilemap.GetCellCenterWorld(cellPosition);
        }

        // for the sake of being able to use a map before having the map items being implemented yet
        Map tempMap = FindFirstObjectByType<Map>();
        InsertMap(tempMap);
    }

    private Vector3Int FindActiveTilePosition()
    {
        // Iterate through all the positions in the tilemap to find the active tile
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                return pos;
            }
        }
        return Vector3Int.zero; // Return zero if no active tile is found
    }
    private void FindPlayer()
    {
        player = FindFirstObjectByType<Player>();
    }

    private void OnMouseDown()
    {
        Debug.Log("distance is " + Vector2.Distance(player.transform.position, mapDevicePosition));
        if (Vector2.Distance(player.transform.position, mapDevicePosition) >= 4f)
        {
            return;
        }

        OpenMapDevice();
    }



    private void OpenMapDevice() // This method will be called when the player interacts with the map device
    {
        // Open the map device UI
        mapDeviceUI.OpenUI();
    }

    private void CloseMapDevice() // This method will be called when the player closes the map device UI
    {
        // Close the map device UI
        mapDeviceUI.CloseUI();
    }

    private void InsertMap(Map newMap) // This method will be called when the player inserts a map into the map device
    {
        if (map != null)
        {
            RemoveMap();
        }
        
        map = newMap;
        mapDeviceUI.SetMapImage(map.mapImage);

        //mapImage = map.mapImage;

        //// Call the event or method when the item is added
        ////OnItemAdded?.Invoke(currentItem);

        //// Optionally, set the item's parent to the box, making it a child in the hierarchy
        //currentItem.transform.SetParent(boxImage.transform, false);
        //currentItem.transform.localPosition = Vector3.zero; // Center it inside the box

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
        Debug.Log("entering map");
        Debug.Log("map is " + map);
        // enter the mapGenerator method but with the correct parameters
        Instance.EnterMap(map);
    }

    private void Update()
    {
        // check if the player is close enough to the map device
        if (Instance != null)
        {
            //Debug.Log("player is null is " + player == null);
            //Debug.Log("instance is null is " + Instance == null);
            if (player != null && Vector2.Distance(player.transform.position, mapDevicePosition) >= 4f)
            {
                CloseMapDevice();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateMapDevice();
        }


    }



}

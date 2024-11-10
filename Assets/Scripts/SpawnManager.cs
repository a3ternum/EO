using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public void Start()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

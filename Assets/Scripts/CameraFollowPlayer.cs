using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Player player;

    private void Start()
    {

        player = FindPlayer();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    private Player FindPlayer()
    {
        return FindFirstObjectByType<Player>();
    }
}

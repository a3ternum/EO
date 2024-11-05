using UnityEngine;

public class weapon : MonoBehaviour
{
    private Transform weaponLocation;
    public GameObject playerProjectile; 

    
    public void Attack() // Attack method (shooting logic)
    {
        // get sword transform
        weaponLocation = GetComponent<Transform>();

        // play attack animation


        // spawn wind slash
        Instantiate(playerProjectile, weaponLocation.position, weaponLocation.rotation);

        // shoot projectile

    }

}

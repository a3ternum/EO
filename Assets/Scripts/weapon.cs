using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform weaponLocation;
    public GameObject windSlashPrefab; 

    
    public void Attack() // Attack method (shooting logic)
    {
        // get sword transform
        weaponLocation = GetComponent<Transform>();

        // play attack animation


        // spawn wind slash
        Instantiate(windSlashPrefab, weaponLocation.position, weaponLocation.rotation);

        // shoot projectile

    }

}

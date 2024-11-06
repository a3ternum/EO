using System;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Transform firePoint;
    public Transform playerTransform;
    public float weaponDistance = 0.5f;

    public weapon currentWeapon;

    public void playerAttack()
    {  
            // rotate firePoint to face mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // get direction vector from the character to mouse position
            Vector3 direction = (mousePosition - firePoint.position).normalized;

            // calculate the angle between the character and the mouse position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // change firepoint position
            firePoint.position = playerTransform.position + direction * weaponDistance;

            // rotate the weapon to have pummel face character
            firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);

            currentWeapon.Attack();
    }

    public Boolean attackInput()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }



}

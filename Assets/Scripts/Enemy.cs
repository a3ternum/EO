using System;
using UnityEngine;

public class Enemy : Creature
{
    private Boolean inAttackRange = false;
    protected AIChase chaseScript;

    public Transform firePoint;
    public float weaponDistance = 0.5f;

    public GameObject projectile;

    private Transform playerTransform;


    // I will use an enumeration to represent different chase states
    // Each of these states will have a different behavior
    // Chase, Flee, Patrol, Circle, etc


    protected override void Start()
    {
        base.Start(); 
        chaseScript = gameObject.AddComponent<AIChase>();
        chaseScript.setPlayer(FindFirstObjectByType<Player>());

        playerTransform = FindFirstObjectByType<Player>().transform;

        // place firepoint to the right of the character
        firePoint.position = this.transform.position + Vector3.right * weaponDistance;

    }

    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
    }

    public virtual void enemyAttack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // rotate firePoint to face the player
            Vector3 playerPosition = Camera.main.ScreenToWorldPoint(playerTransform.position);
            playerPosition.z = 0;

            // get direction vector from the enemy to the player's position
            Vector3 direction = (playerPosition - firePoint.position).normalized;

            // calculate the angle between the enemy and the player's position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // change firepoint position
            firePoint.position = this.transform.position + direction * weaponDistance;

            // rotate the weapon to have pummel face character
            firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
    }

}

using UnityEngine;

public abstract class MeleeAttack : Attack
{
    // technically only setting isMelee is relevant here.
    private bool isMelee = true;
    public float KnockbackForce { get; protected set; }
    public float Range { get; protected set; }

    protected GameObject ourWeapon;
    protected GameObject targets { get; set; }
    protected Vector2 originalHitLocation;

    protected override void Start()
    {
        base.Start();
        ourWeapon = transform.Find("firePoint/Weapon")?.gameObject;
        if (ourWeapon != null)
        {
            animator = ourWeapon.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogError("Weapon object not found on " + gameObject.name);
        }

    }



}

using UnityEngine;

public class RageVortexProjectile : Projectile
{

    protected override void Awake()
    {
        base.Awake();
        destroyOnHit = false;
        animator = GetComponent<Animator>();
    }


    // vortexProjectile uses the exact same logic as Projectile class
    // so we don't need to add any additional code here

}

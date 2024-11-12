using UnityEngine;

public class VortexProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        destroyOnHit = false;
    }

    // vortexProjectile uses the exact same logic as Projectile class
    // so we don't need to add any additional code here
    
}

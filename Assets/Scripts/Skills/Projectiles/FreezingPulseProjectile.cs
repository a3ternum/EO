using UnityEngine;

public class FreezingPulseProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        destroyOnHit = false;
    }

}

using UnityEngine;

public class FrostboltProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        destroyOnHit = false;
    }
}

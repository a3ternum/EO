using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class MeleeAttack : Attack
{
    // technically only setting isMelee is relevant here.
    private bool isMelee = true;
    public float KnockbackForce { get; protected set; }



    protected override void Start()
    {
      base.Start();
    }

    

}

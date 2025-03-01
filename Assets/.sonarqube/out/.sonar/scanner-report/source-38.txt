using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class MeleeAttack : Attack
{
    // technically only setting isMelee is relevant here.
    public bool isMelee { get; protected set; }
    public float KnockbackForce { get; protected set; }




    

}

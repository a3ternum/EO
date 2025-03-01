using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Skill activeSkill;

    public void SetActiveSkill(Skill skill)
    {
        activeSkill = skill;
    }

    public void playerAttack()
    {
        if (!enabled) // if the script is disabled, return
        {
            return;
        }
        if (activeSkill != null)
        {
            activeSkill.ActivateSkill();
        }
        else
        {
            // implement basic attack?
        }
    }

    public Boolean attackInput()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }



}

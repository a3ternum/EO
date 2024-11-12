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
        Debug.Log("trying attack");
        if (activeSkill != null)
        {
            Debug.Log("active skill detected" + activeSkill.skillName);
            activeSkill.ActivateSkill();
        }
        else
        {
            Debug.Log("no active skill");
            // implement basic attack?
        }
    }

    public Boolean attackInput()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }



}

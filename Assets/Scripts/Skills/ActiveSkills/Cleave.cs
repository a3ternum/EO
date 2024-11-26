using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : MeleeAttackArea
{
    private SkillData skillData;
    private float coneAngle = 90f;
    private CleaveCircleMesh cleaveVisual;

    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        InitializeSkill();
        cleaveVisual = Resources.Load<CleaveCircleMesh>("CleaveVisual");

    }


    protected override void Start()
    {
        base.Start();
    }

    public override void InitializeSkill()
    {
        skillName = "Cleave";
        tags = new HashSet<string> { "Melee", "Area", "Physical" };
        skillData = ScriptableObject.CreateInstance<SkillData>();

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 15f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 25f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 35f, 0f, 0f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.attackSpeedPerLevel = new List<float> { 1f, 1f, 1.1f, 1.2f, 1.3f };
        skillData.durationPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.radiusPerLevel = new List<float> { 2, 2.2f, 2.4f, 2.6f, 2.8f };

        int skillLevel = 0;
        if (skillData == null)
        {
            return;
        }
        damage = skillData.damagePerLevel[skillLevel];
        attackSpeed = skillData.attackSpeedPerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];
        radius = skillData.radiusPerLevel[skillLevel];
    }



    protected override IEnumerator SkillCoroutine()
    {
        OnActivate();
        float playerAttackSpeed = user.currentAttackSpeed;
        animationDuration = CalculateAnimationDuration(playerAttackSpeed, attackSpeed);

        if (animator != null) // play animation only if attack has an animation
        {
            animator.speed = attackSpeed * playerAttackSpeed; // Set the animation speed based on the player's attack speed and the attack's attack speed
            animator.SetTrigger("Attack"); // Play the attack animation
        }
        user.canMove = false;
        // spawn cleave visual
        CleaveCircleMesh cleave = Instantiate(cleaveVisual, user.transform.position, Quaternion.identity);

        // change rotation to fit mouse position relative to user
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - user.transform.position).normalized;
        cleave.user = user;
        cleave.skill = this;
        cleave.UpdatePositionAndRotation(user.transform.position, direction);
        yield return new WaitForSeconds(animationDuration);
        Destroy(cleave.gameObject);
        user.canMove = true;
        if (animator != null)
        {
            animator.SetTrigger("AttackFinished"); // Finish the attack animation

        }



    }

    protected override List<Creature> AoECollider(Vector2 mousePosition)
    {
        List<Creature> hitCreatures = new List<Creature>();
        Vector2 userPosition = user.transform.position;

        Vector2 userForward = (mousePosition - userPosition).normalized;

        // Get all potential targets within the radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(userPosition, radius);

        foreach (var collider in colliders)
        {
            if ((collider.gameObject.layer == enemyLayer && user is Player) || (collider.gameObject.layer == playerLayer && user is Enemy))
            {
                Creature creature = collider.GetComponent<Creature>();
                if (creature != null && creature != user)
                {
                    Vector2 directionToCreature = (Vector2)creature.transform.position - userPosition;
                    float angleToCreature = Vector2.Angle(userForward, directionToCreature);

                    // Check if the creature is within the cone angle
                    if (angleToCreature <= coneAngle / 2)
                    {
                        hitCreatures.Add(creature);
                    }
                }
            }
        }

        // Update the visual effect

        return hitCreatures;
    }


}
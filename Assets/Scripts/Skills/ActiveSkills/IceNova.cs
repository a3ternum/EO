using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class IceNova : OffensiveSpell
{
    // Ice nova is a skill that casts a nova of ice around the player cursor position (up to a certain range)

    private SkillData skillData;
    protected IceNovaObject iceNovaPrefab;


    protected override void Awake() // Initialize Sweep skill data
    {
        base.Awake();
        skillName = "Ice Nova";

        iceNovaPrefab = Resources.Load<IceNovaObject>("IceNovaObject");

        skillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Area", "Cold", "Spell" };

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 0f, 0f, 20f, 0f, 0f },
            new float[] { 0f, 0f, 24f, 0f, 0f },
            new float[] { 0f, 0f, 28f, 0f, 0f },
            new float[] { 0f, 0f, 32f, 0f, 0f },
            new float[] { 0f, 0f, 36f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.radiusPerLevel = new List<float> { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        skillData.castSpeedPerLevel = new List<float> { 1.5f, 1.46f, 1.42f, 1.38f, 1.34f };
        skillData.durationPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.rangePerLevel = new List<float> { 4f, 4f, 4f, 4f, 4f };
    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (skillData == null)
        {
            return;
        }
        damage = skillData.damagePerLevel[skillLevel];
        radius = skillData.radiusPerLevel[skillLevel];
        castSpeed = skillData.castSpeedPerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];
        duration = skillData.durationPerLevel[skillLevel];
        range = skillData.rangePerLevel[skillLevel];
    }

    protected override IEnumerator ActivateSkillCoroutine()
    {
        yield return base.ActivateSkillCoroutine();
        SpawnIceNova(DetermineTargetLocation());
    }

    protected void SpawnIceNova(Vector2 location)
    {
        // Spawn the ice nova at the location
        IceNovaObject iceNova = Instantiate(iceNovaPrefab, location, Quaternion.identity);
        iceNova.radius = radius;
        iceNova.duration = duration;

        List<Creature> creaturesHit = AoECollider(location);
        ApplyDamageAndEffects(creaturesHit);

    }

    protected override Vector2 DetermineTargetLocation()
    {
        if (user is Player)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // make sure that we take into account the range of the skill
            // if the target position is out of range we should set it to the maximum range
            // use the range of the skill to determine the target position
            Vector2 direction = targetPosition - (Vector2)user.transform.position;
            if (direction.magnitude > range)
            {
                direction = direction.normalized * range;
                targetPosition = (Vector2)user.transform.position + direction;
            }


        }
        else if (user is Enemy)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player is null");
                return Vector2.zero;
            }
            else
            {
                targetPosition = player.transform.position;
            }
        }
        return targetPosition;
    }


}

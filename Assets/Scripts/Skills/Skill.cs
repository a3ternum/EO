using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
// This becomes the parent class for every skill in the game.
public class Skill : MonoBehaviour
{
    // skills have the following properties:
    public Creature user { get; set; } // the user of the skill
    public string skillName { get; protected set; } // the name of the skill
    public string skillDescription { get; protected set; } // the description of the skill
    public int skillLevel { get; protected set; } // the level of the skill, 
    public string[] skillTags { get; protected set; } // the tags of the skill (e.g. "Fire", "Ice", "Melee")
    public float[] damage { get; protected set; } // the damage of the skill
    public float castSpeed { get; protected set; } // the cast time of the skill
    public float attackSpeed { get; protected set; } // the base attack speed of the skill
    public float duration { get; protected set; } // the duration of the skill
    public float tickRate { get; protected set; } // the tick rate of the skill
    public int quality { get; protected set; } // the quality of the skill
    public bool isReady { get; protected set; } // boolean to indicate whether skill can be used right now 
    public float manaCost { get; protected set; } // the mana cost of the skill
    public float strikeRange { get; protected set; } // the strike Range of the skill
    public float projectileSpeed { get; protected set; } // the speed of the projectile
    public float radius { get; protected set; } // the radius of the skill
    public float range { get; protected set; } // the range of the skill
    public int pierceCount { get; protected set; } // the number of enemies the skill can pierce
    public int enemyLayer { get; protected set; } // the layer mask of the enemy
    public int terrainLayer { get; protected set; } // the layer mask of the terrain
    public int playerLayer { get; protected set; } // the layer mask of the player
    public float animationDuration { get; protected set; } // the duration of the animation

    public Animator animator { get; protected set; } // the animator of the skill
    protected Vector2 targetPosition { get; set; } // the location where the skill is targeted

    protected float areaOfAttackIncrease;

    public HashSet<string> tags { get; protected set; } // the tags of the skill (e.g. "Fire", "Ice", "Melee")
    public Dictionary<Creature, float> lastHitTime { get; protected set; }

    [SerializeField]
    private Sprite skillIcon; // the icon of the skill to display in UI.
    
    protected float cooldownTimer = 0; // the cooldown timer of the skill
    protected bool isActivating = false; // boolean to indicate whether the skill is activating
    protected virtual void Awake()
    {
        lastHitTime = new Dictionary<Creature, float>();
    }


    protected virtual void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        terrainLayer = LayerMask.NameToLayer("Terrain");
        playerLayer = LayerMask.NameToLayer("Player");

        if (user != null)
            areaOfAttackIncrease = user.currentAreaOfEffectIncrease;

    }

    public virtual void InitializeSkill() // child classes will use this method to initialize the skill
    {
        
    }

    public virtual void ActivateSkill()
    {
        if (isActivating) // prevent multiple activations of the same skill
        {
            return;
        }
        bool canActivate = CanActivate();
        if (canActivate)
        {
            OnActivate();
            StartCoroutine(ActivateSkillCoroutine());
        }
    }
    protected virtual IEnumerator ActivateSkillCoroutine()
    {
        isActivating = true;
        yield return StartCoroutine(SkillCoroutine());
        isActivating = false;

        AttackEffect();
    }

    protected virtual void AttackEffect() // empty method to be overridden by child classes
    {
        Debug.Log("entering Attack effect inside skill class");
    }

    protected virtual IEnumerator SkillCoroutine()
    {
        float creatureCastSpeed = user.currentCastSpeed;

        if (animator != null) // play animation only if spell has an animation
        {
            animator.speed = creatureCastSpeed; // Set the animation speed based on the player's cast speed and the cast time of the skill
            animator.SetTrigger("Spell"); // Play the spell animation
        }
        user.canMove = false;
        yield return new WaitForSeconds(animationDuration);
        user.canMove = true;
        if (animator != null)
        {
            animator.SetTrigger("SpellFinished"); // Finish the spell animation
        }
    }

    protected virtual float CalculateAnimationDuration(float creatureCastSpeed, float castSpeed)
    {
        // This method will calculate the duration of the spell animation based on the player's cast speed and the spell's cast speed.
        // to be implemented in child classes
        return 1 / (castSpeed * creatureCastSpeed);
    }

    public virtual bool CanActivate() // this method will be used to check if the skill can be used
    {
        if (user is Player)
        {
            Player player = user.GetComponent<Player>();
            if (player != null)
            {
                if ((player.currentMana >= manaCost) && (cooldownTimer <= 0))
                {
                    return true;
                }
            return false;
            }
            else
            {
                Debug.LogError("Player is null");
                return false;
            }
        }
        else if (user is Enemy)
        {
            if ((cooldownTimer <= 0))
            {
                return true;
            }
            return false;
        }
        else
        {
            Debug.LogError("User is not a Player or an Enemy");
            return false;
        }
        
    }



    public virtual void LevelUp() // this method will be used to level up the skill
    {
        skillLevel++;
        // Increase other properties if necessary (like damage, cooldown reduction)
    }
    public virtual void UpgradeSkill() // this method will be used to upgrade the quality of the skill
    {
        quality++;
        // Increase other properties if necessary (like damage, cooldown reduction)
    }
    public virtual void OnActivate()
    // This method will handle common tasks performed upon activation, like setting the cooldown timer,
    // updating the mana pool, and any general pre-activation setup.
    {
        if (user is Player)
        {
            Player player = user.GetComponent<Player>();
            player.currentMana = Mathf.Max(player.currentMana - manaCost, 0);
        }
        if (user.currentCastSpeed == 0)
        {
            Debug.LogError("Cast speed is 0");
        }
        cooldownTimer = (1 / castSpeed) / user.currentCastSpeed;
        animationDuration = CalculateAnimationDuration(user.currentCastSpeed, castSpeed);

    }
    public virtual float[] CalculateDamage()
    {
        // its important to calculate the final damage combining both the skills attributes and the creatures attributes 
        // inside one method because they should interact multiplicatively
        // later we will add support gems that add more multipliers to the damage calculation

        float[] finalDamage = new float[5];
        // we need an array of 5 ones using 
        

        float[] baseDamage = ElementWiseMultiply(ElementWiseAdd(damage, user.creatureStats.damageFlat), ElementWiseAdd(Enumerable.Repeat(1.0f, 5).ToArray(), user.creatureStats.damageIncreases));
        finalDamage = ElementWiseMultiply(baseDamage, ElementWiseAdd(Enumerable.Repeat(1.0f, 5).ToArray(),user.creatureStats.damageMoreMultipliers));

        // this method will be used to calculate the damage of the skill
        // this method will become far more complex as we add more mechanics and add the players stats
        // note that final damage calculation is computed in the creature that receives the damage.
        return finalDamage;
    }

public virtual void UpdateCooldown(float deltaTime) //A method to manage the cooldown timer. This could be called in Update() to reduce the cooldown timer over time.
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= deltaTime;
        }
    }
                                                          

    public virtual void ResetCooldown() // a method to reset the skills cooldown instantly
    {
        cooldownTimer = 0;
    }

 
    public virtual void ApplyDamageAndEffects(List<Creature> targets)
    {
        if (targets != null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                //supply our damage and accuracy. Later we will add more parameters
                // such as elemental penetration, phys overwhelm, enemy block mitigation etc
                // this way we can make sure that certain abilities cannot be blocked or evaded/missed
                // in particular we want spells to be unevasible so we will override this method in the spell class
                target.TakeDamage(CalculateDamage(), user); 
                TriggerHitEffect(target.transform.position);
            }
        }
    }

    protected virtual void TriggerHitEffect(Vector3 position)
    {

    }

    public static float[] ElementWiseMultiply(float[] array1, float[] array2) // array multiplication 
    {
        if (array1.Length != array2.Length)
        {
            throw new ArgumentException("Arrays must be of the same length");
        }

        return array1.Zip(array2, (a, b) => a * b).ToArray();
    }

    public static float[] AddScalarToArray(float[] array, float scalar)
    {
        float[] result = new float[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i] + scalar;
        }
        return result;
    }

    public static float[] ElementWiseAdd(float[] array1, float[] array2)
    {
        if (array1.Length != array2.Length)
        {
            throw new ArgumentException("Arrays must be of the same length");
        }

        float[] result = new float[array1.Length];
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }
        return result;
    }


    public void AddTag(string tag)
    {
        tags.Add(tag);
    }

    public void RemoveTag(string tag)
    {
        tags.Remove(tag);
    }

    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    protected virtual Vector2 DetermineTargetLocation()
    {
        if (user is Player)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // make sure that we take into account the range of the skill
            // if the target position is out of range we should set it to the maximum range
            // the range depends on the skill and the user


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

    public virtual void Update()
    {
        // Update the cooldown timer
        UpdateCooldown(Time.deltaTime);
    }

}

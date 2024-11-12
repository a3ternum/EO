using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

// This becomes the parent class for every skill in the game.
public abstract class Skill : MonoBehaviour
{
    // skills have the following properties:
    public Creature user { get; set; } // the user of the skill
    public string skillName { get; protected set; } // the name of the skill
    public string skillDescription { get; protected set; } // the description of the skill
    public int skillLevel { get; protected set; } // the level of the skill, 
    public string[] skillTags { get; protected set; } // the tags of the skill (e.g. "Fire", "Ice", "Melee")
    public float damage { get; protected set; } // the damage of the skill
    public float castTime { get; protected set; } // the cast time of the skill
    public float attackSpeed { get; protected set; } // the base attack speed of the skill
    public float duration { get; protected set; } // the duration of the skill
    public float tickRate { get; protected set; } // the tick rate of the skill
    public int quality { get; protected set; } // the quality of the skill
    public bool isReady { get; protected set; } // boolean to indicate whether skill can be used right now 
    public float manaCost { get; protected set; } // the mana cost of the skill
    public float range { get; protected set; } // the range of the skill
    public int enemyLayer { get; protected set; } // the layer mask of the enemy
    public int terrainLayer { get; protected set; } // the layer mask of the terrain
    public int playerLayer { get; protected set; } // the layer mask of the player

    public Dictionary<Creature, float> lastHitTime { get; protected set; }

    [SerializeField]
    private Sprite skillIcon; // the icon of the skill to display in UI.
    
    private float cooldownTimer = 0; // the cooldown timer of the skill

    protected virtual void Awake()
    {
        lastHitTime = new Dictionary<Creature, float>();
    }
    protected virtual void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        terrainLayer = LayerMask.NameToLayer("Terrain");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    public virtual void ActivateSkill() // this method will be used to activate the skill
    {
        if (CanActivate())
        {
            OnActivate();
        }
    }
    public virtual bool CanActivate() // this method will be used to check if the skill can be used
    {
        if ((user.mana >= manaCost) && (cooldownTimer <= 0))
        {
            return true;
        }
        return false;
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
        user.mana = Mathf.Max(user.mana - manaCost, 0);
        cooldownTimer = 1/attackSpeed;
    }                                 
    public abstract float CalculateDamage();

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

    public virtual void Update()
    {
        // Update the cooldown timer
        UpdateCooldown(Time.deltaTime);
    }


}

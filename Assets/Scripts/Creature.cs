using UnityEngine;

// this is a template class for all creatures in the game

public class Creature : MonoBehaviour
{
    public float health;
    public float damage;
    public float attackSpeed;
    public float speed;
    public GameObject healthBarPrefab; 

    protected HealthBar healthBarComponent;
    private GameObject healthBarObject;
    private Canvas canvas;

    protected virtual void Start()
    {
        // instantiate the health bar prefab
        healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);

        healthBarComponent = healthBarObject.GetComponent<HealthBar>();
        // set the parent of the health bar to this creature
        healthBarComponent.setParent(this);

        healthBarComponent.setMaxHealth(health);

        canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("World Space Canvas not found. Make sure there's a canvas set to World Space.");
            return;
        }
        
        healthBarObject.transform.SetParent(canvas.transform, false); // set the health bar object as a child of the canvas
        healthBarObject.transform.position += new Vector3(0, 0.6f, 0); // set the position of the health bar above the creature
    }

    protected virtual void Update()
    {
        UpdateHealthBar();
    }
    protected void UpdateHealthBar()
    {
        if (healthBarComponent != null)
        {
            healthBarComponent.setHealth(health); // update health value
       
            healthBarObject.transform.position = transform.position + new Vector3(0, 0.6f, 0); // move healthbar to creature position

        }
    }

    protected void die()
    {
        Destroy(gameObject);
        Destroy(healthBarObject);
        Destroy(healthBarComponent);
    }

    public virtual void takeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                die();
            }
        }
    }

}

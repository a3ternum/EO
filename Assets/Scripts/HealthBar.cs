using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Creature parent;  // this is the parent creature that the health bar is attached to

    public void setParent(Creature parent)
    {
        this.parent = parent;
    }


    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        // slider.value = health; // This line is not needed since we might not want to reset the health bar to full health every time we set the max health
    
        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
       

   
}

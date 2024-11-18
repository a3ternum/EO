using UnityEngine;
using UnityEngine.UI;
public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Player player;  // this is the parent player that the experience bar is attached to

    public void setParent(Player player)
    {
        this.player = player;
    }

    public void setMaxExperience(float experience)
    {
        slider.maxValue = experience;
        // slider.value = experience; // This line is not needed since we might not want to reset the experience bar to full experience every time we set the max experience

        fill.color = gradient.Evaluate(1f);
    }

    public void setExperience(float experience)
    {
        slider.value = experience;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}

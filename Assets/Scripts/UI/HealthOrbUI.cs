using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthOrbUI : MonoBehaviour
{
    public Image healthOrbImage;        // Reference to the Image component of the orb
    public TextMeshProUGUI healthText; // Reference to the Text for health numbers
    private Player player;           // Reference to the player for positioning
    public Vector3 offset = Vector3.zero; // Offset from the player's position in the world



    private void Start()
    {
        // Find the player in the scene
        offset = new Vector3(11.6f, -3.4f, 0);
        transform.position = offset;
    }



    public void SetParent(Player player)
    {
        this.player = player;
    }

    void Update()
    {

        if (player == null)
        {
            return;
        }
        // Update the health display (maybe this can be called when health changes in the actual game instead of in Update())
        UpdateHealthOrb(player.currentHealth, player.currentMaxHealth);
    }

    public void UpdateHealthOrb(float current, float max)
    {
        // Update fill amount based on health percentage
        float healthPercent = current / max;

        healthOrbImage.fillAmount = healthPercent;
        Debug.Log("health percent is " + healthPercent);
        // Update the text to show current health and max health
        healthText.text = $"{(int)current} / {(int)max}";
    }




}

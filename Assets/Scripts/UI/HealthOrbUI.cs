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
        offset = new Vector3(11, -3.5f, 0);

        Invoke("FindPlayerWithDelay", 0.1f);
        transform.position = offset;
    }

    private void FindPlayerWithDelay()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        if (player == null)
        {
            // Find the player in the scene
            player = FindFirstObjectByType<Player>();
            if (player == null)
            {
                return;
            }
        }


    

        // Update the health display (this can be called when health changes in your actual game)
        
        UpdateHealthOrb((int)player.currentHealth, (int)player.currentMaxHealth);
    }

    public void UpdateHealthOrb(int current, int max)
    {
        // Update fill amount based on health percentage
        float healthPercent = (float)current / max;
        healthOrbImage.fillAmount = healthPercent;

        // Update the text to show current health and max health
        healthText.text = $"{current} / {max}";
    }




}

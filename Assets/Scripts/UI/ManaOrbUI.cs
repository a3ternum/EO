using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManaOrbUI : MonoBehaviour
{
    public Image manaOrbImage;        // Reference to the Image component of the orb
    public TextMeshProUGUI manaText; // Reference to the Text for mana numbers
    private Player player;           // Reference to the player for positioning
    public Vector3 offset = Vector3.zero; // Offset from the player's position in the world



    private void Start()
    {
        // Find the player in the scene
        offset = new Vector3(7.2f, -4.05f, 0);

        Invoke("FindPlayerWithDelay", 0.1f);
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


        // Update the health orb's position to follow the player
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }

        // Update the health display (this can be called when health changes in your actual game)

        UpdateHealthOrb((int)player.currentMana, (int)player.currentMaxMana);
    }

    public void UpdateHealthOrb(int current, int max)
    {
        // Update fill amount based on health percentage
        float manaPercent = (float)current / max;
        manaOrbImage.fillAmount = manaPercent;

        // Update the text to show current health and max health
        manaText.text = $"{current} / {max}";
    }




}

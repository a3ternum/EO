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
        offset = new Vector3(8.0f, -3.9f, 0);
        transform.position = offset;

        // make sure manaOrbImage is not null
        if (manaOrbImage == null)
        {
            Debug.LogError("ManaOrbUI: manaOrbImage is null");
        }
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

        // Update the mana display (maybe this can be called when mana changes in the actual game and not in Update())
        UpdateManaOrb(player.currentMana, player.currentMaxMana);
    }

    public void UpdateManaOrb(float current, float max)
    {
        // Update fill amount based on health percentage
        float manaPercent = current / max;
        manaOrbImage.fillAmount = manaPercent;
        
        // Update the text to show current health and max health
        manaText.text = $"{(int)current} / {(int)max}";
    }




}

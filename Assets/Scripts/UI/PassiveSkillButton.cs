using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSkillButton : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private PassiveSkillTree passiveSkillTree;
    private TextMeshProUGUI text;
    public int availableSkillPoints;

    private void Start()
    {
        // find the player object with a delay
        Invoke("FindPlayerAndInitializeSkillPoints", 0.3f);
    }


    private void FindPlayerAndInitializeSkillPoints()
    {
        // find the player object
        player = FindFirstObjectByType<Player>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        // initialize the button with the number of available skill points
        availableSkillPoints = player.playerStats.availableSkillPoints;
        text.text = "" + availableSkillPoints;
        if (availableSkillPoints == 0)
        {
            HideButton();
        }
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }

    public void AddSkillPoint()
    {
        availableSkillPoints++;
        UpdateText();
        ShowButton();

    }

    public void RemoveSkillPoint()
    {
        // change the number of available skill points on the button
        availableSkillPoints--;
        if (availableSkillPoints == 0)
        {
            HideButton();
        }
        UpdateText();

    }

    public void UpdateText()
    {
        text.text = "" + availableSkillPoints;
    }

    public void OnButtonPress()
    {
        if (passiveSkillTree != null)
        {
            passiveSkillTree.ToggleSkillTree();
        }
        else
        {
            Debug.LogError("PassiveSkillTree not found!");
        }
    }
}
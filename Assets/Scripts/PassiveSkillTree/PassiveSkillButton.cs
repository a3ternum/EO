using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PassiveSkillButton : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private PassiveSkillTree passiveSkillTree;
    private TextMeshProUGUI text;

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
        text.text = "" + player.playerStats.availableSkillPoints;
        if (player.playerStats.availableSkillPoints== 0)
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
        UpdateText();
        UpdateSkillPoints();

    }

    public void RemoveSkillPoint()
    {
        // change the number of available skill points on the button
        UpdateText();
        UpdateSkillPoints();
    }

    public void UpdateSkillPoints()
    {
        if (player.playerStats.availableSkillPoints > 0)
        {
            ShowButton();
            UpdateText();
        }
        if (player.playerStats.availableSkillPoints == 0)
        {
            UpdateText();
            HideButton();
        }
    }

    public void UpdateText()
    {
        text.text = "" + player.playerStats.availableSkillPoints;
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
using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSkillButton : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private PassiveSkillTree passiveSkillTree;
    private TextMeshProUGUI text;

    private void Start()
    {
        // Start a coroutine to invoke the method with a delay
        StartCoroutine(InvokeWithDelay(0.3f));
    }


    private IEnumerator InvokeWithDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Call the method after the delay
        FindPlayerAndInitializeSkillPoints();
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
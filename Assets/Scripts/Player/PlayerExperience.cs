using UnityEngine;

public class PlayerExperience : MonoBehaviour
{ 
    public ExpTable expTable;

    private Player player;
    private PlayerStats playerStats;


    private float experienceToNextLevel;


    private PassiveSkillButton passiveSkillButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        playerStats = player.playerStats;


        experienceToNextLevel = expTable.experienceRequirementsPerLevel[player.playerStats.level];


        player.experienceBarComponent.setMaxExperience(experienceToNextLevel);
        player.experienceBarComponent.setExperience(player.playerStats.experience);

        // find the passive skill button
        passiveSkillButton = FindFirstObjectByType<PassiveSkillButton>();
    }

    public void LevelUp()
    {
        player.playerStats.level++;
        player.playerStats.availableSkillPoints++;
        passiveSkillButton.AddSkillPoint();

    }

    public void UpdateLevel()
    {
        if (player.playerStats.experience >= experienceToNextLevel)
        {
            LevelUp();
            player.playerStats.experience -= experienceToNextLevel;
            experienceToNextLevel = expTable.experienceRequirementsPerLevel[player.playerStats.level -1];
            player.experienceBarComponent.setMaxExperience(experienceToNextLevel);
            player.experienceBarComponent.setExperience(player.playerStats.experience);
        }
    }
    
    public void gainExperience(float experienceGained)
    {
        player.playerStats.totalExperience += experienceGained;
        player.playerStats.experience += experienceGained;

        // update the experience bar
        player.experienceBarComponent.setExperience(player.playerStats.experience);

    }
    
    // we can make this more efficient by updating the playerStats and calling UpdateLevel() only when the player gains experience.
    public void Update()
    {
        UpdateLevel();
    }
}

using UnityEngine;

public class PlayerExperience : MonoBehaviour
{ 
    public ExpTable expTable;

    private Player player;
    private PlayerStats playerStats;

    private float totalExperience;
    private float experience;
    private float experienceToNextLevel;
    private int level;
    private int availableSkillPoints;
    private int totalSkillPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        playerStats = player.playerStats;

        totalExperience = playerStats.totalExperience;
        experience = playerStats.experience;
        level = playerStats.level;
        experienceToNextLevel = expTable.experienceRequirementsPerLevel[level];
        availableSkillPoints = playerStats.availableSkillPoints;
        totalSkillPoints = playerStats.totalSkillPoints;

        player.experienceBarComponent.setMaxExperience(experienceToNextLevel);
        player.experienceBarComponent.setExperience(experience);

    }

    public void LevelUp()
    {
        level++;
        availableSkillPoints++;
    }

    public void UpdateLevel()
    {
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
            experience -= experienceToNextLevel;
            experienceToNextLevel = expTable.experienceRequirementsPerLevel[level-1];
            player.experienceBarComponent.setMaxExperience(experienceToNextLevel);
            player.experienceBarComponent.setExperience(experience);
        }
    }
    
    public void gainExperience(float experienceGained)
    {
        totalExperience += experienceGained;
        experience += experienceGained;

        // update the experience bar
        player.experienceBarComponent.setExperience(experience);

    }
    
    // we can make this more efficient by updating the playerStats and calling UpdateLevel() only when the player gains experience.
    public void Update()
    {
        UpdateLevel();
        // update the playerStats scriptable object
        player.playerStats.level = level;
        player.playerStats.experience = experience;
        player.playerStats.totalExperience = totalExperience;
        player.playerStats.availableSkillPoints = availableSkillPoints;
        player.playerStats.totalSkillPoints = totalSkillPoints;
    }
}

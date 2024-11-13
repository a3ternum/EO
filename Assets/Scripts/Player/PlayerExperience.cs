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
        }
    }
    
    public void gainExperience(float experienceGained)
    {
        totalExperience += experienceGained;
        experience += experienceGained;
    }

    // Update is called once per frame
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

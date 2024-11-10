using UnityEngine;

public class PlayerExperience : MonoBehaviour
{ 
    public ExpTable expTable;

    private Player player;
    private PlayerData playerData;

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
        playerData = player.playerData;

        totalExperience = playerData.totalExperience;
        experience = playerData.experience;
        level = playerData.level;
        experienceToNextLevel = expTable.experienceRequirementsPerLevel[level];
        availableSkillPoints = playerData.availableSkillPoints;
        totalSkillPoints = playerData.totalSkillPoints;
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
        // update the player data scriptable object
        player.playerData.level = level;
        player.playerData.experience = experience;
        player.playerData.totalExperience = totalExperience;
        player.playerData.availableSkillPoints = availableSkillPoints;
        player.playerData.totalSkillPoints = totalSkillPoints;
    }
}

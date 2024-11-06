using UnityEngine;

public class playerExperience : MonoBehaviour
{
    public ExpTable expTable;

    public float totalExperience;
    public float experience;
    public int level;
    public float experienceToNextLevel;
    public int availableSkillPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalExperience = 0;
        experience = 0;
        level = 1;
        experienceToNextLevel = expTable.experienceRequirementsPerLevel[0];
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
    }
}

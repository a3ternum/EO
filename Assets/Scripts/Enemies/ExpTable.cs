using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ExpTable", menuName = "Experience/Exp Table")]
public class ExpTable : ScriptableObject
{
    public List<int> experienceRequirementsPerLevel;
}

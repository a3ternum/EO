using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas healthBarCanvas;


    public void AssignCanvasToCreatures()
    {
        Creature[] creatures = FindObjectsOfType<Creature>();
        foreach (Creature creature in creatures)
        {
            Debug.Log("setting canvas in manager");
            creature.SetCanvas(healthBarCanvas);
        }
    }
}
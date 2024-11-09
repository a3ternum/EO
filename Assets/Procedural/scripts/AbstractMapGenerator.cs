using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tileMapVisualizer;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateMap()
    {
        tileMapVisualizer.ClearMap();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}

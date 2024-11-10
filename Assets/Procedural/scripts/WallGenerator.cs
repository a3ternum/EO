using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    private static int fillRange = 30;
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);

        HashSet<Vector2Int> wallPositions = basicWallPositions;
        wallPositions.UnionWith(cornerWallPositions);
        PaintSurroundingVoidTiles(tilemapVisualizer, floorPositions, wallPositions);
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }

            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var wallPosition = position + direction;
                if (!floorPositions.Contains(wallPosition))
                {
                    wallPositions.Add(wallPosition);
                }
            }
        }
        return wallPositions;
    }

    private static void PaintSurroundingVoidTiles(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> floorTiles, HashSet<Vector2Int> wallTiles)
    {
        
        // Step 1: Calculate the minimum and maximum x and y values from floorTiles
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;

        foreach (var position in floorTiles)
        {
            if (position.x < minX) minX = position.x;
            if (position.x > maxX) maxX = position.x;
            if (position.y < minY) minY = position.y;
            if (position.y > maxY) maxY = position.y;
        }

        // Step 2: Expand the bounds by fillRange
        minX -= fillRange;
        maxX += fillRange;
        minY -= fillRange;
        maxY += fillRange;

        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                if (!floorTiles.Contains(position) && !wallTiles.Contains(position))
                {  
                    tilemapVisualizer.PaintSingleVoidTile(position);
                }
            }
        }
    }
}

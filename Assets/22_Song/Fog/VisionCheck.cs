using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class VisionCheck
{
    private static List<VisionRange> visionRanges
        = new List<VisionRange>();
    public static void Init()
    {
        visionRanges.Clear();
        foreach (var player in Turn.players)
        {
            visionRanges.Add(player.GetComponent<VisionRange>());
        }
        AddTile();
    }
    public static void AddTile()
    {
        foreach (var tile in AllTile.visionTile)
        {
            AllTile.prevVisionTile.Add(tile);
        }
        AllTile.visionTile.Clear();
        
        for (int i = 0; i < visionRanges.Count; i++)
        {
            visionRanges[i].ResetTile();
            var visionRange = visionRanges[i].CheackVision();
            for (int j = 0; j < visionRange.Count; j++)
            {
                if (!AllTile.visionTile.Contains(visionRange[j]))
                {   
                    visionRange[j].CheckParticle();
                    visionRange[j].obstacleVision = visionRange[j].CheakVision;
                    AllTile.visionTile.Add(visionRange[j]);
                    
                    //visionRange[j].CheakVisionSum = 0;
                }
                if(AllTile.prevVisionTile.Contains(visionRange[j]))
                {
                    AllTile.prevVisionTile.Remove(visionRange[j]);
                }
            }
        }
        foreach (var tile in AllTile.prevVisionTile)
        {
            tile.obstacleVision = tile.CheakVision;
            tile.CheckParticle();
        }
        foreach (var obstacle in AllTile.obstacleSight)
        {
            obstacle.CheckSight();
        }
        AllTile.prevVisionTile.Clear();
    }
}

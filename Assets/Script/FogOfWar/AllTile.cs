using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllTile
{
    public static List<GroundTile> SaveTile
        = new List<GroundTile>();

    public static List<GroundTile> allTile
        = new List<GroundTile>();
    public static List<GroundTile> visionTile
        = new List<GroundTile>();
    public static List<GroundTile> prevVisionTile
        = new List<GroundTile>();
    public static List<FadingObject> wallTile
        = new List<FadingObject>();

    public static List<ObstacleSight> obstacleSight = new List<ObstacleSight>();

    public static void OnDestroy()
    {
        SaveTile.Clear();
        allTile.Clear();
        visionTile.Clear();
        prevVisionTile.Clear();
        wallTile.Clear();
        obstacleSight.Clear();
    }
}

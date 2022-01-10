using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllTile
{
    public static List<GroundTile> allTile
        = new List<GroundTile>();
    public static List<GroundTile> visionTile
        = new List<GroundTile>();
    public static List<GroundTile> prevVisionTile
        = new List<GroundTile>();
    public static List<FadingObject> wallTile
        = new List<FadingObject>();
}

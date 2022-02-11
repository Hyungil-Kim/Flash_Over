using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSight : MonoBehaviour
{
    GroundTile tile;
    private void Start()
    {
        tile = GameManager.instance.tilemapManager.ReturnTile(gameObject);
        AllTile.obstacleSight.Add(this);
    }
    public void CheckSight()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            if (tile.obstacleVision)
            {
                renderer.material.color = new Color(1f, 1f, 1f);
            }
            else
            {
                renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        if(gameObject.tag == "Obstacle")
        {
            var meshrenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshrenderers)
            {
                if (tile.obstacleVision)
                {
                    mesh.enabled = true;
                }
                else
                {
                    mesh.enabled = false;
                }
                //mesh.enabled = false;
            }
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndBurn
{
    Destroy,
    NonDestroy,
    Explose
}
public enum DoorState
{
    Default,
    Open,
    Close
}
public class Obstacle : MonoBehaviour
{
    public GameManager gameManager;
    public bool updateOn = true;

    /// //////////////////////////////////
    public int obstacleMesh;
    public int exploseRange;
    public float exploseDamage;
    public EndBurn endState;
    ///////////////////////////////////// 
    public float hp;
    public bool isBurn;
    public DoorState doorState;
   

    void Start()
    {
        gameManager = GameManager.instance;
    }

   public void CheckObstacleHp()
	{
        if (hp <= 0 && updateOn)
        {
            switch (endState)
            {
                case EndBurn.Destroy:
                    gameObject.SetActive(false);
                    updateOn = false;
                    break;
                case EndBurn.NonDestroy:
                    //¸Å½¬ ¹Ù²Þ
                    updateOn = false;
                    break;
                case EndBurn.Explose:
                    var groundTile = gameManager.tilemapManager.ReturnTile(this.gameObject);
                    var list = gameManager.tilemapManager.ReturnFloodFillRange(groundTile, gameManager.setAttackColor, exploseRange);
                    gameManager.tilemapManager.ExplotionDamage(this, list);
                    updateOn = false;
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}

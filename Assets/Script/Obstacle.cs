using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndBurn
{
    Destroy,
    NonDestroy,
    Explose
}
public class Obstacle : MonoBehaviour
{
    public GameManager gameManager;
    public bool updateOn = true;

    /// //////////////////////////////////
    public int obstacleMesh; //오브젝트 가중치
    public int exploseRange; // 터지는 범위
    public float exploseDamage;//터지는 데미지
    public EndBurn endState; // 죽었을때 발생하는 상태
    ///////////////////////////////////// 
    public float hp; // 체력
    public bool isBurn; //현재 타고있는지 
    public float def; //방어력

    public bool isSight;

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
                    //매쉬 바꿈
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

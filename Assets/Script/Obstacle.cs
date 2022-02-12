using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndBurn
{
    Destroy,
    NonDestroy,
    Explose
}
public class ObstacleSaveData
{
    public ObjData data;
    public float hp;
}
public class Obstacle : MonoBehaviour
{
    public GameManager gameManager;
    public bool updateOn = true;

    public int index;

    /// //////////////////////////////////
    public int objectID;
    public float obstacleMesh; //오브젝트 가중치
    public int exploseRange; // 터지는 범위
    public float exploseDamage;//터지는 데미지
    public EndBurn endState; // 죽었을때 발생하는 상태
    ///////////////////////////////////// 
    public float maxhp;
    public float hp; // 체력
    public bool isBurn; //현재 타고있는지 
    public float def; //방어력
    public bool isSight;
    public float count;

    public ObjData data;
    void Start()
    {
        gameManager = GameManager.instance;
        Shader.SetGlobalFloat("StartBurning", 0);
        Shader.SetGlobalFloat("StopBurning", 0);

        count = 0;
        Turn.obstacles.Add(this);
        index = Turn.obstacles.Count;
        Init();
        hp = maxhp;
    }
    public void Init()
    {
        data = MyDataTableMgr.objTable.GetTable(objectID);
        exploseDamage = data.dmg;
        exploseRange = data.range;
        endState = data.endtype;
        obstacleMesh = data.weight;

        maxhp = data.hp;
        def = data.def;
    }
    public ObstacleSaveData GetData()
    {
        var osd = new ObstacleSaveData();
        osd.data = data;
        osd.hp = hp;
        return osd;
    }
    public void SetData(ObstacleSaveData obstacleSaveData)
    {
        data = obstacleSaveData.data;
        hp = obstacleSaveData.hp;
        Init();
    }
    public void Update()
	{
        Material[] material = this.GetComponent<MeshRenderer>().materials;
		if(hp < maxhp)//최대치가 아니면
		{
            material[0] = material[1];
            this.GetComponent<MeshRenderer>().materials = material;
            isBurn = true;
		}
        if(isBurn)
        {
            if (count < 6)
            {
                count += 0.16f;
            }
            material = this.GetComponent<MeshRenderer>().materials;
            material[0].SetFloat("StartBurning", count);
            if(hp <= 5)//최대치의 5퍼센트?
			{
                material[0].SetFloat("StopBurning", count);
            }
		}

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


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
    public int objectID;
    public float obstacleMesh; //������Ʈ ����ġ
    public int exploseRange; // ������ ����
    public float exploseDamage;//������ ������
    public EndBurn endState; // �׾����� �߻��ϴ� ����
    ///////////////////////////////////// 
    public float hp; // ü��
    public bool isBurn; //���� Ÿ���ִ��� 
    public float def; //����
    public bool isSight;
    public float count;
    void Start()
    {
        gameManager = GameManager.instance;
        Shader.SetGlobalFloat("StartBurning", 0);
        Shader.SetGlobalFloat("StopBurning", 0);
        count = 0;
    }
	public void Update()
	{
        Material[] material = this.GetComponent<MeshRenderer>().materials;
		if(hp < 50)//�ִ�ġ�� �ƴϸ�
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
            if(hp <= 5)//�ִ�ġ�� 5�ۼ�Ʈ?
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
                    //�Ž� �ٲ�

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


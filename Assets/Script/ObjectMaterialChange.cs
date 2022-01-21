using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaterialChange : MonoBehaviour
{

    private GameObject[] burn;
    private GameObject[] explosion;
    private float t;//������Ʈ ü�º�?
    private float end;//������ƼŬ ������?
   

    // Start is called before the first frame update
    void Start()
    {
        burn = GameObject.FindGameObjectsWithTag("ObjectBurn");
        explosion = GameObject.FindGameObjectsWithTag("ObjectExplosion");

    }

    // Update is called once per frame
    void Update()
    {
        MaterialChange();
    }

    private void MaterialChange()
    {
        
        for (int i = 0; i < burn.Length; i++)//��İ� �Ǵ°�
        {
            t = 0.01f;//������Ʈ ����ü��/�ƽ� ü�� ������ �� �� ���ƿ�?
            var originColor = burn[i].GetComponent<MeshRenderer>().material.color;
            originColor = Color.Lerp(originColor, Color.black, t);
            burn[i].GetComponent<MeshRenderer>().material.color = originColor; 
        }

        for (int i = 0; i < explosion.Length; i++)//����
        {
            end = 1f;
            Destroy(explosion[i], end);
        }

    }
}



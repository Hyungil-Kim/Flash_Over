using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaterialChange : MonoBehaviour
{

    private GameObject[] burn;
    private GameObject[] explosion;
    private float t;//오브젝트 체력별?
    private float end;//폭발파티클 끝나면?
   

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
        
        for (int i = 0; i < burn.Length; i++)//까맣게 되는거
        {
            t = 0.01f;//오브젝트 현재체력/맥스 체력 넣으면 될 거 같아요?
            var originColor = burn[i].GetComponent<MeshRenderer>().material.color;
            originColor = Color.Lerp(originColor, Color.black, t);
            burn[i].GetComponent<MeshRenderer>().material.color = originColor; 
        }

        for (int i = 0; i < explosion.Length; i++)//삭제
        {
            end = 1f;
            Destroy(explosion[i], end);
        }

    }
}



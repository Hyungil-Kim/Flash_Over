using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPool : MonoBehaviour
{
	[SerializeField] private Shape Prefab;
	[SerializeField] private int spawnAmount = 20;

	private ObjectPool<Shape> pool;
	// Start is called before the first frame update
	void Start()
	{
		pool = new ObjectPool<Shape>(() => { return Instantiate(Prefab); },
			objects => { objects.gameObject.SetActive(true); },
			objects => { objects.gameObject.SetActive(false); },
			objects => { Destroy(objects.gameObject); },
			false,
			20,
			40);


	}

	// Update is called once per frame


	public void Spawn()
	{
		for (int i = 0; i < spawnAmount; i++)
		{
			var objects = pool.Get();
			objects.transform.position = transform.position + UnityEngine.Random.insideUnitSphere * 10;
			objects.Init(DestroyShape);

		}
	}
	private void DestroyShape(Shape shape)
	{
		 pool.Release(shape);
	
	}

	void Update()
	{

	}
}


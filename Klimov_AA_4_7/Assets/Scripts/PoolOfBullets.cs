using System.Collections.Generic;
using UnityEngine;

public class PoolOfBullets : MonoBehaviour
{
	[SerializeField] private GameObject _prefabBullet;
	[SerializeField] private int _poolSize;
	private List<GameObject> _bullets;

	public List<GameObject> Bullets { get => _bullets; private set => _bullets = value; }

	public void Initialize()
	{
		Bullets = new();
		for (int i = 0; i < _poolSize; i++)
		{
			GameObject bullet = Instantiate(_prefabBullet);
			bullet.SetActive(false);
			Bullets.Add(bullet);
		}
	}

	public GameObject GetBullet()
	{
		foreach (GameObject bullet in Bullets)
		{
			if (!bullet.activeInHierarchy)
			{
				bullet.SetActive(true);
				return bullet;
			}
		}

		GameObject newBullet = Instantiate(_prefabBullet);
		newBullet.SetActive(true);
		Bullets.Add(newBullet);
		return newBullet;
	}
}

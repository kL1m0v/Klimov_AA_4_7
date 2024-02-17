using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
	[SerializeField] private Transform _muzzlePosition;
	[Space(15f), SerializeField, Range(0.1f, 2f)] private float _delayBetweenShots;
	private PlayerScript _playerScript;
	private PoolOfBullets _poolOfBullets;
	
	public void Initialize()
	{
		_playerScript = GetComponent<PlayerScript>();
		_poolOfBullets = GetComponent<PoolOfBullets>();
		_poolOfBullets.Initialize();
	}

	private void Start()
	{
		StartCoroutine(Shoot());
	}


	public IEnumerator Shoot()
	{
		while (_playerScript.TargetForShooting != null)
		{
			GameObject r = _poolOfBullets.GetBullet();
				r.transform.position = _muzzlePosition.transform.position;
				r.transform.rotation = transform.rotation;
				r.SetActive(true);
				yield return new WaitForSeconds(_delayBetweenShots);
		} 
	}
}

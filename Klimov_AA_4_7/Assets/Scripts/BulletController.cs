using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private float _speed = 3;
	private float _lifeTime = 3;
	private void OnEnable()
	{
		StartCoroutine(MoveBullet());
		StartCoroutine(Destroy());
	}
	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator MoveBullet()
	{
		while (true)
		{
			transform.position += transform.forward * _speed * Time.deltaTime;
			yield return null;
		}
	}
	private IEnumerator Destroy()
	{
		while(true)
		{
			yield return new WaitForSeconds(_lifeTime);
			gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
	[SerializeField] private Image _hpBarFilling;
	[SerializeField] private PlayerScript _playerScript;
	
	private void OnEnable()
	{
		_playerScript.HealthChanged += OnHealthChanged;
	}
	private void OnDisable()
	{
		_playerScript.HealthChanged -= OnHealthChanged;
	}
	private void Update()
	{
		transform.LookAt(Camera.main.transform);
		transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
		transform.Rotate(0, 0, 0);
	}
	
	private void OnHealthChanged(int currentHealth)
	{
		_hpBarFilling.fillAmount = (float)currentHealth / _playerScript.MaxHealthPoint;
	}
	
}

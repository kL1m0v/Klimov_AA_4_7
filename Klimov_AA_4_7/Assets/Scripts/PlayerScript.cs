using System;
using Photon.Pun;
using UnityEngine;

public class PlayerScript : MonoBehaviourPunCallbacks
{
	[SerializeField, Range(1f, 100f)] private int _healthPoints;
	private int _maxHealthPoint = 100;
	private PlayerController _playerController;
	private ShootingController _shootingController;
	private PlayerType _playerType;
	private GameObject _targetForShooting;
	private PhotonView _photonView;

	public Action<int> HealthChanged;
	public Action<PlayerType> PlayerDead;

	public int MaxHealthPoint { get => _maxHealthPoint; }
	public int HealthPoints { get => _healthPoints; private set => _healthPoints = value; }
	public GameObject TargetForShooting { get => _targetForShooting; set => _targetForShooting = value; }
	private void Awake()
	{
		_playerController = GetComponent<PlayerController>();
		_playerController.Initialize();
		_shootingController = GetComponent<ShootingController>();
		_shootingController.Initialize();
		_photonView = GetComponent<PhotonView>();

	}
	private void Start()
	{
		GameManager.AddPlayer(gameObject);
	}

	[PunRPC]
	private void TakingDamage(int damage)
	{
		HealthPoints -= damage;
		HealthChanged?.Invoke(HealthPoints);
		if (HealthPoints <= 0)
		{
			Death(_playerType);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name.Contains("DeadZone"))
		{
			if (PhotonNetwork.IsConnected && photonView.IsMine)
			{
				photonView.RPC("TakingDamage", RpcTarget.AllBuffered, _maxHealthPoint);
			}
		}
		if (other.name.Contains("Bullet"))
		{
			if (PhotonNetwork.IsConnected && photonView.IsMine)
			{
				photonView.RPC("TakingDamage", RpcTarget.AllBuffered, 10);
			}
		}


	}

	private void Death(PlayerType player)
	{
		gameObject.SetActive(false);
		GameManager.StopGame();
	}
	public void StartShooting()
	{
		StartCoroutine(_shootingController.Shoot());
	}

	public void SetTarget(GameObject target)
	{
		_targetForShooting = target;
		if (!_photonView.IsMine) return;
		StartShooting();
	}
}

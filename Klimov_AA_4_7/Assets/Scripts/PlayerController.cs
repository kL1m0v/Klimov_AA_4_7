using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	private InputSystem_Character _input;
	private CharacterController _characterController;
	[SerializeField] private PlayerType _playerType;
	[SerializeField, Range(1, 10)] private float _speed;
	private PlayerScript _playerScript;
	
	private Vector3 _directionOfMovement;
	private PhotonView _photonView;

	public InputSystem_Character Input { get => _input; private set => _input = value; }

	public void Initialize()
	{
		Input = new();
		_directionOfMovement = new();
		_characterController = GetComponent<CharacterController>();
		Input.Player.Enable();
		_photonView = GetComponent<PhotonView>();
		_playerScript = GetComponent<PlayerScript>();
	}
	private void OnDisable()
	{
		Input.Player.Disable();
	}

	private void Update()
	{
		if(_photonView.IsMine)
		{
			MovePlayer();
			RotatePlayer();
		}
	}
	private void RotatePlayer()
	{
		if (_playerScript.TargetForShooting == null)
			return;
		transform.LookAt(_playerScript.TargetForShooting.transform);
	}
	private void MovePlayer()
	{
		_directionOfMovement = Input.Player.Movement.ReadValue<Vector2>();
		Vector3 move = new Vector3(_directionOfMovement.x, 0, _directionOfMovement.y);
		_characterController.Move(move * _speed * Time.deltaTime);
	}
}


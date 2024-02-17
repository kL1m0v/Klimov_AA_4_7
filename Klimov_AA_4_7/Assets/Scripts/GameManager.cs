using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private EndGameCanvasScript _endGameCanvasScript;
	private GameObject _player1;
	private GameObject _player2;
	public static GameManager gameManager;
	
	private void Start()
	{
		gameManager = this;
	}
	private void Update()
	{
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			QuitGame();
		}
	}
	public override void OnJoinedRoom()
	{
		#if UNITY_EDITOR
		PhotonNetwork.Instantiate("Player" + PhotonNetwork.NickName, new Vector3(0, 0.5f, -4f), new Quaternion(0, 0, 0, 0));
		#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
		PhotonNetwork.Instantiate("Player" + PhotonNetwork.NickName, new Vector3(0, 0.5f, 4f), new Quaternion(0, 0, 0, 0));
		#endif
	}
	

	public static void StopGame()
	{
		if(gameManager._player1.activeSelf == false) 
			gameManager._endGameCanvasScript.SetText("Победил красный игрок");
		else 
			gameManager._endGameCanvasScript.SetText("Победил синий игрок");
		gameManager.TurnOffControl();
		gameManager.Exit();
	}

	private void QuitGame()
	{
		PhotonNetwork.LeaveRoom();
	}
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}
	public static void AddPlayer(GameObject player)
	{
		if(player.name.Contains("1")) gameManager._player1 = player;
		else gameManager._player2 = player;
		
		if(gameManager._player1 != null && gameManager._player2 != null)
		{
			gameManager._player1.GetComponent<PlayerScript>().SetTarget(gameManager._player2);
			gameManager._player2.GetComponent<PlayerScript>().SetTarget(gameManager._player1);
		}
	}
	
	private void TurnOffControl()
	{
		_player1.GetComponent<PlayerController>().Input.Player.Disable();
		_player2.GetComponent<PlayerController>().Input.Player.Disable();
	}
	
	private void Exit()
	{
		StartCoroutine(WaitExit());
	}
	private IEnumerator WaitExit()
	{
		while(true)
		{
			yield return new WaitForSeconds(2f);
			QuitGame();
		}
	}
	
}

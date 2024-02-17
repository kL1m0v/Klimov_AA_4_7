using UnityEngine;
using Photon.Pun;

public class NetworkHelper : MonoBehaviourPunCallbacks
{
	private MenuScript _menuScript;
	private void Start()
	{
		_menuScript = GetComponent<MenuScript>();
		#if UNITY_EDITOR
		PhotonNetwork.NickName = "1";
		#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
		PhotonNetwork.NickName = "2";
		#endif
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.GameVersion = "0.1";
		PhotonNetwork.ConnectUsingSettings();
	}
	
	public void OnCreate()
	{
		PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2});
	}
	public void OnJoin()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	public override void OnConnectedToMaster()
	{
		_menuScript.EnableButtons();
	}

	public override void OnCreatedRoom()
	{
		Debug.Log("Комната создана");
	}
	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log($"Ошибка создания комнаты {returnCode}, {message}");
	}
	
	
}

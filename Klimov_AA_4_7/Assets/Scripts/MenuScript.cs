using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
	[SerializeField] private Button _createButton;
	[SerializeField] private Button _joinButton;
	[SerializeField] private Button _exitButton;
	private NetworkHelper _networkhelper;

	private void Start()
	{
		_networkhelper = GetComponent<NetworkHelper>();
		_createButton.onClick.AddListener(OnCreateButtonClick);
		_joinButton.onClick.AddListener(OnJoinButtonClick);
		_exitButton.onClick.AddListener(OnExitButtonClick);
	}
	
	private void OnCreateButtonClick()
	{
		_networkhelper.OnCreate();
		SceneManager.LoadScene(1);
	}
	private void OnJoinButtonClick()
	{
		_networkhelper.OnJoin();
		SceneManager.LoadScene(1);
	}
	private void OnExitButtonClick()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
		Application.Quit();
		#endif
	}
	public void EnableButtons()
	{
		_createButton.gameObject.SetActive(true);
		_joinButton.gameObject.SetActive(true);
		_exitButton.gameObject.SetActive(true);
	}
}

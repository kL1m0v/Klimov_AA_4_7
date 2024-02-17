using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EndGameCanvasScript : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _endGameText;
	public void SetText(string text)
	{
		_endGameText.text = text;
	}
}

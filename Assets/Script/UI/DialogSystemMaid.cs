using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemMaid : MonoBehaviour
{
	// 掛上 TextArea 才能夠輸入多行
	[TextArea(2, 4)]
	public string[] TempDialogs;
	public float CharCountPerSecond = 4f;
	// 提示按按鈕進入下一句對話用的圖示
	public GameObject NextButton;
	public Text TextContainer;

	// 目前是哪一句對話（陣列 index）
	private int currentDialogId = 0;
	// 目前顯示進度（多少個字）
	private int currentProgress = 0;
	private float timer;
	private float timePerChar;
	private string currentText;

	// Use this for initialization
	void Start () {
		currentDialogId = 0;
		currentProgress = 0;
		timer = 0f;
		NextButton.SetActive(false);
		timePerChar = 1f / CharCountPerSecond;
		currentText = (TempDialogs.Length > 0 ? TempDialogs[currentDialogId] : "");
	}
	
	// Update is called once per frame
	void Update () {
		// 如果未顯示完全部文字
		if (currentProgress < currentText.Length)
		{
			// 如果按了空白鍵，就一口氣顯示到底
			if (Input.GetKeyDown(KeyCode.Space))
			{
				currentProgress = currentText.Length;
			}
			// 如果沒按，就計時
			else
			{
				timer += Time.deltaTime;
				// 按照經過時間，決定是否顯示下一個字
				while (timer >= timePerChar && currentProgress < currentText.Length)
				{
					timer -= timePerChar;
					currentProgress++;
				}
			}
			// 更新顯示的文字，用 Substring 取得前 currentProgress 個字
			TextContainer.text = currentText.Substring(0, currentProgress);
			// 如果顯示完了，就把提示這句對話已結束，等待玩家按按鈕繼續的圖示顯示出來
			if (currentProgress == currentText.Length)
			{
				NextButton.SetActive(true);
			}
		}
		// 如果已顯示完所有文字，等待玩家按下空白鍵
		else
		{
			// 如果玩家按了空白鍵
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (TempDialogs.Length > 0)
				{
					// 抓下一句，用 % 讓它循環；其它東西清空，把提示按按鍵進下一句的圖示藏起來
					currentDialogId = (currentDialogId + 1) % TempDialogs.Length;
					currentProgress = 0;
					currentText = TempDialogs[currentDialogId];
					NextButton.SetActive(false);
					timer = 0f;
				}
			}
		}
	}
}

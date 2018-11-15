using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemMaid : MonoBehaviour
{
	// 方便公開給他人召喚
	public static DialogSystemMaid Summon;

	// 掛上 TextArea 才能夠輸入多行
	[TextArea(2, 4)]
	public string[] TempDialogs;
	public float CharCountPerSecond = 4f;
	// 提示按按鈕進入下一句對話用的圖示
	public GameObject NextButton;
	public Text TextContainer;
	public GameObject DialogContainer;

	// 目前是哪一句對話（陣列 index）
	private int currentDialogId = 0;
	// 目前顯示進度（多少個字）
	private int currentProgress = 0;
	private float timer;
	private float timePerChar;
	private string currentText;

	private bool isStarted = false;
	private bool isFinished = false;

	// Use this for initialization
	void Start () {
		// 覺醒時馬上登錄自己至召喚用名單
		Summon = this;
		// 開始先隱藏自身，等待啟動的時機到來
		DialogContainer.SetActive(false);
		Init();
	}

	public bool IsFinished()
	{
		return isFinished;
	}

	public void DialogStart()
	{
		isStarted = true;
		DialogContainer.SetActive(true);
		Init();
	}

	public void SetDialogList(string[] dialog)
	{
		TempDialogs = dialog;
		Init();
	}

	private void Init()
	{
		currentDialogId = 0;
		currentProgress = 0;
		timer = 0f;
		NextButton.SetActive(false);
		timePerChar = 1f / CharCountPerSecond;
		currentText = (TempDialogs.Length > 0 ? TempDialogs[currentDialogId] : "");
		isFinished = false;
		TextContainer.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		// 啟動時才進行運算和處理
		if (isStarted)
		{
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
						currentDialogId++;
						// 如果還有對話，就繼續
						if (currentDialogId < TempDialogs.Length)
						{
							currentProgress = 0;
							currentText = TempDialogs[currentDialogId];
							NextButton.SetActive(false);
							timer = 0f;
						}
						// 否則就結束這次的對話
						else
						{
							isFinished = true;
							isStarted = false;
							DialogContainer.SetActive(false);
						}
					}
				}
			}
		}
	}
}

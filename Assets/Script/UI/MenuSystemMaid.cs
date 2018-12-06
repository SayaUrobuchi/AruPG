using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuFunc
{
	None = 0, 
	Main = 1, 
	Item = 10, 
	Skill = 20, 
	Equip = 30, 
	Close = 1024, 
}

public class MenuSystemMaid : MonoBehaviour
{
	// struct 必須加入 [System.Serializable] 才能在 Inspector 編輯
	[System.Serializable]
	public struct MainMenuItemData
	{
		public string DisplayName;
		public MainMenuFunc Func;
	}

	public static MenuSystemMaid Summon;

	[Header("References")]
	public GameObject View;
	public Transform MainMenuItemContainer;
	public MainMenuItemMaid MainMenuItemTemplate;
	public MainMenuItemData[] MainMenuItems;
	public RectTransform MainMenuCursor;

	[Header("Adjust Settings")]
	public float MainMenuCursorXOffset = 2f;

	private bool active = false;
	private MainMenuItemMaid[] MainMenuMaidList;
	private int currentSelected = 0;
	private MainMenuFunc currentPage;

	// Use this for initialization
	void Start () {
		Summon = this;
		// 生成主選單項目
		MainMenuMaidList = new MainMenuItemMaid[MainMenuItems.Length];
		for (int i = 0; i < MainMenuItems.Length; i++)
		{
			MainMenuItemMaid maid = Instantiate(MainMenuItemTemplate, MainMenuItemContainer, false);
			maid.SetData(MainMenuItems[i]);
			MainMenuMaidList[i] = maid;
		}
		// 設定預設選取項目
		SelectItem(0);
		MenuClose();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool menuKeyPressed = Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape);
		bool up = Input.GetKeyDown(KeyCode.UpArrow);
		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		bool decide = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z);
		// 選單存在時
		if (active)
		{
			// 位於主選單時
			if (currentPage == MainMenuFunc.Main)
			{
				// 選單鍵 => 關閉選單（或者可能是退回上一層頁面）
				if (menuKeyPressed)
				{
					MenuClose();
				}
				// 上下鍵 => 移動項目
				else if (up)
				{
					SelectItem((currentSelected + MainMenuItems.Length - 1) % MainMenuItems.Length);
				}
				else if (down)
				{
					SelectItem((currentSelected + 1) % MainMenuItems.Length);
				}
				else if (decide)
				{
					Debug.Log("press on index ["+currentSelected+"] with ("+MainMenuItems[currentSelected].DisplayName+", "+MainMenuItems[currentSelected].Func+")");
					// 道具欄已可呼叫
					MainMenuFunc target = MainMenuItems[currentSelected].Func;
					if (target == MainMenuFunc.Item)
					{
						currentPage = target;
						ItemSystemMaid.Summon.ItemMenuOpen();
					}
				}
			}
		}
		// 選單不存在時
		else
		{
			// 選單鍵 => 呼叫選單
			if (menuKeyPressed)
			{
				if (!WorldMaid.Summon.IsEvent)
				{
					MenuOpen();
				}
			}
		}
	}

	public void MenuOpen()
	{
		currentPage = MainMenuFunc.Main;
		WorldMaid.Summon.IsMenu = true;
		View.SetActive(true);
		active = true;
		// 設定預設的選取與否
		// 在這裡改 currentSelected 可以重置遊標，不洗就維持上次選擇
		for (int i = 0; i < MainMenuItems.Length; i++)
		{
			MainMenuMaidList[i].SetSelected(i == currentSelected);
		}
		SelectItem(currentSelected);
	}

	public void MenuClose()
	{
		WorldMaid.Summon.IsMenu = false;
		View.SetActive(false);
		active = false;
	}

	public void SelectItem(int idx)
	{
		// 把目前的取消選取，再把新的設為已選取
		MainMenuMaidList[currentSelected].SetSelected(false);
		currentSelected = idx;
		MainMenuMaidList[currentSelected].SetSelected(true);
		// 移動遊標位置用
		MoveCursor(MainMenuMaidList[currentSelected].transform);
	}

	public void MoveCursor(Transform t)
	{
		// 抓 RectTransform 上的 rect 能較好地掌握矩形的位置和範圍
		RectTransform rt = t.GetComponent<RectTransform>();
		// 抓位置加上 Offset 的位移
		Vector2 pos = rt.anchoredPosition + Vector2.left * MainMenuCursorXOffset;
		MainMenuCursor.anchoredPosition = pos;
	}

	public MainMenuFunc GetCurrentPage()
	{
		return currentPage;
	}

	public void BackToMain()
	{
		currentPage = MainMenuFunc.Main;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystemMaid : MonoBehaviour {

	public static ItemSystemMaid Summon;

	public GameObject View;
	public RectTransform ItemContainer;
	public RectTransform ItemCursor;
	public Text ItemDescriptionContainer;

	public ItemListItemMaid Template;
	public float CursorXOffset = 10f;

	private ItemBagMaid.ItemIDToOwned[] itemOwned;
	private ItemListItemMaid[] maidList;
	private bool isOpen;
	private int currentSelected = 0;

	// Use this for initialization
	void Start () {
		Summon = this;
		View.SetActive(false);
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		// 如果道具欄開啟中，才處理道具欄的輸入
		if (isOpen)
		{
			bool up = Input.GetKeyDown(KeyCode.UpArrow);
			bool down = Input.GetKeyDown(KeyCode.DownArrow);
			bool left = Input.GetKeyDown(KeyCode.LeftArrow);
			bool right = Input.GetKeyDown(KeyCode.RightArrow);
			bool decide = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z);
			bool cancel = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X);
			// 方向鍵改變選取目標
			if (up)
			{
				SetSelected((currentSelected + maidList.Length - 2) % maidList.Length);
			}
			if (down)
			{
				SetSelected((currentSelected + 2) % maidList.Length);
			}
			if (left)
			{
				SetSelected((currentSelected + maidList.Length - 1) % maidList.Length);
			}
			if (right)
			{
				SetSelected((currentSelected + 1) % maidList.Length);
			}
			// 按下確定時，先顯示訊息就好
			if (decide)
			{
				ItemListItemMaid selected = maidList[currentSelected];
				ItemBagMaid.ItemIDToOwned owned = selected.Data;
				Debug.Log("Item decide on index ["+currentSelected.ToString()+"], with "+owned.Owned+" ["+owned.ID.ToString()+"]");
			}
			// 取消回主畫面
			if (cancel)
			{
				ItemMenuClose();
			}
		}
	}

	public void ItemMenuOpen()
	{
		View.SetActive(true);
		// 取得道具欄
		itemOwned = ItemBagMaid.Summon.GetAllItems();
		// 對每項道具生成顯示用 GameObject
		maidList = new ItemListItemMaid[itemOwned.Length];
		for (int i = 0; i < itemOwned.Length; i++)
		{
			ItemListItemMaid maid = Instantiate(Template, ItemContainer);
			maid.SetData(itemOwned[i]);
			maidList[i] = maid;
		}
		// 設定預設選取的道具
		SetSelected(0);
		isOpen = true;
	}

	public void ItemMenuClose()
	{
		// 刪掉所有生成的 maid，下次會全部重新生成
		for (int i = 0; i < maidList.Length; i++)
		{
			// 注意必須 Destroy 掉 GameObject
			// 否則只會把 Component 從 GameObject 上拿掉，GameObject 不會消失
			Destroy(maidList[i].gameObject);
		}
		View.SetActive(false);
		isOpen = false;
		// 通知主選單回上一頁，交回控制權
		MenuSystemMaid.Summon.BackToMain();
	}

	public void SetSelected(int next)
	{
		// 改變當前所選，並移動遊標
		currentSelected = next;
		MoveCursor(maidList[currentSelected].transform);
		// 記得變更道具說明
		ItemDescriptionContainer.text = maidList[currentSelected].GetItemDescription();
	}

	public void MoveCursor(Transform target)
	{
		// 移動遊標到目標位置，加上位移
		RectTransform rt = target.GetComponent<RectTransform>();
		ItemCursor.anchoredPosition = rt.anchoredPosition + Vector2.left * CursorXOffset;
	}
}

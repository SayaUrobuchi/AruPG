using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListItemMaid : MonoBehaviour {

	public ItemBagMaid.ItemIDToOwned Data;

	public Image IconContainer;
	public Text ItemNameContainer;
	public Text ItemOwnedNumberContainer;

	private Item itemCache;

	// Use this for initialization
	void Start () {
		OnDataChanged();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 改變顯示道具資料用
	public void SetData(ItemBagMaid.ItemIDToOwned t)
	{
		Data = t;
		OnDataChanged();
	}

	// 資料變動時更新畫面顯示
	[ContextMenu("更新畫面")]
	public void OnDataChanged()
	{
		itemCache = ItemMaid.Summon.GetPrefabByID(Data.ID);
		IconContainer.sprite = itemCache.DisplayIcon;
		ItemNameContainer.text = itemCache.DisplayName;
		ItemOwnedNumberContainer.text = Data.Owned.ToString();
	}

	// 獲取 Description
	public string GetItemDescription()
	{
		return itemCache.Description;
	}
}

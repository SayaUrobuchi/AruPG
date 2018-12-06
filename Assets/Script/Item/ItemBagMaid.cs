using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagMaid : MonoBehaviour {

	// 由於這在 Script 中需要修改，所以使用 class
	// 只在 inspector 中需要修改的話，就用 struct
	[System.Serializable]
	public class ItemIDToOwned
	{
		public ItemID ID;
		public int Owned;
	}

	public static ItemBagMaid Summon;

	public ItemIDToOwned[] Items;

	private Dictionary<ItemID, ItemIDToOwned> table = new Dictionary<ItemID, ItemIDToOwned>();

	// Use this for initialization
	void Start () {
		Summon = this;
		for (int i = 0; i < Items.Length; i++)
		{
			table[Items[i].ID] = Items[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 詢問是否持有某道具
	public bool IsOwned(ItemID id)
	{
		return table.ContainsKey(id);
	}

	// 詢問某道具的持有數量
	public int GetOwnedByID(ItemID id)
	{
		if (!IsOwned(id))
		{
			return 0;
		}
		return table[id].Owned;
	}

	// 獲得某道具 1 個
	public void GetOneItem(ItemID id)
	{
		GetItem(id, 1);
	}

	// 獲得某道具 n 個
	public void GetItem(ItemID id, int num)
	{
		// 還未持有就扔進 table
		if (!IsOwned(id))
		{
			ItemIDToOwned item = new ItemIDToOwned();
			item.ID = id;
			item.Owned = num;
			table[id] = item;
		}
		// 已持有就直接增加數量
		else
		{
			table[id].Owned += num;
		}
	}

	public ItemIDToOwned[] GetAllItems()
	{
		ItemIDToOwned[] list = new ItemIDToOwned[table.Count];
		int idx = 0;
		foreach (ItemID id in table.Keys)
		{
			list[idx] = table[id];
			idx++;
		}
		return list;
	}
}

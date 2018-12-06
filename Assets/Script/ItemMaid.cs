using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum 可以使用中文名稱，這在下拉式選單會較好讀
public enum ItemID
{
	// 製造一些不使用的 ID 來作為下拉式選單的分類使用，可以讓選取時更輕鬆；命名能加一些巧思幫助快速辨認
	// enum 裡的名字只用在 Inspector 和 Script 中，玩家是看不到的，所以名字上可以偷偷做筆記
	____恢復類____ = 100, 
	藥草 = 101, 
	藥水 = 102, 
	// 不同分類 ID 保留間隔，避免中間要插入新道具時的大量修改
	// ID 不應被改動
	____裝備類____ = 400, 
	木劍 = 401, 
	木杖 = 402, 
}

public class ItemMaid : MonoBehaviour {

	// ID => Prefab 配對
	[System.Serializable]
	public struct ItemIDToPrefabPair
	{
		public ItemID ID;
		public Item Prefab;
	}

	public static ItemMaid Summon;

	// 儲存查詢失敗時的墊擋物
	public Item ItemNotExists;
	// 用來儲存複數存在的 ID => Prefab 配對
	public ItemIDToPrefabPair[] Pairs;

	// Dictionary 方便讓我們丟 ID 快速找到 Prefab
	private Dictionary<ItemID, ItemIDToPrefabPair> table = new Dictionary<ItemID, ItemIDToPrefabPair>();

	// Use this for initialization
	void Start () {
		Summon = this;
		// 將陣列轉存為 Dictionary 方便查詢
		for (int i = 0; i < Pairs.Length; i++)
		{
			table[Pairs[i].ID] = Pairs[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 傳入 ID 獲取 Prefab
	public Item GetPrefabByID(ItemID id)
	{
		if (table.ContainsKey(id))
		{
			return table[id].Prefab;
		}
		return ItemNotExists;
	}
}

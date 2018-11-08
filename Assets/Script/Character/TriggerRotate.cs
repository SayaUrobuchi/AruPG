using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotate : MonoBehaviour {

	public Character Chara;

	private MoveDirection lastDir = MoveDirection.None;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Chara != null)
		{
			// 如果方向有變
			if (Chara.Dir != lastDir)
			{
				lastDir = Chara.Dir;
				// 依照 MoveDirection 的整數代號，計算出相對應角度；公式可按個人設定代號不同進行修改
				// 旋轉方向、0 度時的方向則視個人 Collider 設定不同，實際旋轉看看找出對應表就 OK
				// Dir 0 (Left) => 90, Dir 1 (Up) => 0, Dir 2 (Right) => -90, Dir 3 (Down) => -180
				int angle = 90 - (int)lastDir * 90;
				// 計算 z 軸旋轉 angle 度後的結果，並對 transform 進行修改
				transform.rotation = Quaternion.Euler(0f, 0f, angle);
			}
		}
	}
}

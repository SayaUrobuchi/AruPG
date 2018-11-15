using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
	Left = 0, 
	Up = 1, 
	Right = 2, 
	Down = 3, 
	None = -1, 
}

public class PlayerControllMover : MonoBehaviour {

	public Character Chara;
	public MoveDirection Dir;
	public float MoveSpeed = 2f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		if (Chara == null)
		{
			Chara = GetComponent<Character>();
		}
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		if (WorldMaid.Summon.IsEvent)
		{
			x = 0f;
			y = 0f;
		}
		// 以傾向 x 或者 y 來判斷斜向移動時該面向垂直方向，還是水平方向
		if (Mathf.Abs(y) > Mathf.Abs(x))
		{
			if (y > 0f)
			{
				Dir = MoveDirection.Up;
			}
			else
			{
				Dir = MoveDirection.Down;
			}
		}
		else if (Mathf.Abs(x) > Mathf.Abs(y))
		{
			if (x < 0f)
			{
				Dir = MoveDirection.Left;
			}
			else
			{
				Dir = MoveDirection.Right;
			}
		}
		// 變更存放方向的變數、調整 Animator 的參數以對應動畫的變更
		bool moving = (x != 0f || y != 0f);
		Chara.Dir = Dir;
		Chara.Ani.SetInteger("Direction", (int)Dir);
		Chara.Ani.SetBool("Moving", moving);
		// 實際速度以 normalized 將向量一般化為長度 1 後再乘速度，以保證長度永遠相同（等速）
		rb.velocity = new Vector2(x, y).normalized * MoveSpeed;
	}
}

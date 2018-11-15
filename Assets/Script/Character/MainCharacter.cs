using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
	public PlayerTriggerTest TriggerTest;
	public SpriteRenderer QuestionIcon;
	public float QuestionIconDisplayTime = 2f;

	private float timer;

	// Use this for initialization
	void Start () {
		QuestionIcon.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0f)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				QuestionIcon.enabled = false;
			}
		}
		if (!WorldMaid.Summon.IsEvent)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (TriggerTest.IsTargetNPCExists())
				{
					NPC target = TriggerTest.GetTargetNPC();
					target.OnEventStart();
				}
				else
				{
					Debug.Log("主角面前不存在任何 NPC！！");
					timer = QuestionIconDisplayTime;
					QuestionIcon.enabled = true;
				}
			}
		}
	}
}

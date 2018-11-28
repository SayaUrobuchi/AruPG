using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuItemMaid : MonoBehaviour {

	public MenuSystemMaid.MainMenuItemData Data;

	public Text TextContainer;
	public Animator AnimatorRef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetData(MenuSystemMaid.MainMenuItemData data)
	{
		Data = data;
		TextContainer.text = data.DisplayName;
		SetSelected(false);
	}

	public void SetSelected(bool value)
	{
		AnimatorRef.SetBool("Selected", value);
	}
}

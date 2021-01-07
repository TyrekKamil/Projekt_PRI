using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	//Set only in minigame
	[SerializeField] UI ui;
	// Update is called once per frame
	bool canPress = true;
    public void Select()
    {
		if (LoadGameNotPossible())
		{
			return;
		}
		animator.SetBool ("selected", true);
		
    }
	public void Unselect()
	{
		if (LoadGameNotPossible())
		{
			return;
		}
		animator.SetBool("selected", false);
	}
	public void SpeedUp() {
		if (canPress)
		{
			ui.targetTime = -999.0f;
			canPress = false;
		}
	}
	public void Click(){
		if (LoadGameNotPossible())
		{
			return;
		}
		animator.SetBool ("pressed", true);
	}
	public void QuitGame(){
		Debug.Log("quitting...");
		Application.Quit();
	}
	public void PlaySound(AudioClip whichSound){
		if (LoadGameNotPossible())
		{
			return;
		}

		menuButtonController.audioSource.PlayOneShot(whichSound);
	}

	private bool LoadGameNotPossible()
    {
		return gameObject.name == "Load game" && !SaveLoad.SaveExists("Scene");

	}
}

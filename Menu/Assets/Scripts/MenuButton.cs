using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

    // Update is called once per frame
    public void Select()
    {
		animator.SetBool ("selected", true);
		
    }
	public void Unselect()
	{
		animator.SetBool("selected", false);
	}
	public void Click(){
		animator.SetBool ("pressed", true);
	}
	public void QuitGame(){
		Debug.Log("quitting...");
		Application.Quit();
	}
	public void PlaySound(AudioClip whichSound){
		menuButtonController.audioSource.PlayOneShot(whichSound);
	}
}

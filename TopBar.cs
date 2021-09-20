using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour {

	public Image pause;
	public Image speed1x;
	public Image speed2x;

	public void Pause(){
		FindObjectOfType<AudioManager>().Play("Click");
		pause.color = Color.green;
		speed1x.color = Color.white;
		speed2x.color = Color.white;
	}
	public void Speed1x(){
		FindObjectOfType<AudioManager>().Play("Click");
		pause.color = Color.white;
		speed1x.color = Color.green;
		speed2x.color = Color.white;
	}
	public void Speed2x(){
		FindObjectOfType<AudioManager>().Play("Click");
		pause.color = Color.white;
		speed1x.color = Color.white;
		speed2x.color = Color.green;
	}
}

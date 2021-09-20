using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneScript : MonoBehaviour {

	public GameObject s1;
	public GameObject s2;

	bool done;

	void Start(){
		s1.SetActive(true);
		s2.SetActive(false);
	}

	public void Clicked(){
		if(!done){
		s1.SetActive(false);
		s2.SetActive(true);
		done = true;
		}else{
			SceneManager.LoadScene("MainGame");
		}
	}
}

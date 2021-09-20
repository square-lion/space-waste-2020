using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public string nextScene;

	public GameObject tutorial;

	public GameObject settings;

	public void Go(){
		SceneManager.LoadScene(nextScene);
	}
	public void Clicked(){
		tutorial.SetActive(true);
	}

	public void Yes(){
		PlayerPrefs.SetInt("Tutorial", 1);
		Go();
	}

	public void No(){
		PlayerPrefs.SetInt("Tutorial", 0);
		Go();
	}

	public void Settings(){
		settings.SetActive(!settings.activeSelf);
	}
}

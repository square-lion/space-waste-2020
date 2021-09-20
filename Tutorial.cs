using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public int stage;

	public string[] stages;

	public Text stageText;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Tutorial") != 1)
			Destroy(gameObject);
		else
			stageText.text = stages[stage];
	}

	public void NextStage(){
		stage++;

		if(stage >= stages.Length){
			GetComponent<Image>().enabled = false;
			stageText.gameObject.SetActive(false);
		}
		stageText.text = stages[stage];
	}

	public void Clicked(){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 5){
			NextStage();
		}
	}
}

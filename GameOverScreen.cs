using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	public Text reason;
	public Text year;
	public Text trashRemoved;
	public Text totalPopulation;
	public Text trashPlanets;
	public Text planetsColonized;

	public PlanetController planet;
	public string reasonForEnd;

	void Start(){
		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}
		GetComponent<Image>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;
	}

	public void TheGameIsOver(PlanetController p, string r){

		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(true);
		}
		GetComponent<Image>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;

		planet = p;
		reason.text = planet.planetName + " revolted because of " + r;
		year.text = "You survived until " + FindObjectOfType<Manager>().year;
		trashRemoved.text = "Total Trash Removed: " + PlayerPrefs.GetInt("TotalTrash");
		totalPopulation.text = "Total Population: " + PlayerPrefs.GetInt("TotalPop");
		trashPlanets.text = "Trash Planets Formed: " + PlayerPrefs.GetInt("TrashP");
		planetsColonized.text = "Planets Colonized: " + PlayerPrefs.GetInt("ColonyP");
		FindObjectOfType<Manager>().PauseTime();
	}	

	public void Restart(){
		FindObjectOfType<WatchAd>().ShowAd();
		SceneManager.LoadScene("MainGame");
	}

	public void Menu(){
		FindObjectOfType<WatchAd>().ShowAd();
		SceneManager.LoadScene("Menu");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public List<PlanetController> planets;

	Ray ray;
	RaycastHit hit;
	public LayerMask touchInputMask;
	public Camera main;

	public PlanetInfoScreen planetScreen;

	PlanetController curPlanet;

	public static bool changingTrash = false;
	public static bool colonizing = false;

	public Text YearText;
	public int year;

	public bool spaced;

	void Start(){
		PauseTime();
		year = 2020;
		InvokeRepeating("Day", 3,3);
		PlayerPrefs.SetInt("TotalTrash", 0);
		PlayerPrefs.SetInt("TotalPop", 0);
		PlayerPrefs.SetInt("TrashP", 0);
		PlayerPrefs.SetInt("ColonyP", 0);
	}

	void Day(){
		year += 10;
		YearText.text = "Year: " + year;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
				PauseTime();
		}
	}


	public void ChangeTrash(PlanetController p){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 1){
			FindObjectOfType<Tutorial>().NextStage();
		}
		curPlanet.curTrashPlanet = p.GetComponent<PlanetController>();
		curPlanet.noTrashPlanetWarning.SetActive(false);
		changingTrash = false;
		planetScreen.Open();
	}

	public void Colonizing(PlanetController p){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 3){
			FindObjectOfType<Tutorial>().NextStage();
		}
		curPlanet.SendColonyShip(p.GetComponent<PlanetController>());
		colonizing = false;
		planetScreen.Open();
	}

	public void ClickedPlanet(PlanetController p){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 0){
			FindObjectOfType<Tutorial>().NextStage();
		}
		if(curPlanet != null)
			curPlanet.selected = false;
		p.selected = true;
		curPlanet = p;
		planetScreen.planet = curPlanet;
		planetScreen.Open();
	}
	public void PauseTime(){
		spaced = true;
		Time.timeScale = 0f;
		FindObjectOfType<TopBar>().Pause();
	}
	public void Time1x(){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 4){
			FindObjectOfType<Tutorial>().NextStage();
		}
		spaced = false;
		Time.timeScale = 1f;
		FindObjectOfType<TopBar>().Speed1x();
	}	
	public void Time2x(){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 4){
			FindObjectOfType<Tutorial>().NextStage();
		}
		spaced = false;
		Time.timeScale = 2f;
		FindObjectOfType<TopBar>().Speed2x();
	}
}

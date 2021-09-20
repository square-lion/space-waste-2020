using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoScreen : MonoBehaviour {

	public bool open;
	//Shared Variables
	public Text planetName;
	public Text trashAmount;

	//Colony Variable
	public Text shipAmount;
	public Text population;
	public Button setTrashPlanet;
	public Button colonizeButton;

	public GameObject trashPlanetW;
	public GameObject overpopulationW;
	public GameObject garbageW;
	

	public GameObject infoBox;
	public Text bottomBatText;

	public PlanetController planet;


	public GameObject trashPlanet;
	public GameObject trashPlanetIcon;

	public Sprite questionMark;

	void Start(){
		Close();
	}

	void Update(){
		if(!open)
			return;

		if(planet.colony){
			planetName.text  = planet.planetName + "";
			trashAmount.text = "Trash: " + planet.curTrash + "/" + planet.trashCapacity;
			shipAmount.text =  "Ships: " + planet.curShipAmount + "/" + planet.shipAmount;
			population.text =  "Population: " + planet.curPopulation + "/" + planet.population;
			if(planet.curTrashPlanet != null){
				trashPlanetIcon.GetComponent<Image>().sprite = planet.curTrashPlanet.GetComponent<SpriteRenderer>().sprite;
			}else{
				trashPlanetIcon.GetComponent<Image>().sprite = questionMark;
			}
		}else{
			planetName.text  = planet.planetName + "";
			trashAmount.text = "Trash: " + planet.curTrash + "/" + planet.trashCapacity;
			shipAmount.text =  "";
			population.text =  "";
			setTrashPlanet.gameObject.SetActive(false);
			colonizeButton.gameObject.SetActive(false);
		}
	}

	public void SetTrashPlanet(){
		FindObjectOfType<AudioManager>().Play("Click");
		Manager.changingTrash = true;
		bottomBatText.text = "Select a planet to send trash to";
		colonizeButton.gameObject.SetActive(false);
		overpopulationW.SetActive(false);
		trashPlanetW.SetActive(false);
		garbageW.SetActive(false);
	}

	public void ColonizePlanet(){
		if(PlayerPrefs.GetInt("Tutorial") == 1 && FindObjectOfType<Tutorial>().stage == 2){
			FindObjectOfType<Tutorial>().NextStage();
		}
		FindObjectOfType<AudioManager>().Play("Click");
		Manager.colonizing = true;
		bottomBatText.text = "Select a planet to colonize";
		colonizeButton.gameObject.SetActive(false);
		overpopulationW.SetActive(false);
		trashPlanetW.SetActive(false);
		garbageW.SetActive(false);
	}

	public void Open(){
		FindObjectOfType<AudioManager>().Play("Click");
		GetComponent<Image>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;
		open = true;
		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(true);
		}
		bottomBatText.text = "";

		if(planet.colony){
		setTrashPlanet.gameObject.SetActive(true);
		colonizeButton.gameObject.SetActive(true);
		trashPlanet.SetActive(true);
		}else{
			setTrashPlanet.gameObject.SetActive(false);
			colonizeButton.gameObject.SetActive(false);
			trashPlanetW.SetActive(false);
			overpopulationW.SetActive(false);
			trashPlanet.SetActive(false);
			garbageW.SetActive(false);
		}
		if(planet.noTrashPlanetWarning.activeSelf){
			trashPlanetW.SetActive(true);
		}else{
			trashPlanetW.SetActive(false);
		}
		if(planet.curPopulation >= planet.population){
			overpopulationW.SetActive(true);
		}else{
			overpopulationW.SetActive(false);
		}
		if(planet.GarbageWarning.activeSelf){
			garbageW.SetActive(true);		
		}else{
			garbageW.SetActive(false);
		}
	}
	public void Close(){
		GetComponent<Image>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;
		open = false;
		bottomBatText.text = "";
		for(int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).gameObject.SetActive(false);
		Manager.changingTrash = false;
		Manager.colonizing = false;
	}
}

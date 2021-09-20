using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertSystem : MonoBehaviour {

	public GameObject trashAlert;
	public GameObject populationAlert;
	public GameObject garbageAlert;

	public Text trashAlertAmount;
	public Text populationAlertAmount;
	public Text garbageAlertAmount;

	public List<PlanetController> trashAlertPlanets;
	public List<PlanetController> populationAlertPlanets;
	public List<PlanetController> garbageAlertPlanets;

	void Update(){
		if(trashAlertPlanets.Count != 0){
			trashAlert.SetActive(true);
			if(trashAlertPlanets.Count > 1)
				trashAlertAmount.text = "" + trashAlertPlanets.Count;
			else
				trashAlertAmount.text = "";
		}else if(trashAlert.activeSelf){
			trashAlert.SetActive(false);
		}
		if(populationAlertPlanets.Count != 0){
			populationAlert.SetActive(true);
			if(populationAlertPlanets.Count > 1)
				populationAlertAmount.text = "" + populationAlertPlanets.Count;
			else
				populationAlertAmount.text = "";
		}else if(populationAlert.activeSelf){
			populationAlert.SetActive(false);
		}
		if(garbageAlertPlanets.Count != 0){
			garbageAlert.SetActive(true);
			if(garbageAlertPlanets.Count > 1)
				garbageAlertAmount.text = "" + garbageAlertPlanets.Count;
			else
				garbageAlertAmount.text = "";
		}else if(garbageAlert.activeSelf){
			garbageAlert.SetActive(false);
		}
	}

	public void TrashedAlertPlanetsClicked(){
		FindObjectOfType<AudioManager>().Play("Click");
		Manager.changingTrash = false;
		Manager.colonizing = false;
		var p = trashAlertPlanets[0];
		Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -10f);
		FindObjectOfType<Manager>().ClickedPlanet(p);
	}

	public void PopulationAlertPlanetsClicked(){
		FindObjectOfType<AudioManager>().Play("Click");
		Manager.changingTrash = false;
		Manager.colonizing = false;
		var p = populationAlertPlanets[0];
		Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -10f);
		FindObjectOfType<Manager>().ClickedPlanet(p);
	}

	public void GarbageAlertPlanetsClicked(){
		FindObjectOfType<AudioManager>().Play("Click");
		Manager.changingTrash = false;
		Manager.colonizing = false;
		var p = garbageAlertPlanets[0];
		Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -10f);
		FindObjectOfType<Manager>().ClickedPlanet(p);
	}

}

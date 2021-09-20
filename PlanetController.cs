using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetController : MonoBehaviour {

	public string planetName;


	public float timeTillTrashAdded;

	public bool colony;

	//Shared Variables
	public int trashCapacity;
	public int curTrash;
	public bool selected;
	public float planetSize;
	public float rotationSpeed;

	//Colony Variables
	[Header("Colony Only")]
	public int shipAmount;
	public int curShipAmount;
	public int population;
	public int curPopulation;
	public GameObject ship;
	public GameObject colonizingShip;
	public PlanetController curTrashPlanet;
	GameObject ring;

	//Trash Variables
	[Header("Trash Only")]
	public bool full;
	public bool trash;


	//Warnings
	[Header("Warnings")]
	public bool hasWarning;
	public GameObject noTrashPlanetWarning;
	GameObject populationWarning;
	GameObject TrashFullWarning;
	public GameObject GarbageWarning;



	bool firstTimeTrash;
	bool firstTimePop;
	bool firstTimeGarbage;

	void Awake(){
		noTrashPlanetWarning = transform.parent.GetChild(1).gameObject;
		populationWarning = transform.parent.GetChild(3).gameObject;
		TrashFullWarning = transform.parent.GetChild(2).gameObject;
		GarbageWarning = transform.parent.GetChild(4).gameObject;
		ring = transform.parent.GetChild(5).gameObject;
	}
	void Start(){
		trashCapacity *= (int)planetSize;
		curShipAmount = shipAmount;
		curPopulation = 10;
		ring.SetActive(false);

		rotationSpeed /= planetSize;

		FindObjectOfType<Manager>().planets.Add(this);

		InvokeRepeating("AddTrash", timeTillTrashAdded,timeTillTrashAdded);
	}

	//#if UNITY_EDITOR || UNITY_STANDALONE
	void OnMouseDown(){
		 if (!EventSystem.current.IsPointerOverGameObject()){
			if(Manager.changingTrash){
				if(colony)
					return;
				FindObjectOfType<Manager>().ChangeTrash(this);
			}else if(Manager.colonizing){
				if(colony || trash)
					return;
				FindObjectOfType<Manager>().Colonizing(this);
			}else{
				FindObjectOfType<Manager>().ClickedPlanet(this);
			}
		 }    
	}
	//#endif

	#if UNITY_IOS || UNITY_ANDROID
	public void PlanetClicked(){
		if(Manager.changingTrash){
			if(colony)
					return;
				FindObjectOfType<Manager>().ChangeTrash(this);
		}else if(Manager.colonizing){
			if(colony || trash)
				return;
			FindObjectOfType<Manager>().Colonizing(this);
		}else{
			FindObjectOfType<Manager>().ClickedPlanet(this);
		}
	}
	#endif

	void Update(){
		transform.Rotate (Vector3.forward * (rotationSpeed * Time.deltaTime));

		if(colony && !ring.activeSelf)
			ring.SetActive(true);

		//Get rid of Trash Planet if it is full
		if(colony && curTrashPlanet != null){
			if(curTrashPlanet.full){
				curTrashPlanet = null;
			}
		}
			if(curTrashPlanet == null && colony){
				noTrashPlanetWarning.SetActive(true);
				if(!firstTimeTrash)
					FindObjectOfType<AlertSystem>().trashAlertPlanets.Add(this);
				firstTimeTrash = true;
			}
			else if(colony){
				noTrashPlanetWarning.SetActive(false);
				FindObjectOfType<AlertSystem>().trashAlertPlanets.Remove(this);
				firstTimeTrash = false;
			}
			if(curPopulation >= population){
				populationWarning.SetActive(true);
				if(!firstTimePop)
					FindObjectOfType<AlertSystem>().populationAlertPlanets.Add(this);
				firstTimePop = true;
			}else{
				populationWarning.SetActive(false);
				FindObjectOfType<AlertSystem>().populationAlertPlanets.Remove(this);
				firstTimePop = false;
			}
			if(curTrash >= trashCapacity && colony){
				GarbageWarning.SetActive(true);
				if(!firstTimeGarbage)
					FindObjectOfType<AlertSystem>().garbageAlertPlanets.Add(this);
				firstTimeGarbage = true;
			}else if(colony){
				GarbageWarning.SetActive(false);
				FindObjectOfType<AlertSystem>().garbageAlertPlanets.Remove(this);
				firstTimeGarbage = false;
			}

			if(full)
				TrashFullWarning.SetActive(true);
			else if(TrashFullWarning.activeSelf)
				TrashFullWarning.SetActive(false);
	}
	//Add trash every few seconds
	public void AddTrash(){
		//Only run if is colony
		if(colony){
			curTrash ++;
			curPopulation++;
			PlayerPrefs.SetInt("TotalPop", PlayerPrefs.GetInt("TotalPop") + 1);

			if(curShipAmount > 0 && curTrashPlanet != null){
				curTrash --;
				curShipAmount --;
				var s = Instantiate(ship, transform.position, transform.rotation).GetComponent<ShipController>();
				s.toTrash = true;
				s.homePlanet = this;
				s.trashPlanet = curTrashPlanet;
				s.trashAmount ++;
			}

			if(curTrash >= trashCapacity + 5 )
				FindObjectOfType<GameOverScreen>().TheGameIsOver(this, "trash overflow");
			if(curPopulation >= population + 5)
				FindObjectOfType<GameOverScreen>().TheGameIsOver(this, "overpopulation");
		}
	}
	
	//Detect Ship
	void OnTriggerStay(Collider other){
		var ship = other.GetComponent<ShipController>();
		//If hit by colony ship
		if(ship.colonizing && ship.homePlanet != this){
			if(ship.trashPlanet != this)
				return;
			if(!colony)
				PlayerPrefs.SetInt("ColonyP", PlayerPrefs.GetInt("ColonyP") + 1);
			colony = true;
			curPopulation = ship.population;
			Destroy(ship.gameObject);
			transform.tag = "Colony";
			 return;
		}
		//Get Returning ships
		if(colony && !ship.toTrash && ship.homePlanet == this){
			if(ship.trashAmount != 0)
				curTrash += ship.trashAmount;
			Destroy(ship.gameObject);
			
			if(curTrash > 0 && curTrashPlanet != null){
				curTrash --;
				var s = Instantiate(ship, transform.position, transform.rotation).GetComponent<ShipController>();
				s.enabled = true;
				s.toTrash = true;
				s.homePlanet = this;
				s.trashPlanet = curTrashPlanet;
				s.trashAmount ++;
			}else{
				curShipAmount++;
			}
		//Trash planets gain trash
		}else if(ship.toTrash && transform.tag != "Colony" && ship.trashPlanet == this){
			curTrash  += ship.trashAmount;
			PlayerPrefs.SetInt("TotalTrash", PlayerPrefs.GetInt("TotalTrash") + ship.trashAmount);
			ship.trashAmount = 0;
			ship.toTrash = false;
			if(!trash)
				PlayerPrefs.SetInt("TrashP", PlayerPrefs.GetInt("TrashP") + 1);
			trash = true;
			if(curTrash == trashCapacity)
				full = true;

		}
	}
	//Sends a colony ship to a planet
	public void SendColonyShip(PlanetController colony){
		var s = Instantiate(colonizingShip, transform.position, transform.rotation).GetComponent<ShipController>();
		s.toTrash = true;
		s.homePlanet = this;
		s.colonizing = true;
		s.trashPlanet = colony;
		s.population = curPopulation / 2;
		curPopulation /= 2;
	}
}

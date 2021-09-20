using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public bool colonizing;
	public int population;

	public float speed;
	public int trashAmount;

	public PlanetController homePlanet;
	public PlanetController trashPlanet;

	public bool toTrash;

	void Update(){
		Vector3 vectorToTarget = transform.position - trashPlanet.transform.position;
 		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
 		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
 		transform.rotation = q;

		if(trashPlanet == null || (trashPlanet.full && toTrash))
			toTrash = false;

		if(toTrash){
			transform.position = Vector3.MoveTowards(transform.position, trashPlanet.transform.position, speed * Time.deltaTime);
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, homePlanet.transform.position, speed * Time.deltaTime);
			GetComponent<SpriteRenderer>().flipX = false;
		}

	}
}

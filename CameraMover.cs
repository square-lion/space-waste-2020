using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_STANDALONE

	public float speed;
	//Scroll
	int minFov = 5;
	int maxFov = 25;


	void Update () {
		  var direction = Vector3.zero;
     if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
        direction += Vector3.left;  
     }
     if(Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.DownArrow)) {
        direction += Vector3.down;  
     }
     if(Input.GetKey(KeyCode.D) ||  Input.GetKey(KeyCode.RightArrow)) {
        direction += Vector3.right;  
     }
     if(Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.UpArrow)) {
        direction += Vector3.up;  
     }

	  transform.Translate(direction * speed * .1f);

		/* 
		if(Input.GetKey(KeyCode.W))
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
		if(Input.GetKey(KeyCode.S))
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,-speed);

		if(Input.GetKey(KeyCode.A))
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
		if(Input.GetKey(KeyCode.D))
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);


		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)){
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		*/

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -45f, 45f),
                                         Mathf.Clamp(transform.position.y, -45f, 45f),
                                         transform.position.z);

	}
	public void OnGUI(){
     if(Event.current.type == EventType.ScrollWheel){
		 var fov = Camera.main.orthographicSize;
         fov += Event.current.delta.y;
		 fov = Mathf.Clamp(fov, minFov, maxFov);
		 Camera.main.orthographicSize = fov;
	}

 }

 #endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_STANDALONE
	void OnMouseDown(){
		if (!EventSystem.current.IsPointerOverGameObject()){
			FindObjectOfType<PlanetInfoScreen>().Close();
		}
	}
	#endif

	/*
	#if UNITY_IOS || UNITY_ANDROID
	void Update(){
		if(Input.touchCount == 1)
			if(Input.GetTouch(0).phase == TouchPhase.Began)
				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
					FindObjectOfType<PlanetInfoScreen>().Close();
	}
	#endif
	*/
}

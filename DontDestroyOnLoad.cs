using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var g = FindObjectsOfType<DontDestroyOnLoad>();

		if(g.Length > 1)
			Destroy(this.gameObject);
		 
		 DontDestroyOnLoad(this.gameObject);
	}
}

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	public static GameObject instance;

	void Awake(){
		foreach(Sound s in sounds){
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}	
	}
	public void Play(string name){
		Sound s = Array.Find(sounds, sounds => sounds.name == name);
		if(s == null){
			Debug.LogWarning("The Audio File " + name + " could not be found.");
			return;
		}
		s.source.Play();
	}
}

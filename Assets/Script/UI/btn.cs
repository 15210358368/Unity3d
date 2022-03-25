using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void onclik1(){
	
	
		if (gameObject.name == "Button") {
			SceneManager.LoadScene(1); 
		
		}
		if (gameObject.name == "Button1") {
			SceneManager.LoadScene (2); 
		}
	
		if (gameObject.name == "Button2") {
			SceneManager.LoadScene (3); 
		}

		if (gameObject.name == "Button3") {
			SceneManager.LoadScene (4); 
		}
		if (gameObject.name == "Button4") {
			SceneManager.LoadScene (5); 
		}

	
		if (gameObject.name == "Button5") {
			SceneManager.LoadScene (6); 
		}
		if (gameObject.name == "ly") {
			SceneManager.LoadScene ("comment"); 
		}
	}
}

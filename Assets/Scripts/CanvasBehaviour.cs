using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasBehaviour : MonoBehaviour {

	public AudioBrowser AudioBrowser;
	public static GameObject openGUI;

	// Use this for initialization
	void Start () {
		openGUI = transform.FindChild ("Button").gameObject;
	}
	
	// Update is called once per frame
	void Update () { 
		//Init Button in OpenGUI Object
		Button triggerGUI = openGUI.GetComponent<Button> ();

		openGUI.SetActive (!(AudioBrowser.openGUI));
		triggerGUI.onClick.AddListener (() => {openBrowser();});
	}
	
	void openBrowser(){
		AudioBrowser.openGUI = true;
	}
}

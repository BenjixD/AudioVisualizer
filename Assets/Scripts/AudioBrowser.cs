using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class AudioBrowser : MonoBehaviour {

	//skins and textures
	public GUISkin[] skins;
	public Texture2D file,folder,back,drive;
	public bool openGUI = true;
	public AudioSource aSource;

	//initialize file browser
	FileBrowser fb = new FileBrowser();
	string output = "No File Selected";

	// Use this for initialization
	void Start () {
		//setup file browser style
		//fb.guiSkin = skins[0]; //set the starting skin

		//set the various textures
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;

		//show the search bar
		fb.showSearch = true;
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = true;
	}

	void OnGUI(){
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Space(10);

		//select from available gui skins
		//fb.guiSkin = s;
			
		GUILayout.Space(10);
		GUILayout.EndVertical();
		GUILayout.Space(10);

		GUILayout.Label("Selected File: "+output);
		GUILayout.EndHorizontal();

		/*
		//draw and display output
		if(fb.draw()){ //true is returned when a file has been selected
			//the output file is a member if the FileInfo class, if cancel was selected the value is null
			output = (fb.outputFile==null)?"cancel hit":fb.outputFile.ToString();		
		}*/
		if (openGUI) {
			if (fb.draw()) {
				//the output file is a member if the FileInfo class, if cancel was selected the value is null
				output = (fb.outputFile==null)?"No File Selected":fb.outputFile.ToString();
				if (fb.outputFile == null) {
					openGUI = false;
				} else {
					//Match match = Regex.Match (fb.outputFile.ToString(), ".*Resources[\\\\](.*)[.].*"); //Note \\\\ matches actually 1 backslash
					//aSource.clip = Resources.Load (match.Groups[1].Value) as AudioClip;
					//aSource.clip = www.audioClip;
					//aSource.Play ();

					StartCoroutine(getAndPlayAudio ("file:///" + fb.outputFile.ToString ()));
					openGUI = false;
				}
			}
		}
	}

	IEnumerator getAndPlayAudio(string song){
		Debug.Log (song);
		WWW www = new WWW (song);
		while (www.isDone == false) {
			yield return www;
		}
			
		aSource.clip = www.GetAudioClip(false, false);
		while (aSource.clip.isReadyToPlay == false) {
			yield return 0;
		}

		aSource.Play();
	}
}

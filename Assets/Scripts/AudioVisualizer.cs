using UnityEngine;
using System.Collections;

public class AudioVisualizer: MonoBehaviour {
	public float offsetX = 0f;
	public int inverted = 1;
	public static int sampleSize = 64;
	public static float spacingMultiplier = 0.3f;
	public static int linePadder = 0; //pads points to left and right 
	public AudioSource aSource;
	private float[] aSamples = new float[sampleSize];
	private Vector3[] linePoints = new Vector3[sampleSize + 2*linePadder];
	private LineRenderer lRenderer;

	//Pre game-initialization
	void Awake (){
		//Get and store a reference to the following attached components:  
		//AudioSource  
		//this.aSource = GetComponent<AudioSource>();  
		//LineRenderer  
		this.lRenderer = GetComponent<LineRenderer>(); 
	}

	// Use this for initialization
	void Start () {
		//Initialize Posiiton of line
		transform.position = new Vector2 (-(sampleSize + 2*linePadder)/2 * spacingMultiplier + offsetX, transform.position.y);

		//Initialize the points on the line renderer
		for(int i = 0; i < linePoints.Length; i++){
			linePoints [i] = new Vector3 ((float)i * spacingMultiplier, 0, 0);
		}

		//Set size and push the points to the line
		lRenderer.SetVertexCount (linePoints.Length);
		lRenderer.SetPositions(linePoints);
	}


	// Update is called once per frame
	void FixedUpdate () {
		//New line point pos declarations
		Vector3[] finalPos = new Vector3[linePoints.Length];
		System.Array.Copy (linePoints, finalPos, linePoints.Length);

		//Obtain audio frequency bands
		aSource.GetSpectrumData(aSamples,0,FFTWindow.BlackmanHarris);

		//Update the final positions init
		for(int i = 0; i < sampleSize; i++){
			Vector3 offset = new Vector3 (0, Mathf.Clamp(aSamples[i] * (5 + i*i), 0, 3), 0);
			finalPos [i+linePadder] += offset * inverted; 
		}

		//set the final positions
		lRenderer.SetPositions(finalPos);
	}
}

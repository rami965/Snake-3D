using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	public Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = "Score: " + Score.score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

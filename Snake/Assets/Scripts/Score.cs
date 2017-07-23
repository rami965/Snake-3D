using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static int score = 0;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//get the score
		score = FruitRotator.count;
	}
}

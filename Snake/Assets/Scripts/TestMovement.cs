using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMovement : MonoBehaviour {

	private LevelManager levelManager;
	public List<Transform> bodyParts = new List<Transform>();
	public float minDistance = 0.015f;
	public float speed = 1.0f;
	public float rotationSpeed = 50.0f;
	public GameObject bodyPartPrefab;
	public GameObject rockPrefab;
	public GameObject grassPrefab;
	public GameObject grassPrefab2;
	public int startBodySize = 1;
	public int noOfRocks = 2;
	public int noOfGrass = 50;
	public int noOfGrass2 = 50;

	//movement variables
	private float distance;
	private Transform currentBodyPart;
	private Transform prevBodyPart;
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//rotation variables
	private Quaternion startingRotation;
	private float angel = 90;
	private float fTurnRate = 90.0f;
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//swipe variables with touch
	private Vector2 firstPressPosition;
	private Vector2 secondPressPosition;
	private Vector2 swipeDirection;
	//swipe variables with mouse
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//rock instance position bounds
	private int boardMinDistance = -8;
	private int boardMaxDistance = 8;

	//start function
	void Start(){
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//get starting rotation
		if(bodyParts.Count != 0)
		startingRotation = bodyParts[0].rotation;
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//adding body parts in the begining
		for(int i=0; i<startBodySize - 1; i++){
			addBodyPart();
		}

		//adding rocks
		for(int j=0; j<noOfRocks; j++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.75f, Random.Range(boardMinDistance, boardMaxDistance));
			//check the rock object
			if(rockPrefab != null) {
				GameObject go = Instantiate(rockPrefab, newPosition, Quaternion.identity) as GameObject;
			}
		}

		//adding grass1
		for(int k=0; k<noOfGrass; k++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			//check the grass1 object
			if(grassPrefab != null) {
				GameObject go = Instantiate(grassPrefab, newPosition, Quaternion.Euler(new Vector3(-90,0,0))) as GameObject;
			}
		}

		//adding grass2
		for(int l=0; l<noOfGrass2; l++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			//check the grass2 object
			if(grassPrefab2 != null) {
				GameObject go = Instantiate(grassPrefab2, newPosition, Quaternion.Euler(new Vector3(-90,0,0))) as GameObject;
			}
		}

		//get level manager
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

	}
     
     void Update () {
     	move();

     	//TODO Add bodyparts
     	if(Input.GetKeyUp(KeyCode.Q)) addBodyPart();
     }

     //move function
     public void move(){
     	float currentSpeed = speed;

     	if(Input.GetKey(KeyCode.UpArrow)) currentSpeed *= 2;

     	//move the head
     	if(bodyParts.Count != 0)
     	bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//rotate the head with directions
		//go to 90 degrees with right arrow
		if( (Input.GetKeyDown( KeyCode.RightArrow ) || SwipeManager.IsSwipingRight()) && bodyParts.Count != 0){
	        StopAllCoroutines();
	        StartCoroutine(Rotate(angel));
	        angel+=90;
			bodyParts[0].Rotate (-Vector3.forward * fTurnRate * Time.deltaTime);
	    }

	    //go to -90 degrees with left arrow
		if( (Input.GetKeyDown( KeyCode.LeftArrow ) || SwipeManager.IsSwipingLeft()) && bodyParts.Count != 0){
	        StopAllCoroutines();
	        StartCoroutine(Rotate(angel-180));
	        angel-=90;
			bodyParts[0].Rotate (Vector3.forward * fTurnRate * Time.deltaTime);
	    }

	    //move the head
		if(bodyParts.Count != 0)
		bodyParts[0].position = bodyParts[0].position + bodyParts[0].forward * 2.0f * Time.deltaTime;
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

     	for(int i = 1; i<bodyParts.Count; i++){
     		currentBodyPart = bodyParts[i];
     		prevBodyPart = bodyParts[i-1];

     		//calc distance between current and prev bodyPart
     		distance = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

     		//previous part position
     		Vector3 newPosition = prevBodyPart.position;

     		//lock the y to prevent bodypart from moving up
     		newPosition.y = bodyParts[0].position.y;

			//calc time delta
			float dTime = Time.deltaTime * (distance/minDistance) * currentSpeed;

			//keep time delta small to prevent slow movement (frame drop)
			if(dTime > 0.5f) dTime = 0.5f;

			//move between current position and new position
			currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, dTime);

			//rotate current body part
			currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, dTime);
     	}
     }

     public void addBodyPart(){
		if(bodyParts.Count != 0){
			//instantiate after last bodypart in the list
			Transform newPart = (Instantiate(bodyPartPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count -1].rotation) as GameObject).transform;
			//set parent to be this transform
			newPart.SetParent(transform);
			//add the new part to the list
			bodyParts.Add(newPart);
		}
     }

     //slow rotation function
	 IEnumerator Rotate(float rotationAmount){
	    Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;

	    while(bodyParts[0].rotation != finalRotation){
			bodyParts[0].rotation = Quaternion.Lerp(bodyParts[0].rotation, finalRotation, Time.deltaTime*rotationSpeed);
	        yield return null;
	    }
	}

	//trigger wall collision
	void OnTriggerEnter(Collider other){
		//Debug.Log("Triggredd");
		//collide with wall or rock
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Rock")){
			Debug.Log("Triggred With Wall");
			//goto lose screen
			levelManager.LoadLevel("Lose");
		}
	}

	//collision 
	void OnCollisionEnter(Collision collision){
		Debug.Log("Collision");
	}

}

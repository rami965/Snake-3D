using UnityEngine;
using System.Collections;

public class FruitRotator : MonoBehaviour {

	public GameObject snakeObject;
	public Transform particles;
	private int minDistance = -8;
	private int maxDistance = 8;
	private AudioSource audioSrc;
	private Vector3 newPosition;

	//score
	public static int count;

	// Use this for initialization
	void Start () {
		//initialize counter
		count = 0;

		//initialize audio source
		audioSrc = GetComponent<AudioSource>();

		//stop particles
		particles.GetComponent<ParticleSystem>().enableEmission = false;

		//new position
		newPosition = new Vector3(Random.Range(minDistance, maxDistance), 0.5f, Random.Range(minDistance, maxDistance));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 120, 0) * Time.deltaTime);
	}

	//add body part when colliding with head
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Head")){
			snakeObject.GetComponent<TestMovement>().addBodyPart();

			//start particles
			particles.GetComponent<ParticleSystem>().enableEmission = false;
			StartCoroutine(stopParticles());

			//play sound
			audioSrc.Play();

			//get new position
			StartCoroutine(getNewPosition());

			//hide apple
			this.gameObject.SetActive(false);
			//Debug.Log("Triggred");

			//set new position for spawn
			this.gameObject.transform.position = newPosition;

			//show apple in new position
			this.gameObject.SetActive(true);

			//increase score counter
			count++;
			//Debug.Log(count);
		}
	}

	//get new position for spawn
	IEnumerator getNewPosition(){
		while(Physics.CheckSphere(newPosition, 1.0f)){
			//wrong place to spawn
			Debug.Log("Wrong Place");
			newPosition = new Vector3(Random.Range(minDistance, maxDistance), 0.5f, Random.Range(minDistance, maxDistance));
			yield return null;
		}
	}

	//stop particles after collision
	IEnumerator stopParticles(){
		yield return new WaitForSeconds(0.4f);
		//stop particles
		particles.GetComponent<ParticleSystem>().enableEmission = false;
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 cameraInitialPosition;
	private Vector3 cameraInitialRotation;
	public GameObject mainCamera;
	public GameObject snakeHead;


	//CAMERA POSITIONS
	enum CameraPosition {close, middle, far};

	//current camera position
	CameraPosition currentCameraPostion;

	// Use this for initialization
	void Start () {
		initializeCameraDetails();
	}

	//Initial camera details
	void initializeCameraDetails(){
		//initial camera position
		currentCameraPostion = CameraPosition.close;

		//camera initial position and rotation
		cameraInitialPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, mainCamera.transform.localPosition.z);
		cameraInitialRotation = new Vector3(mainCamera.transform.localRotation.eulerAngles.x, mainCamera.transform.localRotation.eulerAngles.y, mainCamera.transform.localRotation.eulerAngles.z);
		Debug.Log(cameraInitialPosition + "   " + cameraInitialRotation.y);
	}

	//change position of the camera
	public void changeCameraPosition(){
		if (currentCameraPostion == CameraPosition.close){
			currentCameraPostion = CameraPosition.middle;


			Transform newTransform = snakeHead.transform;
			Vector3 newPosition = newTransform.position;
			mainCamera.transform.rotation = newTransform.rotation;
			mainCamera.transform.position = newPosition;
		///////////////////////////////////////////////////////////////////////////////////////////////////
		}else if(currentCameraPostion == CameraPosition.middle){
			currentCameraPostion = CameraPosition.far;

			mainCamera.transform.localRotation = Quaternion.Euler(cameraInitialRotation.x, cameraInitialRotation.y, cameraInitialRotation.z);
			mainCamera.transform.localPosition = new Vector3(cameraInitialPosition.x, cameraInitialPosition.y + 2f, cameraInitialPosition.z - 3f);
			Debug.Log(cameraInitialPosition + "   " + cameraInitialRotation.y);
		///////////////////////////////////////////////////////////////////////////////////////////////////
		}else if(currentCameraPostion == CameraPosition.far){
			currentCameraPostion = CameraPosition.close;

			mainCamera.transform.localRotation = Quaternion.Euler(cameraInitialRotation.x, cameraInitialRotation.y, cameraInitialRotation.z);
			mainCamera.transform.localPosition = cameraInitialPosition;
			Debug.Log(cameraInitialPosition + "   " + cameraInitialRotation.y);
		}
	}
}

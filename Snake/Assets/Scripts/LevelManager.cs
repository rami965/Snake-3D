using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	AsyncOperation ao;
	public Text loadingText;
	public Button startButton;

	public void LoadLevel (string name){
		Debug.Log("Loaded Level: "+name);
		//Application.LoadLevel(name);
		SceneManager.LoadScene(name);
	}
	
	public void QuitLevel (){
		Debug.Log("Quit");
		Application.Quit();
	}
	
	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	//loading the game scene
	public void loadingGame(){
		//hide start button
		startButton.gameObject.SetActive(false);

		//show Loading text
		loadingText.gameObject.SetActive(true);
		StartCoroutine(loadGameWithProgress());
	}


	//loading with progress
	IEnumerator loadGameWithProgress(){
		yield return new WaitForSeconds(1);

		ao = SceneManager.LoadSceneAsync(1);

		ao.allowSceneActivation = false;

		while(!ao.isDone){
			Debug.Log("Loading ...");

			if(ao.progress == 0.9f){ //level is completely loaded
				ao.allowSceneActivation = true;
			}

			Debug.Log(ao.progress);
			yield return null;
		}
	}
}

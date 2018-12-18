using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myNewHelper : MonoBehaviour {
    
	public GameObject umPlayer;
	public GameObject controllerMain;
	public GameObject audioPlayer;
	public GameObject startSlomo;
	public GameObject endSlomo;
	public GameObject video_Canvas;
	//private Component player;

    //0 = normal
    //1 = slomo
    //2 = hast
	private int slomoStage = 0;
	private float rotateStartPoint_video = 0;
	private float rotateStartPoint_controller = 0;
	private int dragging = 0;

	// Use this for initialization
	void Start () {
		Debug.Log("starting");
		//player = umPlayer.GetComponent<UniversalMediaPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GvrControllerInput.ClickButtonDown && slomoStage == 0){
			//TogglePlaySpeed();
			StartCoroutine(Slomo10seconds());
		}

		if (GvrControllerInput.AppButtonDown && dragging == 0)
        {
			//start rotating
			rotateStartPoint_video = video_Canvas.transform.rotation.eulerAngles.y;
			rotateStartPoint_controller = GvrControllerInput.Orientation.eulerAngles.y;

			Debug.Log("click down");
			Debug.Log("video start y " );
			Debug.Log(rotateStartPoint_video);
			Debug.Log("controller start y " );
			Debug.Log(rotateStartPoint_controller);
			dragging = 1;

        }

		if (dragging == 1)
        {
			float new_controler_y = GvrControllerInput.Orientation.eulerAngles.y;
			float new_video_x = video_Canvas.transform.rotation.eulerAngles.x;
			float new_video_y = video_Canvas.transform.rotation.eulerAngles.y;
			float new_video_z = video_Canvas.transform.rotation.eulerAngles.z;
			float midified_y = rotateStartPoint_video + new_controler_y - rotateStartPoint_controller;
            
			Quaternion rot = video_Canvas.transform.rotation;
			rot.eulerAngles = new Vector3(new_video_x, midified_y , new_video_z);
			video_Canvas.transform.rotation = rot;

			Debug.Log("click keeping down");
            Debug.Log("video modified y ");
            Debug.Log(midified_y);
            Debug.Log("new controller y  ");
            Debug.Log(new_controler_y);
        }

		if (GvrControllerInput.AppButtonUp)
        {
   
            dragging = 0;

        }


		AdjustPitch();
		
	}

	void TogglePlaySpeed (){
		if (slomoStage == 0){
			umPlayer.GetComponent<UniversalMediaPlayer>().PlayRate = 0.5f;
			slomoStage = 1;
		} else {
			umPlayer.GetComponent<UniversalMediaPlayer>().PlayRate = 1.0f;
			umPlayer.GetComponent<UniversalMediaPlayer>().Position = 1.0f;
			slomoStage = 0;
		}
	}
    
	void AdjustPitch (){
		
		if (slomoStage == 0){
			if (audioPlayer.GetComponent<AudioSource>().pitch > 1.0f)
            {
                audioPlayer.GetComponent<AudioSource>().pitch -= 0.001f;
              
			}else if (audioPlayer.GetComponent<AudioSource>().pitch < 1.0f){
				audioPlayer.GetComponent<AudioSource>().pitch += 0.001f;
			}

		} else if(slomoStage == 1){
			if (audioPlayer.GetComponent<AudioSource>().pitch > 0.5f)
            {
                audioPlayer.GetComponent<AudioSource>().pitch -= 0.001f;
            }
		} else if(slomoStage == 2){
			if (audioPlayer.GetComponent<AudioSource>().pitch < 1.3f)
            {
                audioPlayer.GetComponent<AudioSource>().pitch += 0.001f;
            }
		}
      

	}

	IEnumerator Slomo10seconds()
    {
		if (slomoStage == 0)
        {
			umPlayer.GetComponent<UniversalMediaPlayer>().PlayRate = 0.5f;
			startSlomo.GetComponent<AudioSource>().Play();
			slomoStage = 1;
        }
		yield return new WaitForSeconds(8);

		if (slomoStage == 1) 
        {
			umPlayer.GetComponent<UniversalMediaPlayer>().PlayRate = 1.8f;
			endSlomo.GetComponent<AudioSource>().Play();
			slomoStage = 2;
        }
        
		yield return new WaitForSeconds(6.5f);
        
		if (slomoStage == 2)
        {
			umPlayer.GetComponent<UniversalMediaPlayer>().PlayRate = 1.0f;

            slomoStage = 0;
        }

    }
}

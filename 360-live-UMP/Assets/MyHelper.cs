using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleVR.VideoDemo
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using Gvr.Internal;

    

	public class MyHelper : MonoBehaviour
	{
		public GameObject mainManu;

        public GameObject cubeRoom;
        
		public GameObject displayCanvas360;

		public GameObject sliderBackground;

		public float sliderPercent = 99.9f;

		[Tooltip("Reference to GvrControllerMain")]
        public GameObject controllerMain;
        public static string CONTROLLER_MAIN_PROP_NAME = "controllerMain";

		public float delay = 1f;

		private bool doneAutoStart;

		private float t;

		private bool frozen = false;
        


        
		// Use this for initialization
		void Start()
		{
			t = 0;
			doneAutoStart = false;
			//controller = daydreamController.GetComponent<GvrControllerInput>();

		}
        
		// Update is called once per frame

		void Update()
		{
            
			if (cubeRoom.activeSelf == false){

				if (GvrControllerInput.ClickButtonDown )
				{
					if (!frozen)
					{
						StartCoroutine(FreezeFiveSeconds());
					} else
                    {
                        sliderBackground.GetComponent<ScrubberEvents>().GoToEnd(sliderPercent);
                        displayCanvas360.GetComponent<VideoControlsManager>().OnPlayPause();
                        frozen = false;
                    }
				}

				//if (GvrControllerInput.AppButtonDown)
                //{
                //    Debug.Log("Pressed app button.");

                //}

			}         

			if (!doneAutoStart)
            {
				t += Time.deltaTime;
                if (t >= delay)
                {
                    mainManu.GetComponent<SwitchVideos>().On360Video();
                    cubeRoom.SetActive(false);
                    doneAutoStart = true;
                    Debug.Log("Done auto start, and let you know I can print messages");
                }
            }
		}
       

		IEnumerator FreezeFiveSeconds(){
			if (!frozen)
			{
				// pause
				displayCanvas360.GetComponent<VideoControlsManager>().OnPlayPause();
				frozen = true;
			}
			yield return new WaitForSeconds(5);
			if (frozen){
				
    			sliderBackground.GetComponent<ScrubberEvents>().GoToEnd(sliderPercent);
    			displayCanvas360.GetComponent<VideoControlsManager>().OnPlayPause();
				frozen = false;
			}
		}
	}
}
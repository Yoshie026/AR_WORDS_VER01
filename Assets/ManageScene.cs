using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
     //Direct Light
     GameObject dLight;

     //AR Camera
     GameObject arCam; 
    // Start is called before the first frame updat
     private int buildIndex;

     public float transitionTime = 0.5f;

     public Animator transition;
     void Awake()
    {
         buildIndex = SceneManager.GetActiveScene().buildIndex;
          Debug.Log(buildIndex);
         //Direct Light
         dLight = GameObject.Find("Directional Light");
         //AR Camera
         arCam = GameObject.Find("AR Camera");
    }

     public void switchArView()
    {     
         StartCoroutine(LoadLevel("LOCATION", "out"));  
              //AR camera back on 
              arCam.SetActive(true);
              //Direct Light back on
              dLight.SetActive(true);
     }

    
    public void switchToMapView()
    {
         StartCoroutine(LoadLevel("ZoomableMap", "map_in"));
          //AR camera off
              arCam.SetActive(false);
          //Direct Light off
              dLight.SetActive(false);
    }
    public void switchToAboutPage()
    {
         StartCoroutine(LoadLevel("ABOUT","about_in"));
              //AR camera off
              arCam.SetActive(false);
              //Direct Light off
              dLight.SetActive(false);
    }

    IEnumerator LoadLevel(string levelName, string trans)
    {
     transition.SetTrigger(trans);
     yield return new WaitForSeconds(transitionTime);
     SceneManager.LoadScene(levelName,LoadSceneMode.Single); 
    }
 }        
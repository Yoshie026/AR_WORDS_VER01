using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(ARRaycastManager))]
public class PlacementControllerWithMultiple : MonoBehaviour
{
    [SerializeField]
    private Button arNoisy;

    [SerializeField]
    private Button arCenter;

    [SerializeField]
    private Button arOnlyNoisy;
    
    [SerializeField]
    private Button arOnly;

    private GameObject placedPrefab;
    private GameObject obj;
    public Slider slider;
    public Slider sliderHeight;

    private float scale;
    private float height;
    // private GameObject obj;

    

    private ARRaycastManager arRaycastManager;

    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    
        // set initial prefab
        arNoisy.onClick.AddListener(() => ChangePrefabTo("03_0305_noisy_full"));
        arOnly.onClick.AddListener(() => ChangePrefabTo("02_0305_only_full"));
        arCenter.onClick.AddListener(() => ChangePrefabTo("01_0305_center_full"));
    }

     void ChangePrefabTo(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");


        // if(placedPrefab == null)
        // {
        //     Debug.LogError($"Prefab with name {prefabName} could not be loaded, make sure you check the naming of your prefabs...");
        // }
    }
    //     public static bool IsPointerOverGameObject(Vector2 screenPosition) 
    // {   
    //     PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);    
    //     eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);  
    //     List<RaycastResult> results = new List<RaycastResult>();   
    //     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);  
    //     return results.Count > 0;  
    // }


//  private bool IsPointerOverUIObject() {
//          PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
//          eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//      List<RaycastResult> results = new List<RaycastResult>();
//      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
//      return results.Count > 0;
//  }

    // bool TryGetTouchPosition(out Vector2 touchPosition)
    // {
    //     if(Input.touchCount > 0)
    //     {
    //         touchPosition = Input.GetTouch(0).position;
    //             // if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)||!IsPointerOverUIObject())
    //             // {
    //             return true;
    //             }

                   
    //     touchPosition = default;
    //     return false;
    // }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }


     private bool IsPointerOverUIObject() {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
 }


        public void OnSliderValueChanged()
    {
        scale = slider.value*1.5f;
    }

    public void OnSliderValueChangedHeight()
    {
        height = sliderHeight.value*1.5f;
        //Debug.LogError("slider "+ scale);
    }


    void Update()
    {
        if(placedPrefab == null)
        {
            return;
        }
        if(!TryGetTouchPosition(out Vector2 touchPosition))
         {   
            return;
         }

        foreach (var touch in Input.touches)
        {
         if (IsPointerOverUIObject())
         {
             print("return touch");
             return;
         }
        }
    
                Debug.LogError("slider "+ scale);
        if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
                // Detect touch event

     // Other code...
            var hitPose = hits[0].pose;
            obj = Instantiate(placedPrefab, hitPose.position, hitPose.rotation * Quaternion.Euler(1f, 180f, 1f));
            // obj = Instantiate(placedPrefab, hitPose.position, hitPose.rotation * Quaternion.Euler (1f, 180f, 1f));
        }
            obj.transform.localScale = new Vector3(scale,scale,scale); // change its local scale in x y z format

         }

    


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
//     void Update()
//     {
//         if(!TryGetTouchPosition(out Vector2 touchPosition))
//         // if(!TryGetTouchPosition(out Vector2 touchPosition) && !IsPointerOverUIObject())
//         Debug.LogError("hey hey hey");
//         if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
//         {
//             var hitPose = hits[0].pose;
//            // if(obj == null){

//             obj = Instantiate(placedPrefab, hitPose.position, hitPose.rotation * Quaternion.Euler (1f, 180f, 1f));
//             obj.transform.localScale = new Vector3(scale,scale,scale); // change its local scale in x y z format
//             //obj.transform.position = new Vector3(0.0f, height, 0.0f); // change its local scale in x y z format
//             // }else
//             // {
//             //         obj.transform.position = hitPose.position;
//             //}
//          }
//     }
//     static List<ARRaycastHit> hits = new List<ARRaycastHit>();
// }



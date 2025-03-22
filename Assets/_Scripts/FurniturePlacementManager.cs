using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FurniturePlacementManager : MonoBehaviour
{
    public GameObject SpawnableFurniture;
    public ARSessionOrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            // Shoot Raycast to place the objects
            // Disable planes and The Plane Manager
            bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
            if (collision && isButtonPressed() == false)
            {
                GameObject _object = Instantiate(SpawnableFurniture);
                _object.transform.position = raycastHits[0].pose.position;
                _object.transform.rotation = raycastHits[0].pose.rotation;

            }
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
            planeManager.enabled = false;
        }
    }

    public bool isButtonPressed()
    {
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else{
            return true;
        }
    }

    public void SwitchFurniture(GameObject furniture)
    {
        SpawnableFurniture = furniture;
    }

}



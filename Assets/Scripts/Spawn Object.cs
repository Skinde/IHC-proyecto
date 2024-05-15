using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectOnClick : MonoBehaviour
{
   
    [SerializeField] public GameObject objectToSpawn; // The object to spawn
    [SerializeField] public Button button; //The Button To Click
    [SerializeField] public Camera Maincamera; //The Player Camera

    void Start()
    {
        // Ensure that the UI button has an onClick listener attached
        
        if (button != null)
        {
            button.onClick.AddListener(SpawnObject);
        }
        else
        {
            Debug.LogError("No Button component found on GameObject.");
        }
    }

    void SpawnObject()
    {
        // Check if the object to spawn and the spawn point are assigned
        if (objectToSpawn != null)
        {
            // Instantiate the object at the spawn point's position and rotation
            Ray ray = Maincamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            Vector3 hitpoint;
            Vector3 Rotation = new Vector3(Maincamera.transform.rotation.eulerAngles.x * 0, Maincamera.transform.rotation.eulerAngles.y, Maincamera.transform.rotation.eulerAngles.z * 0);
            Quaternion Quaternion_rotation = Quaternion.Euler(Rotation);
            if (Physics.Raycast(ray, out hit))
            {
                hitpoint = hit.point;
                Instantiate(objectToSpawn, hitpoint, Quaternion_rotation);
            }
            
        }
        else
        {
            Debug.LogError("Object to spawn or spawn point not assigned.");
        }
    }
}
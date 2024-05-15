using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] public Button MoveButton; //The Button To Click
    [SerializeField] public Button RotateButton;
    [SerializeField] public Button DeleteButton;
    [SerializeField] public Camera Maincamera; //The Player Camera
    


    private GameObject Actor = null;
    private int Mode = 0;
    private Outline outline = null;
    // Start is called before the first frame update
    void Start()
    {
        MoveButton.GetComponent<Image>().color = Color.white;
        MoveButton.onClick.AddListener(SetMoveMode);
        RotateButton.onClick.AddListener(SetRotateMode);
        DeleteButton.onClick.AddListener(SetDeleteMode);

    }

    // Update is called once per frame
    void Update()
    {
        
        Touch touch = Input.GetTouch(0);
        switch (Mode)
        {
            //Select object to move mode
            case 1:
            
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit hit;
                    Ray ray = Maincamera.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Actor"))
                        {
                            Actor = hit.collider.gameObject;
                            outline = Actor.AddComponent<Outline>();
                            outline.OutlineMode = Outline.Mode.OutlineAll;
                            outline.OutlineColor = Color.yellow;
                            outline.OutlineWidth = 5f;
                            Mode = 2;
                            
                        }
                    }
                }
            break;

            //Drag the actor to it's new position
            case 2:
          
                if (touch.phase == TouchPhase.Began)
                {
                    if (Actor != null)
                    {
                        RaycastHit hit;
                        Ray ray = Maincamera.ScreenPointToRay(Input.GetTouch(0).position);
                        Vector3 hitpoint;
                        if (Physics.Raycast(ray, out hit))
                        {
                            hitpoint = hit.point;
                            if (hit.collider.tag != "Actor")
                            {
                                Actor.transform.position = hitpoint;
                                MoveButton.GetComponent<Image>().color = Color.white;
                                if (outline != null)
                                {
                                    Destroy(outline);
                                    outline = null;
                                }
                                Mode = 0;
                            }
                            
                        }
                    }
                }
            break;
            //By default do nothing

            case 3:
                //Seleccionar para rotar
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit hit;
                    Ray ray = Maincamera.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Actor"))
                        {
                            Actor = hit.collider.gameObject;
                            outline = Actor.AddComponent<Outline>();
                            outline.OutlineMode = Outline.Mode.OutlineAll;
                            outline.OutlineColor = Color.yellow;
                            outline.OutlineWidth = 5f;
                            Mode = 4;
                        }
                    }
                }
            break;
            case 4:
                //Rotar el actor
                if (touch.phase == TouchPhase.Began)
                {
                    if (Actor != null)
                    {
                        RaycastHit hit;
                        Ray ray = Maincamera.ScreenPointToRay(Input.GetTouch(0).position);
                        Vector3 hitpoint;
                        if (Physics.Raycast(ray, out hit))
                        {
                            hitpoint = hit.point;
                            if (hit.collider.tag != "Actor")
                            {
                                Vector3 relativePos = hitpoint - Actor.transform.position;
                                Actor.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                RotateButton.GetComponent<Image>().color = Color.white;
                                if (outline != null)
                                {
                                    Destroy(outline);
                                    outline = null;
                                }
                                Mode = 0;
                            }

                        }
                    }
                }
                break;
                case 5:
                    if (touch.phase == TouchPhase.Began)
                    {
                        RaycastHit hit;
                        Ray ray = Maincamera.ScreenPointToRay(Input.GetTouch(0).position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.gameObject.CompareTag("Actor"))
                            {
                                Actor = hit.collider.gameObject;
                                Destroy(Actor);
                                Actor = null;
                                Mode = 0;

                            }
                        }
                    }
                break;

        }
    }

    void SetMoveMode()
    {
        if (Mode == 0)
        {
            Mode = 1;
            MoveButton.GetComponent<Image>().color = Color.yellow;
        }
        else if (Mode == 1 || Mode == 2)
        {
            Mode = 0;
            MoveButton.GetComponent<Image>().color = Color.white;
            Destroy(outline);
            outline = null;
            Actor = null;
        }
        
    }

    void SetRotateMode()
    {
        if (Mode == 0)
        {
            Mode = 3;
            RotateButton.GetComponent<Image>().color = Color.yellow;
        }
        else if (Mode == 3 || Mode == 4)
        {
            Mode = 0;
            RotateButton.GetComponent <Image>().color = Color.white;
            Destroy (outline);
            outline = null;
            Actor = null;
        }
    }

    void SetDeleteMode()
    {
        if (Mode == 0)
        {
            Mode = 5;
            DeleteButton.GetComponent<Image>().color = Color.red;
        }
        else if (Mode == 5)
        { 
            Mode = 0;
            DeleteButton.GetComponent <Image>().color = Color.white;
            Destroy(outline);
            outline = null;
            Actor = null;
        }
    }


}

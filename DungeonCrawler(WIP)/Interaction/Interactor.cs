using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactor : MonoBehaviour
{
    public Camera fpsCamera;

    public float rayDistance = 0.5f;

    public int level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        fpsCamera = GetComponentInChildren<Camera>();
        //level = GetComponent<AnimatedGridMovement>().gridLevel.debugLevel;
    }

    private void Update()
    {
        //Debug.Log($"My Position: {new Vector3(Mathf.RoundToInt(this.transform.position.x), 0, Mathf.RoundToInt(this.transform.position.z))}");
        Ray rayMouse = fpsCamera.ScreenPointToRay(Input.mousePosition);
        //Ray rayScreen = fpsCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit raycastHit;

        if(Physics.Raycast(rayMouse, out raycastHit, rayDistance))
        {
            Debug.DrawLine(transform.position, raycastHit.point, Color.red);

            if (raycastHit.collider.tag == "Door" && raycastHit.distance <= 2f)
            {
                Vector3 doorPosition = raycastHit.collider.transform.position;
                //Debug.Log($"Door in range, position Z: {Mathf.RoundToInt(doorPosition.z)}");

                if (Input.GetMouseButtonDown(0))
                {
                    //raycastHit.collider.transform.position = Vector3.up * 2;

                    //play door open animation
                    raycastHit.collider.GetComponentInParent<DoorScript>().AnimateOpeningDoor();

                    LevelLayouts.ChangeLayoutToPassable(level, Mathf.RoundToInt(doorPosition.x), Mathf.RoundToInt(doorPosition.z));
                }
            }

            if (raycastHit.collider.tag == "Item")
            {
                //Debug.Log("item in range");
                if (Input.GetMouseButtonDown(0))
                {
                    //pickup item
                    //add item to inventory
                    //Debug.Log(raycastHit.distance);
                }
            }
        }
        else
        {
            //Debug.DrawLine(transform.position, ray.origin + ray.direction * 100, Color.green);
            //Debug.Log("Out of range");
        }
    }
}

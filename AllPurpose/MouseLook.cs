    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivityX = 100f; //varijabla s kojom kontroliramo osjetljivost misa po X osi
    public float mouseSensitivityY = 100f; //varijabla s kojom kontroliramo osjetljivost misa po Y osi

    public Transform playerBody;

    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //zakljucavanje cursora da se ne moze micati dok smo u play mode   
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime; //prikupljanje informacija za pomicanje misa na X osi
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime; //prikupljanje informacija za pomicanje misa na y osi

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //ogranicavanje rotacije po Y osi da ne mozemo rotirati kameru 360 stupnjeva

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); //odredivanje rotacije
        playerBody.Rotate(Vector3.up * mouseX); //brzina rotacije

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

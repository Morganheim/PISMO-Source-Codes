using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y, 0);
        lastCameraPosition = cameraTransform.position;

        if (cameraTransform.position.x - transform.position.x >= textureUnitSizeX)
        {
            transform.position = new Vector3(cameraTransform.position.x, transform.position.y);
        }
    }







    //private float length;
    //private float startPosition;
    //public float parallax;
    //public GameObject cam;

    //private void Start()
    //{
    //    startPosition = transform.position.x;
    //    length = GetComponent<SpriteRenderer>().bounds.size.x;
    //}

    //private void FixedUpdate()
    //{
    //    float distance = (cam.transform.position.x * parallax);
    //    transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

    //    float temp = (cam.transform.position.x * (1 - parallax));

    //    if (temp > startPosition + length)
    //    {
    //        startPosition += length;
    //    }
    //    else if (temp < startPosition - length)
    //    {
    //        startPosition -= length;
    //    }
    //}
}

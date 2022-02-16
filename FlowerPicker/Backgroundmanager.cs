using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backgroundmanager : MonoBehaviour
{
    public GameObject background;
    public Sprite[] sprites;
    FlowerMaker fm;
    DestroyerMoveToMouse dmtm;

    float alphaStep;

    private void Start()
    {
        fm = FindObjectOfType<FlowerMaker>();
        dmtm = FindObjectOfType<DestroyerMoveToMouse>();
        alphaStep = 1f / fm.spsps.Length;
        background.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r,
                                                           background.GetComponent<Image>().color.g, 
                                                           background.GetComponent<Image>().color.b, 0);
    }

    private void Update()
    {
        background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, 
                                                           background.GetComponent<Image>().color.g,
                                                           background.GetComponent<Image>().color.b, 
                                                           alphaStep * (float)dmtm.destroyed);
    }


}

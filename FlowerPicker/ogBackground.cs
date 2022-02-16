using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ogBackground : MonoBehaviour
{
    public GameObject grassBackground;
    public Sprite grassSprite;
    FlowerMaker fm;
    DestroyerMoveToMouse dmtm;

    float alphaStep;

    private void Start()
    {
        fm = FindObjectOfType<FlowerMaker>();
        dmtm = FindObjectOfType<DestroyerMoveToMouse>();
        alphaStep = 1f / fm.spsps.Length;
        grassBackground.GetComponent<Image>().sprite = grassSprite;
        grassBackground.GetComponent<Image>().color = new Color(grassBackground.GetComponent<Image>().color.r,
                                                                   grassBackground.GetComponent<Image>().color.g,
                                                                   grassBackground.GetComponent<Image>().color.b, 1f);
    }

    public void Reduce()
    {
        grassBackground.GetComponent<Image>().color = new Color(grassBackground.GetComponent<Image>().color.r,
                                                                   grassBackground.GetComponent<Image>().color.g,
                                                                   grassBackground.GetComponent<Image>().color.b,
                                                                   grassBackground.GetComponent<Image>().color.a - alphaStep);
    }


}

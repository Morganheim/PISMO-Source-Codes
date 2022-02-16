using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpImageChanger : MonoBehaviour
{
    public GameObject[] powerUpImages;
    public GameObject powerUpText;

    public void ChangeUiImageToBean()
    {
        for (int i = 0; i < powerUpImages.Length; i++)
        {
            powerUpImages[i].SetActive(false);
        }

        powerUpImages[0].SetActive(true);
        powerUpText.SetActive(true);
    }

    public void ChangeUiImageToShroom()
    {
        for (int i = 0; i < powerUpImages.Length; i++)
        {
            powerUpImages[i].SetActive(false);
        }
        powerUpImages[2].SetActive(true);
        powerUpText.SetActive(true);
    }

    public void ChangeUiImageToBottle()
    {
        for (int i = 0; i < powerUpImages.Length; i++)
        {
            powerUpImages[i].SetActive(false);
        }
        powerUpImages[1].SetActive(true);
        powerUpText.SetActive(true);
    }

    public void ChangeUiImageToEmpty()
    {
        for (int i = 0; i < powerUpImages.Length; i++)
        {
            powerUpImages[i].SetActive(false);
        }
        powerUpImages[3].SetActive(true);
        powerUpText.SetActive(false);
    }
}

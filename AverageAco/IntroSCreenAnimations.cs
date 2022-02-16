using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSCreenAnimations : MonoBehaviour
{
    public Text click;
    public GameObject introScreen;
    public GameObject mainMenu;

    public GameObject rotator;

    private void Start()
    {
        InvokeRepeating("BlinkNSpin", 0f, 0.7f);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            introScreen.SetActive(false);
            mainMenu.SetActive(true);
        }

        rotator.transform.Rotate(Vector3.forward * 40f*Time.deltaTime);
    }
    void BlinkNSpin()
    {
        click.enabled = !click.enabled;
    }
}

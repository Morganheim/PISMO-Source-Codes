using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorOpened;

    public Animator anim;

    private void Start()
    {
        doorOpened = false;
        anim = GetComponent<Animator>();
    }

    public void AnimateOpeningDoor()
    {
        if (!doorOpened)
        {
            anim.SetBool("isOpen", true);
            doorOpened = false;
        }
    }
}

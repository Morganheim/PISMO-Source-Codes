using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSymbolChanger : MonoBehaviour
{
    public Sprite[] chernobog;

    private void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = chernobog[0];
    }

    public void ChangeSpellButtonSprite()
    {
        if (GetComponent<Image>().sprite == chernobog[0])
        {
            GetComponent<Image>().sprite = chernobog[1];
        }
        else
        {
            GetComponent<Image>().sprite = chernobog[0];
        }
    }
}

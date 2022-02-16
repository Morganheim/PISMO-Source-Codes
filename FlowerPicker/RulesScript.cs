using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesScript : MonoBehaviour
{
    public Image sampleImage;
    public Text rulesText;
    public Sprite[] samples;

    public void FirstRule()
    {
        sampleImage.sprite = samples[0];
        rulesText.text = "Help the flower pollinate by destroying its petals and spreading its pollen!";
    }

    public void SecondRule()
    {
        sampleImage.sprite = samples[1];
        rulesText.text = "Find and tap on the petal with the shape and color like the one in the middle of the flower!";
    }

    public void ThirdRule()
    {
        sampleImage.sprite = samples[2];
        rulesText.text = "But look out for the buzzzzzy bees! They're minding their own business but will sting you if you tap on them!";
    }
}

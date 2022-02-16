using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShape : MonoBehaviour
{
    public GameObject centerSprite;
    public GameObject[] sprites;
    public Color[] colors = new Color[4];
    FlowerMaker fm;

    public float timerReset = 5f;
    float timer;
    public float petNumber;

    public string currentSpriteTag;
    public Color currentSpriteColor;
    private void Start()
    {
        fm = FindObjectOfType<FlowerMaker>();
        colors[0] = new Color(255f / 255f, 190f / 255f, 11f / 255f);
        colors[1] = new Color(251f / 255f, 86f / 255f, 7f / 255f);
        colors[2] = new Color(58f / 255f, 134f / 255f, 255f / 255f);
        colors[3] = new Color(131f / 255f, 56f / 255f, 236f / 255f);
        timer = timerReset;
        ChangeSpriteAndColor();
        petNumber = fm.spsps.Length;
    }

    private void Update()
    {
        
    }

    public void ChangeSpriteAndColor()
    {
        int spriteIndex = Random.Range(0, fm.spsps.Length - 2);
        if(fm.spsps[spriteIndex].gameObject.activeSelf == false)
        {
            if (Random.Range(0, 1000) % 2 == 0)
            {
                for (int i = 0; i < fm.spsps.Length - 2; i++)
                {
                    if (fm.spsps[i].gameObject.activeSelf)
                    {
                        spriteIndex = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = fm.spsps.Length - 3; i >= 0; i--)
                {
                    if (fm.spsps[i].gameObject.activeSelf)
                    {
                        spriteIndex = i;
                        break;
                    }
                }
            }
        }

        currentSpriteTag = fm.spsps[spriteIndex].tag;
        Sprite tempSprite = fm.spsps[spriteIndex].sprite;
        centerSprite.GetComponent<SpriteRenderer>().sprite = tempSprite;
        currentSpriteColor = fm.spsps[spriteIndex].color;
        centerSprite.GetComponent<SpriteRenderer>().color = currentSpriteColor;
    }
}

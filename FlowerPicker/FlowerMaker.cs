using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMaker : MonoBehaviour
{
    public int startingFlowerAmount = 5;
    public float radiusStep = 2f;
    public float radius = 1f;
    public int maxItteration = 5;
    int itteration = 1;
    //public float angle;
    public GameObject[] spriteTypes;
    public GameObject middle;
    public Color[] colors = new Color[4];
    public BoxCollider2D[] created;
    public SpriteRenderer[] spsps;

    void MakeColors()
    {
        colors[0] = new Color(255, 190, 11);
        colors[1] = new Color(251, 86, 7);
        colors[2] = new Color(58, 134, 255);
        colors[3] = new Color(131, 56, 236);
    }

    private void Start()
    {
        for (itteration = 1; itteration <= maxItteration; itteration++)
        {
            InstantiateCircle();
            radius += radiusStep;
        }
        created = GameObject.FindObjectsOfType<BoxCollider2D>();
        for (int i = 0; i < created.Length; i++)
        {
            spsps[i] = created[i].GetComponent<SpriteRenderer>();
        }

    }

    private void Update()
    {
        //for (itteration = 0; itteration < maxItteration && doLoop; itteration++)
        //{
        //    InstantiateCircle();
        //    radius += radiusStep;
        //}
    }

    int num = 0;
    void InstantiateCircle()
    {
        int offsetAngle = 0;
        float angle = 360f / (float)(itteration  * startingFlowerAmount);
        if (itteration % 2 != 0)
        {
            offsetAngle = 1;
        }
        for (int i = 0; i < (itteration  * startingFlowerAmount); i++)
        {
            Quaternion rotation = Quaternion.AngleAxis((i + offsetAngle) * angle - 90, Vector3.forward);
            Vector3 direction = rotation * Vector3.forward;
            
            Vector3 position = middle.transform.position + (new Vector3(Mathf.Cos((i + offsetAngle) * angle * Mathf.Deg2Rad), Mathf.Sin((i + offsetAngle) * angle * Mathf.Deg2Rad), itteration + 1) * radius);
            GameObject newObject = Instantiate(spriteTypes[Random.Range(0,spriteTypes.Length)], position, rotation);
            newObject.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
            newObject.transform.SetParent(middle.transform, true);
            newObject.name = (num++).ToString();
        }
    }
}

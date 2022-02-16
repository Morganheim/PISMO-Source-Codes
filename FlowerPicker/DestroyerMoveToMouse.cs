using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerMoveToMouse : MonoBehaviour
{
    Vector3 pointerPos;
    SelectShape ss;
    FlowerMaker fm;
    RotateFlower rf;
    float speedInc;
    public bool canIClick = false;
    public GameObject particle;
    public int destroyed = 0;
    UIManager uim;
    MusicManager mm;
    PcelaMovement[] pm;

    ogBackground ogb;

    public void MouseTurnerOn()
    {
        canIClick = true;
    }

    private void Start()
    {
        uim = FindObjectOfType<UIManager>();
        ss = FindObjectOfType<SelectShape>();
        fm = FindObjectOfType<FlowerMaker>();
        rf = FindObjectOfType<RotateFlower>();
        pm = FindObjectsOfType<PcelaMovement>();
        mm = FindObjectOfType<MusicManager>();
        ogb = FindObjectOfType<ogBackground>();
        speedInc = 35f / fm.spsps.Length;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canIClick)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && !pm[0].move && !pm[1].move)
            {
                if(hit.collider.gameObject.tag == ss.currentSpriteTag && hit.collider.GetComponent<SpriteRenderer>().color == ss.currentSpriteColor)
                {
                    hit.collider.gameObject.SetActive(false);
                    rf.rotationSpeedInAngle += speedInc;
                    Instantiate(particle, new Vector3(mousePos.x, mousePos.y, 0.5f), Quaternion.identity);
                    //Destroy(hit.collider.gameObject);
                    destroyed++;
                    mm.FlowerSound();
                    ogb.Reduce();
                    if(destroyed == fm.spsps.Length - 2)
                    {
                        uim.isGameOver = true;
                        uim.ShowEndGame();
                    }
                    ss.ChangeSpriteAndColor();
                }
                if (hit.collider.gameObject.tag == "bee1")
                {
                    pm[1].OnClickMovement();
                    mm.BeeSound();
                }
                if (hit.collider.gameObject.tag == "bee")
                {
                    pm[0].OnClickMovement();
                    mm.BeeSound();
                }
            }

            if (transform.position != pointerPos)
            {
                transform.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            }
        }
    }
}

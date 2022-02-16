using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    public CoinManager cm;
    public LevelManager lm;
    public UIManager uim;
    public PowerUpImageChanger pimp;

    public AudioSource ass;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer; //za rotaciju lika

    public Sprite deadAco;

    public GameObject[] doubleCoins; //dupli novcici koji se ukljucuju/iskljucuju ovisno jesi li pio (isDrunk)
    public GameObject[] hiddenPlatforms; //ukljucuju/iskljucuju se ovisno jesi li na gljivama
    public GameObject[] babaLoot;

    public GameObject fireLeft;
    public GameObject fireRight;

    public GameObject follower; //na praznom gameobjektu je kamera koja prati igraèa

    public GameObject speachBubble;
    public Text speachText;

    public GameObject gameOverScreen;
    public GameObject levelWinScreen;
    public GameObject pauseMenu;

    [SerializeField]
    private AudioClip[] acoSounds;

    [SerializeField]
    private bool isInAir; //zasebno gibanje kada si u zraku
    [SerializeField]
    private bool fartingInAir;
    public bool isDrunk; //jesi li pokupio
    public bool isFarting; //jel prdis
    public bool isOnShrooms; //jesi li se nadrogira
    [SerializeField]
    private bool canFart; //jel mos prdit

    //za stari movement
    public float movementSpeed;
    public float inAirSpeed;
    public float jumpForce;
    public float maxSpeed;
    public float maxVelocity;

    //za novi movement
    public float jumpForce2;
    public float speed2;
    private float horizontalMove;


    private float timeInAir;

    public float waitTimer;
    //public float gravityIncrease;

    [SerializeField]
    private int bossHit;

    private Vector3 startPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ass = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ukoliko mu skocis na glavu
        if (collision.gameObject.tag == "enemyHead")
        {
            GameObject jebiMe = collision.gameObject.transform.parent.gameObject;
            jebiMe.SetActive(false);
            Debug.Log("misevaglava");
            StartCoroutine(SpeachBubble("SHE LIFTED ME \n FROM ASHES"));
            rb.AddForce(Vector2.up * 11f, ForceMode2D.Impulse);
        }

        //ukoliko babi skocis na glavu
        if (collision.gameObject.tag == "bossHead")
        {
            bossHit++;
            rb.AddForce(Vector2.one * 6.5f, ForceMode2D.Impulse); //bilo je vector2.up jedi govna
            if (bossHit >= 3)
            {
                for (int i = 0; i < babaLoot.Length; i++)
                {
                    babaLoot[i].SetActive(true);
                }

                GameObject jebiMe = collision.gameObject.transform.parent.gameObject;
                jebiMe.SetActive(false);
                //collision.gameObject.GetComponentInParent<BabaSkripta>().ChangeSprite();
                StartCoroutine(SpeachBubble("GOODNIGHT PLAYERS"));
                bossHit = 0;
            }
        }

        //kad pokupis coin
        if (collision.gameObject.tag == "coin")
        {
            cm.CoinCollect();
            ass.clip = acoSounds[2];
            ass.Play();
            collision.gameObject.SetActive(false);
        }

        //kada pokupis konzervu graha
        if (collision.gameObject.tag == "beanCans")
        {
            BeanPickiup();
            StartCoroutine(SpeachBubble("My LEFT asscheek is \n out of CONTROL!"));
            collision.gameObject.SetActive(false);
        }

        //kada pokupis gljive
        if (collision.gameObject.tag == "shroom")
        {
            ShroomPickup();
            StartCoroutine(SpeachBubble("2D OR NOT 2D?"));
            collision.gameObject.SetActive(false);
        }

        //kada pokupis cugu
        if (collision.gameObject.tag == "booze")
        {
            BottlePickup();
            StartCoroutine(SpeachBubble("YAGODA I <3 U"));
            collision.gameObject.SetActive(false);
        }

        //padnes s platforme
        if (collision.gameObject.tag == "edge")
        {
            StartCoroutine(SpeachBubble("X"));
            //rb.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb.AddForce(Vector2.up * 2000, ForceMode2D.Impulse);
            //collidder.enabled = false;
            AcoDiedYouFool();
        }

        //doðeš do cilja
        if (collision.gameObject.tag == "end")
        {
            if (SceneManager.GetActiveScene().buildIndex != 5)
            {
                StartCoroutine(SpeachBubble("Now to pay my dues..."));
                Debug.Log("treba dalje "); //DALJE NAPISAT

                uim.LevelEnd(cm.lvlCoins, cm.maxCoins[SceneManager.GetActiveScene().buildIndex - 1]);

                lm.LevelPassed();

                cm.AddCoinsToAllCoinsCounter();

                //uim.CheckIfAllLevelsAreCompleted();
            }
            else
            {
                uim.LevelEndTextUpdate(0,0);
            }

            levelWinScreen.SetActive(true);
        }

        //save pozicije na checkpointu
        if (collision.gameObject.tag == "checkpoint")
        {
            lm.SavePositionAtCheckpoint();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("kolizijha");

        //ako te dotakne neprijatelj
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "saw")
        {
            //ako imas powerup izgubis ga na prvi dodir
            if (isDrunk || canFart || isOnShrooms)
            {
                if (isDrunk)
                {
                    HideDoubleCoinsAndPlatforms();
                    isDrunk = false;
                }
                if (isOnShrooms)
                {
                    HideHiddenPlatforms();
                    isOnShrooms = false;
                }
                if (canFart)
                {
                    TurnoffFartFlame();
                    canFart = false;
                }

                pimp.ChangeUiImageToEmpty();
                StartCoroutine(SpeachBubble("I'M DYIING \n DYIING MOTHEER"));
                Debug.Log("You were drunk, now you not");
            }
            //ako nemas nijedan power up, umres
            else if (!isDrunk && !canFart && !isOnShrooms)
            {
                StartCoroutine(SpeachBubble("X"));
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                Debug.Log("You die bitch");
                AcoDiedYouFool();
            }
        }

        if (collision.gameObject.tag == "floor") //ovdje ne stavljati dasku
        {
            //rb.velocity = rb.velocity.normalized/2;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //ukoliko si na podu 
        if (collision.gameObject.tag == "floor")
        {
            animator.SetBool("isjumping", false);
            //animator.SetFloat("Speed", 0);
            isInAir = false;

        }

        if (collision.gameObject.tag == "daska"|| collision.gameObject.tag == "test")
        {
            animator.SetBool("isjumping", false);
            //animator.SetFloat("Speed", 0);
            isInAir = false;
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                BoxCollider2D coly = collision.gameObject.GetComponent<BoxCollider2D>();
                StartCoroutine(FallTimer(coly));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //u zraku 
        if (collision.gameObject.tag == "floor" || collision.gameObject.tag == "daska")
        {
            isInAir = true;

            if (!fartingInAir)
            {
                animator.SetBool("isFarting", false);
                animator.SetBool("isjumping", true);
            }
        }
    }

    void Update()
    {
        //jump
        if (!isInAir && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce2, ForceMode2D.Impulse);
        }

        //horizontalno kretanje
        horizontalMove = Input.GetAxis("Horizontal");

        //pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            uim.PauseGame();
        }

        //fart start
        if (Input.GetKeyDown(KeyCode.LeftControl) && canFart)
        {
            isFarting = true;
            ass.clip = acoSounds[3];
            ass.Play();
            if (isInAir)
            {
                animator.SetBool("isjumping", false);
                animator.SetBool("isFarting", true);
            }
            else
            {
                animator.SetBool("isFarting", true);
            }

            if (spriteRenderer.flipX == false)
            {
                fireLeft.gameObject.SetActive(true);
            }
            else if (spriteRenderer.flipX == true)
            {
                fireRight.gameObject.SetActive(true);
            }
        }

        //fart end
        if (Input.GetKeyUp(KeyCode.LeftControl) && canFart)
        {
            isFarting = false;
            ass.Stop();
            animator.SetBool("isFarting", false);
            fireLeft.gameObject.SetActive(false);
            fireRight.gameObject.SetActive(false);

            if (isInAir)
            {
                animator.SetBool("isjumping", true);
            }
        }

        //fart animation flipper (za slucaj da zelimo da kretanje i prdac rade istovremeno)
        //if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl) && isFarting || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl) && isFarting || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftControl) && isFarting || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftControl) && isFarting)
        //{
        //    if (spriteRenderer.flipX == false)
        //    {
        //        fireLeft.gameObject.SetActive(true);
        //    }
        //    else if (spriteRenderer.flipX == true)
        //    {
        //        fireRight.gameObject.SetActive(true);
        //    }
        //}
    }

    private void FixedUpdate()
    {
        //follower i kamera prate igraca - tu je prdec game object
        follower.transform.position = transform.position;

        rb.velocity = new Vector2(horizontalMove * speed2 * Time.deltaTime, rb.velocity.y);
        if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("Speed", 1);

            if (isFarting)
            {
                //za slucaj da ne zelimo da kretanje i prdac rade istovremeno
                TurnoffFartFlame();

                //za slucaj da zelimo kretanje i prdac istovremeno da rade
                //fireLeft.gameObject.SetActive(true);
            }
        }
        else if(horizontalMove<0)
        {
            spriteRenderer.flipX = true;
            animator.SetFloat("Speed", 1);

            if (isFarting)
            {
                //za slucaj da ne zelimo da kretanje i prdac rade istovremeno
                TurnoffFartFlame();

                //za slucaj da zelimo kretanje i prdac istovremeno da rade
                //fireRight.gameObject.SetActive(true);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void BottlePickup()
    {
        ass.clip = acoSounds[1];
        ass.Play();
        isDrunk = true;
        canFart = false;
        isOnShrooms = false;

        pimp.ChangeUiImageToBottle();

        ShowDoubleCoinsAndPlatforms();

        HideHiddenPlatforms();
    }

    public void ShroomPickup()
    {
        ass.clip = acoSounds[4];
        ass.Play();
        isDrunk = false;
        canFart = false;
        isOnShrooms = true;

        pimp.ChangeUiImageToShroom();

        ShowHiddenPlatforms();

        HideDoubleCoinsAndPlatforms();
    }

    public void BeanPickiup()
    {
        ass.clip = acoSounds[0];
        ass.Play();
        isDrunk = false;
        canFart = true;
        isOnShrooms = false;

        pimp.ChangeUiImageToBean();

        HideDoubleCoinsAndPlatforms();
        HideHiddenPlatforms();
    }

    private void TurnoffFartFlame()
    {
        isFarting = false;
        fireLeft.gameObject.SetActive(false);
        fireRight.gameObject.SetActive(false);
        animator.SetBool("isFarting", false);
        animator.SetFloat("Speed", 1);
    }

    //IEnumerator koji se poziva da propadnes kroz platformu
    IEnumerator FallTimer(BoxCollider2D coly)
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(waitTimer);
        GetComponent<EdgeCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        Debug.Log("korutinica");
    }

    //metoda koja pokazuje skrivene platforme
    public void ShowHiddenPlatforms()
    {
        for (int i = 0; i < hiddenPlatforms.Length; i++)
        {
            hiddenPlatforms[i].gameObject.SetActive(true);
        }
    }

    //metoda koja skriva platforme
    public void HideHiddenPlatforms()
    {
        for (int i = 0; i < hiddenPlatforms.Length; i++)
        {
            hiddenPlatforms[i].gameObject.SetActive(false);
        }
    }

    //metodcica koji pokazuje duple novcice i platforme
    public void ShowDoubleCoinsAndPlatforms()
    {
        for (int i = 0; i < doubleCoins.Length; i++)
        {
            doubleCoins[i].gameObject.SetActive(true);
        }
    }

    //metodica koji skriva duple novcice i platforme
    public void HideDoubleCoinsAndPlatforms()
    {
        for (int i = 0; i < doubleCoins.Length; i++)
        {
            doubleCoins[i].gameObject.SetActive(false);
        }
    }

    //IENumerator koji poziva oblacic s govorom
    IEnumerator SpeachBubble(string speach)
    {
        speachBubble.gameObject.SetActive(true);
        speachText.text = speach;
        yield return new WaitForSeconds(2);
        speachBubble.gameObject.SetActive(false);
    }

    public void AcoDiedYouFool()
    {
        uim.AcoDied();
        ass.clip = acoSounds[5];
        ass.Play();
        animator.enabled = false;
        spriteRenderer.sprite = deadAco;
        gameOverScreen.SetActive(true);
        transform.position = startPosition;
    }

    IEnumerator AcoDiedFoolmerator()
    {
        uim.AcoDied();
        ass.clip = acoSounds[5];
        ass.Play();
        animator.enabled = false;
        spriteRenderer.sprite = deadAco;
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
        transform.position = startPosition;
    }

    public void StartGame()
    {
        isDrunk = false;
        canFart = false;
        isOnShrooms = false;

        HideDoubleCoinsAndPlatforms();
        HideHiddenPlatforms();

        gameOverScreen.SetActive(false);
        levelWinScreen.SetActive(false);
        pauseMenu.SetActive(false);

        pimp.ChangeUiImageToEmpty();

        lm.LoadPlayerPositionAtCheckPoint();

        startPosition = transform.position;
    }
}

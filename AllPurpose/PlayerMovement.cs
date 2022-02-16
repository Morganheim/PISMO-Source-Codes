using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController controller; //dodajemo fps controllera

    public float speedNormal;
    public float speedDash;
    public float speed;
    public float dashCooldown;
    public float dashCooldownReset;

    Vector3 velocity; //odredivanje brzine padanja koja je konstantna

    private void Start()
    {
        speed = speedNormal;
        if (PlayerPrefs.GetFloat("DashCooldown") == 0)
        {
            dashCooldownReset = dashCooldown;
        }
        else
        {
            dashCooldownReset = PlayerPrefs.GetFloat("DashCooldown");
        }

        if (PlayerPrefs.GetFloat("MovementSpeed") == 0)
        {
            speedNormal = 12f;
            PlayerPrefs.SetFloat("MovementSpeed", speedNormal);
        }
        else
        {
            speedNormal = PlayerPrefs.GetFloat("MovementSpeed");
        }
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal"); //pozivanje osi po kojoj se krece lik
        float z = Input.GetAxis("Vertical"); //pozivanje osi po kojoj se krece lik

        Vector3 move = transform.right * x + transform.forward * z; //dodavanje kretanja liku

        dashCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            speed = speedDash;
            dashCooldown = dashCooldownReset;
        }

        controller.Move(move * speed * Time.deltaTime); //dodavanje komande u kojoj mnozimo smijer kretanja s brzinom i deltaTime, jer zelimo da kretnja bude jednako neovisno o framerateu

        controller.Move(velocity * Time.deltaTime); //brzina kretanja kontrolera
    }

    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, speedNormal, 0.1f);
    }
}

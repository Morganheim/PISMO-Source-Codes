using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private const float leftHand = -90f;
    private const float rightHand = +90f;

    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 3f;

    private Vector3 moveTowardsPosition;
    private Quaternion rotateFromDirection;
    private Quaternion rotateTowardsDirection;
  
    private float rotationTime = 0.0f;

    public TestingLevelConstructor gridLevel;
    public AudioSource footstepsAudioSource;

    private PlayerStats stats;

    void Start()
    {
        stats = GetComponentInChildren<PlayerStats>();

        int[,] level;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            level = LevelLayouts.level1;
            transform.position = LevelLayouts.level1StartPos;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level = LevelLayouts.level2;
            transform.position = LevelLayouts.level2StartPos;
            transform.Rotate(LevelLayouts.level2StartRot);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            level = LevelLayouts.level3;
            transform.position = LevelLayouts.level3StartPos;
            transform.Rotate(LevelLayouts.level3StartRot);
        }

        moveTowardsPosition = transform.position;
        rotateTowardsDirection = transform.rotation;
        rotateFromDirection = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (IsStationary())
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveForward();
                //footstep sound
                if (IsMoving())
                {
                    PlayFootstepAudio();
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveBackward();
                //footstep sound
                PlayFootstepAudio();
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                TurnLeft();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                TurnRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StrafeLeft();
                //footstep sound
                PlayFootstepAudio();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StrafeRight();
                //footstep sound
                PlayFootstepAudio();
            }
        }
    }

    void Update()
    {
        if (IsMoving())
        {
            var step = Time.deltaTime * gridSize * movementSpeed;
            AnimateMovement(step);
        }
        if (IsRotating())
        {
            AnimateRotation();
        }
    }

    private void AnimateRotation()
    {
        rotationTime += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(rotateFromDirection, rotateTowardsDirection, rotationTime * rotationSpeed);
        CompensateRotationRoundingErrors();
    }

    private void AnimateMovement(float step)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTowardsPosition, step);
    }

    private void CompensateRotationRoundingErrors()
    {
        if (transform.rotation == rotateTowardsDirection)
        {
            transform.rotation = rotateTowardsDirection;
        }
    }

    private void MoveForward()
    {
        CollisonCheckedMovement(CalculateForwardPosition());
    }

    private void MoveBackward()
    {
        CollisonCheckedMovement(-CalculateForwardPosition());
    }

    private void StrafeRight()
    {
        CollisonCheckedMovement(CalculateStrafePosition());
    }

    private void StrafeLeft()
    {
        CollisonCheckedMovement(-CalculateStrafePosition());
    }

    private void CollisonCheckedMovement(Vector3 movementDirection)
    {
        bool canMove = true;

        Vector3 targetPosition = moveTowardsPosition + movementDirection;

        int[,] level;
        float carryWeightCapacity = stats.carryWeightCapacity;
        float carryWeight = stats.carryWeight;
        
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level = LevelLayouts.level2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            level = LevelLayouts.level3;
        }
        else
        {
            level = LevelLayouts.level1;
        }

        if (level[-(int)targetPosition.z, (int)targetPosition.x] == 0 || level[-(int)targetPosition.z, (int)targetPosition.x] == 5 || level[-(int)targetPosition.z, (int)targetPosition.x] == 6 || carryWeight >= carryWeightCapacity)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (canMove)
        {
            moveTowardsPosition = targetPosition;
            //Debug.Log("Hodaj");
        }
        else
        {
            //Debug.Log("Ne mrdaj");
        }
    }

    private void TurnRight()
    {
        TurnEulerDegrees(rightHand);
    }

    private void TurnLeft()
    {
        TurnEulerDegrees(leftHand);
    }

    private void TurnEulerDegrees(in float eulerDirectionDelta)
    {
        rotateFromDirection = transform.rotation;
        rotationTime = 0.0f;
        rotateTowardsDirection *= Quaternion.Euler(0.0f, eulerDirectionDelta, 0.0f);
    }

    private bool IsStationary()
    {
        return !(IsMoving() || IsRotating());
    }

    private bool IsMoving()
    {
        return transform.position != moveTowardsPosition;
    }

    private bool IsRotating()
    {
        return transform.rotation != rotateTowardsDirection;
    }

    private Vector3 CalculateForwardPosition()
    {
        return transform.forward * gridSize;
    }

    private Vector3 CalculateStrafePosition()
    {
        return transform.right * gridSize;
    }



    private void PlayFootstepAudio()
    {
        footstepsAudioSource.Play();
    }
}

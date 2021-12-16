using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Speed")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpSpeed;

    [Header("Jump")]
    [SerializeField]
    private bool jumpPad;

    private float gravity = 9.81f;

    //Final Vector3 used for the Movement fonction
    Vector3 moveVector = Vector3.zero;

    [Header("Horizontal Boost")]
    [SerializeField] [Range(-1,1)]
    private int boostDirection;

    [Tooltip("Direction (on the axis) the bass push the player")]
    [SerializeField]
    private bool boostActif;

    [SerializeField]
    [Tooltip("Amount of reduced speed per second after a bass boost to smooth the effect")]
    private float boostSlow;

    [Tooltip("Amount of current force after a bass boost")]
    [SerializeField]
    private float currentBoostForce;

    [Tooltip("Amount of the original force for the bass boost")]
    [SerializeField]
    private float baseBoostForce;

    private enum MovementType {Hub, TransitionSlide, Slide, TransitionFly,  Fly }

    [Header("Movement Mode")]
    [Tooltip("Type of Movement")]
    [SerializeField]
    private MovementType currentMovementType;

    [Tooltip("Rotation Speed for transition")]
    [SerializeField]
    private float rotateSpeed;

    [Header("My Components")]
    [SerializeField]
    private CharacterController mycc;

    [SerializeField]
    private AudioSource music;

    private SpawnerManager spawnerManagerScript;

    [SerializeField]
    private Canvas pauseMenu;

    private bool gameStop;

    //private static PlayerMovement instance = null;

    /*void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }*/

    private void Start()
    {
        pauseMenu.enabled = false;
        spawnerManagerScript = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
    }

    private void Update()
    {

        ControlBoostSpeed();
        Movement();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameStop)
            {
                Time.timeScale = 1;
                pauseMenu.enabled = false;
                gameStop = false;
                music.UnPause();
            }

            else
            {
                pauseMenu.enabled = true;
                Time.timeScale = 0;
                gameStop = true;
                music.Pause();
            }
        }

        //If a boost occur
        if (boostActif)
        {
            currentBoostForce = baseBoostForce;
            boostActif = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            spawnerManagerScript.SetSpeedDoor(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetBoostDirection(-1);
            SetBoostActif(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetBoostDirection(1);
            SetBoostActif(true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spawnerManagerScript.SetBoost(true);
        }


    }

    //Player Movement
    private void Movement()
    {
        //Avoid Z movement due to collision
        if (transform.position.z > 0.5)
        {
            moveVector.z -= 3 * Time.deltaTime;
        }

        else if (transform.position.z < -0.5)
        {
            moveVector.z += 3 * Time.deltaTime;
        }
        else
        {
            moveVector.z = 0;
        }

        if (currentMovementType == MovementType.Slide) { 
            //Left/Right input * the speed + the current boost if there is one * the direction the boost push the player
            moveVector.x = Input.GetAxisRaw("Horizontal") * speed + currentBoostForce * boostDirection;

            //Just gravity things for jumps
            moveVector.y -= gravity * Time.deltaTime;

            //If the player touch a Jump Pad
            if (jumpPad)
            {
                moveVector.y = jumpSpeed;
                jumpPad = false;
            }

            //Movement fonction with the final Vector for the movement
            mycc.Move(moveVector * Time.deltaTime);
        }

        else if (currentMovementType ==  MovementType.TransitionFly)
        {
            moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
            moveVector.y = speed;

            transform.Rotate(new Vector3(1, 0, 0), rotateSpeed * Time.deltaTime);

            //Movement fonction with the final Vector for the movement
            mycc.Move(moveVector * Time.deltaTime);

            if (transform.rotation.x >= 0.70)
            {
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                currentMovementType = MovementType.Fly;
            }
        }

        else if (currentMovementType == MovementType.TransitionSlide)
        {
            moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
            moveVector.y -= gravity * Time.deltaTime;

            transform.Rotate(new Vector3(1, 0, 0), -rotateSpeed * Time.deltaTime);

            //Movement fonction with the final Vector for the movement
            mycc.Move(moveVector * Time.deltaTime);

            if (transform.rotation.x <= 0.0)
            {
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                currentMovementType = MovementType.Slide;
            }

        }

        else if(currentMovementType == MovementType.Fly)
        {

            //Left/Right input * the speed + the current boost force if there is one * the direction the boost push the player
            moveVector.x = Input.GetAxisRaw("Horizontal") * speed + currentBoostForce * boostDirection;
            moveVector.y = Input.GetAxisRaw("Vertical") * speed;


            //Movement fonction with the final Vector for the movement
            mycc.Move(moveVector * Time.deltaTime);
        }

        else if(currentMovementType == MovementType.Hub)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed;

        }
    }

    //Reduce the push effect of a boost over time
    private void ControlBoostSpeed()
    {
        
        if (currentBoostForce > 0)
        {
            currentBoostForce -= boostSlow * Time.deltaTime;
        }

        else
        {
            currentBoostForce = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Tremplin")
        {
            currentMovementType = MovementType.TransitionFly;
        }

        if (collision.gameObject.tag == "ReturnSlide")
        {
            currentMovementType = MovementType.TransitionSlide;
        }

        if (collision.gameObject.tag == "FlyDoor")
        {
            SceneManager.LoadScene("FlyScene");
        }

        if (collision.gameObject.tag == "SlideDoor")
        {
            SceneManager.LoadScene("SlideScene");
        }

           
    }

    public void SetBoostDirection(int value)
    {
        boostDirection = value;
    }

    public void SetBoostActif(bool value)
    {
        boostActif = value;
    }

    public void SetJumpPad(bool value)
    {
        jumpPad = value;
    }

    public string GetCurrentMovementType()
    {
        return currentMovementType.ToString();
    }

}

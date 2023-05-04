using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Set Up \\
    // Cameras
    public GameObject Camera1; // Player Camera 
    public GameObject Camera2; // Tower Camera
  
    // States
    public enum PlayerState
    {
        Walk = 0, // walking default control player
        Turret = 1 // controling turret on tower to shoot
    }
    public PlayerState PState; // PlayerState

    // Moving the Player \\
    // Directional Inputs
    private int Up = 0;
    private int Down = 0;
    private int Right = 0;
    private int Left = 0;

        // Direction Inputs Output Values
        private int Horizontal;
        private int Vertical;

        // Directional Output shown within Vectors
        Vector2 HV = Vector2.zero;
        Vector2 start = Vector2.zero;

    // Rigidbody to move using Physics
    Rigidbody2D rb = null;
    private Vector2 movement;

        // Player Speed 
        private float speed = 5; // speed to move between walk and sprint
        private float walk = 5; // walk speed
        private float sprint = 10; // sprint speed

    // Other Inputs \\

    // Interacting
    private bool interacting = false; // Input for Interaction

    // Shooting
    private bool Shooting = false; // Input for Shooting

    // Turret \\
    // Interaction
    public GameObject TurretSeat = null; // Object to interact with to operate Turret
    private float DistanceTurretSeat; // Distance to Seat to Interact

    // Turret Positions
    public GameObject Turret; // Turret Object that will be moved
    public GameObject TurretCP; // Turret Center Point to rotate around
    Vector2 TurretPosition = Vector2.zero; // Turret Position to go to

    // Turret Movement
    private float TurretSpeed;// Speed at which turret can move
    private float TSW = 0.6f;
    private float TSS = 1f;
    private float TurretAngleDeg = 90; // Angle of Turret in Degrees
    public float TurretAngle = 0; // Turret Angle in Radians
    public float TurretRadius = 2; // Radius of Turret circle it moves in

    // Turret Firing
    public GameObject Bullet; // Bullet To create when shooting

    // Turret Delay
    public float firerate = 1; // how long it takes before you can shoot again in secs
    private float firedelay = 0; // increases with time to ensure passage of time before next shot fired
    private bool firing = false; // is firing?

    public GameObject GM;
    
    // Start of Code \\

    // Called Once at Start
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("EnemySpawner");
        rb = GetComponent<Rigidbody2D>(); // Rigidbody used for movement 
        PState = PlayerState.Walk; // Player Default State - Walking
        TurretSpeed = TSW;
        Camera1.SetActive(true); // PLayer Camera is on
        Camera2.SetActive(false); // Tower Camera is off
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!GM.GetComponent<EnemySpawner>().GameOver)
        {
            // Inputs for Movement
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Up = 1;
                }
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    Up = 0;
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Down = 1;
                }
                else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    Down = 0;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Right = 1;
                }
                else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    Right = 0;
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Left = 1;
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    Left = 0;
                }
             // 0 if key not pressed 1 if pressed (WASD + Arrow Keys)

            // Outcome of inputs
            Horizontal = Right - Left;
            Vertical = Up - Down;

            // Outcome of inputs in a vector 2 format
            HV.x = Horizontal;
            HV.y = Vertical;

            // Direction
            Vector2 direction = start + (HV);
            movement = direction;

            // Inputs for Sprint
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    speed = sprint;
                    TurretSpeed = TSS;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                {
                    speed = walk;
                    TurretSpeed = TSW;
                }
            }

            // Input for Interaction
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    interacting = true;
                    Debug.Log("Interacting");
                }
                else if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    interacting = false;
                }
            }

            // Input for Shooting
            {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    Shooting = true;
                    Debug.Log("Shooting");
                }
                else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Keypad0))
                {
                    Shooting = false;
                }
            }
        }

        // Player State Walk
        if (PState == PlayerState.Walk)
        {
            // Gets the distance to the turret seat to determine whether it is close enough to switch to control turret;
            DistanceTurretSeat = Vector2.Distance(transform.position, TurretSeat.transform.position);

            if (DistanceTurretSeat < 2 && interacting == true)
            {
                interacting = false; // Prevents double interactions
                PState = PlayerState.Turret; // Switchs the control from Player to Turret
                Debug.Log("Switch to Turret Control");

                // Camera Switch Focus to Turret
                Camera1.SetActive(false); 
                Camera2.SetActive(true);
            }
        }

            // Player State Turret
            if (PState == PlayerState.Turret)
            {
                // This means player is in the turret so can interact to get out of the seat;
                if (interacting == true)
                {
                    interacting = false; // Prevents double interactions
                    PState = PlayerState.Walk; // Switchs the control from Turret to Player
                    Debug.Log("Switch to Player Control");

                    // Camera Switch Focus to Player
                    Camera1.SetActive(true);
                    Camera2.SetActive(false);
                }

                // Turret Controls Movement
                TurretAngleDeg -= TurretSpeed * Horizontal; // left increases the number while right decreases the number as horizontal is 1 or negative 1 and we are taking it away.
                TurretAngleDeg = Mathf.Clamp(TurretAngleDeg, 0, 180); // ensures that the turret stays within 0 and 180 degrees to ensure turret stays above the ground.
                TurretAngle = TurretAngleDeg * Mathf.Deg2Rad; // swaps the angle in degrees to radians

                // Turret Movement
                TurretPosition = Turret.transform.position; // gets start position
                TurretPosition.x = TurretCP.transform.position.x + (Mathf.Cos(TurretAngle) * TurretRadius); // Moves as required on X axis to stay within path of circle
                TurretPosition.y = TurretCP.transform.position.y + (Mathf.Sin(TurretAngle) * TurretRadius); // Moves as required on Y axis to stay within path of circle
                Turret.transform.position = TurretPosition; // Sets the Position of Turret

                // Shooting the Turret
                if (Shooting == true)
                {
                    if (firing == false) //
                                         // Bullet not just fired
                    {
                        Instantiate(Bullet, TurretPosition, transform.rotation); // Creates a Bullet
                        firing = true; // Bullet just fired
                        Debug.Log("Create Bullet");
                    }

                    if (firing == true) // Bullet just fired
                    {
                        firedelay += Time.deltaTime; // time increase 

                        if (firedelay >= firerate) // if enough time has passed
                        {
                            firedelay = 0; // timer reset
                            firing = false; // Bullet not just fired
                        }
                    }
                }
            }
        }

    }
    private void FixedUpdate()
    {
        if (PState == PlayerState.Walk) // Checks that the player can move
            MoveCharacter(movement); // Moves the Player
    }

    void MoveCharacter(Vector2 direction)
    {
        // Moves the player from the current location to the desired location over time 
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));  
    }
}

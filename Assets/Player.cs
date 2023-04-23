using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Set Up

    // Cameras

    public GameObject Camera1;
    public GameObject Camera2;
  
    //States
    public enum PlayerState
    {
        Walk = 0, // walking default control player
        Turret = 1 // controling turret on tower to shoot
    }
    public PlayerState PState; // PlayerState

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
        private float speed = 5;
        private float walk = 5;
        private float sprint = 10;

    // Interacting
    private bool interacting = false;

    // Turret
    public GameObject TurretSeat = null; // Object to interact with to operate Turret
    private float DistanceTurretSeat; // Distance to Seat to Interact

    public GameObject Turret; // Turret Object that will be moved
    public GameObject TurretCP; // Turret Center Point to rotate around

    public float TurretSpeed = 0.1f; // Speed at which turret can move
    private float TurretAngleDeg = 90; // Angle of Turret in Degrees
    public float TurretAngle = 0; // Turret Angle in Radians
    public float TurretRadius = 2; // Radius of Turret circle it moves in

    Vector2 TurretPosition = Vector2.zero; // Turret Position to go to

    // Shooting
    private bool Shooting = false; // Input for Shooting
    public GameObject Bullet; // Bullet To create when shooting

    public float firerate = 1; // how long it takes before you can shoot again in secs
    private float firedelay = 0; // increases with time to ensure passage of time before next shot fired
    private bool firing = false; // is firing?


    
    //Start of Code

    // Called Once at Start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PState = PlayerState.Walk;

        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
        }

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
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                speed = walk;
            }
        }

        // Input for Interaction
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interacting = true;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                interacting = false;
            }
        }

        // Input for Shooting
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Shooting = true;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                Shooting = false;
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
                    Instantiate(Bullet, TurretPosition, transform.rotation);
                    firing = true; // Bullet just fired
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
    private void FixedUpdate()
    {
        if (PState == PlayerState.Walk) // Checks that the player can move
            moveCharacter(movement); // Moves the Player
    }

    void moveCharacter(Vector2 direction)
    {
        // Moves the player from the current location to the desired location over time 
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));  
    }
}

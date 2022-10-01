using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //Assingables
    public Transform playerCam;
    public Transform orientation;

    //Other
    private Rigidbody rb;

    //Rotation and look
    public bool lockLook;
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    public float energy, energyRegen;
    public int health, regen;
    int maxHealth;
    bool fighting;
    public SimpleHealthBar energyBar;
    public SimpleHealthBar healthBar;

    //Movement
    public Vector3 inputVector;
    public float moveSpeed = 4500;
    public float baseSpeed = 20;
    private float startBaseSpeed;
    public bool grounded;
    public LayerMask whatIsGround;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;
    public float crouchGravityMultiplier;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;

    public int startDoubleJumps = 1;
    int doubleJumpsLeft;

    //Input
    public float x, y;
    bool jumping, sprinting, crouching;

    //Air contorl
    public float airForwardForce;

    //AirDash
    public float dashForce;
    public float dashCooldown;
    public float dashTime;
    bool allowDashForceCounter;
    bool readyToDash;
    int wTapTimes = 0;
    Vector3 dashStartVector;

    //RocketBoost
    public float maxRocketTime;
    public float rocketForce;
    bool rocketActive, readyToRocket;
    bool alreadyInvokedRockedStop;
    float rocketTimer;

    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    //flash
    public float flashCooldown,flashRange;
    public int maxFlashesLeft;
    bool alreadySubtractedFlash;
    public int flashesLeft = 3;

    //Wallrunning
    public LayerMask whatIsWall;
    RaycastHit wallHitR, wallHitL;
    public bool isWallRight, isWallLeft;
    public float maxWallrunTime;
    public float wallrunForce;
    public float maxWallSpeed;
    bool readyToWallrunR, readyToWallrunL;
    public bool isWallRunning;
    public float maxWallRunCameraTilt;
    public float wallRunCameraTilt = 0;

    //Climbing
    public float climbForce, maxClimbSpeed;
    public LayerMask whatIsLadder;
    bool alreadyStoppedAtLadder;

    //Speed Display
    public float speedRecord;
    public TextMeshProUGUI baseVelocityDisplay, currentVelocityDisplay;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startBaseSpeed = baseSpeed;
        maxHealth = health;
    }
    void Start()
    {
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        MyInput();
        if (!lockLook) Look();
        CheckForWall();

        EnergyManager();

        //Stuff
        if (Input.GetKeyDown(KeyCode.L)) SceneManager.LoadScene(1);

        //Trail renderer
        //if (rb.velocity.magnitude <= 25) GetComponent<TrailRenderer>().startWidth 

        //Set speed display
        if (currentVelocityDisplay != null)
            currentVelocityDisplay.SetText("currVelocity: " + Mathf.RoundToInt(rb.velocity.magnitude));
        if (baseVelocityDisplay != null)
            baseVelocityDisplay.SetText("baseVelocity: " + baseSpeed);

        //new speed record
        if (speedRecord < rb.velocity.magnitude)
            speedRecord = Mathf.RoundToInt(rb.velocity.magnitude);
    }

    private void EnergyManager()
    {
        if (isWallRunning && energy > 0)
            energy -= Time.deltaTime * 15;

        if (rocketActive && energy > 0)
            energy -= Time.deltaTime * 30;

        if (!isWallRunning && !rocketActive && energy < 100)
            energy += energyRegen * Time.deltaTime;

        energyBar.UpdateBar(energy, 100f);
    }
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftShift);

        //Calculating input vector
        //inputVector = new Vector3(x, 0, y);

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftShift))
            StopCrouch();

        //Double Jumping
        if (Input.GetButtonDown("Jump") && !grounded && doubleJumpsLeft >= 1)
        {
            Jump();
            doubleJumpsLeft--;
        }

        //Dashing
        if (Input.GetKeyDown(KeyCode.W) && wTapTimes<=1){
            wTapTimes++;
            Invoke("ResetTapTimes", 0.3f);
        }
        if (wTapTimes == 2 && readyToDash) Dash();

        //Wallrun
        if (Input.GetKey(KeyCode.D) && isWallRight && energy > 0) StartWallrun();
        if (Input.GetKey(KeyCode.A) && isWallLeft && energy > 0) StartWallrun();

        //Climbing
        if (Physics.Raycast(transform.position, orientation.forward, 1, whatIsLadder) && y > .9f)
            Climb();
        else alreadyStoppedAtLadder = false;
    }

    private void ResetTapTimes()
    {
        wTapTimes = 0;
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        //Extra gravity
        //Needed that the Ground Check works better!
        float gravityMultiplier = 10f;

        if (crouching) gravityMultiplier = crouchGravityMultiplier;

        rb.AddForce(Vector3.down * Time.deltaTime * gravityMultiplier);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        //Air Control
        //if (!grounded) rb.AddForce(orientation.forward * Time.deltaTime * airForwardForce);

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping && grounded && !rocketActive) Jump();

        //ResetStuff when touching ground
        if (grounded){
            readyToDash = true;
            readyToRocket = true;
            doubleJumpsLeft = startDoubleJumps;
        }

        //Set max speed
        float maxSpeed = this.baseSpeed;

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //If speed is larger than maxSpeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // Movement while sliding
        if (grounded && crouching) multiplierV = 0f;

        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if (grounded) { 
            readyToJump = false;

            //Add jump forces
            rb.AddForce(Vector3.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (!grounded){
            readyToJump = false;

            //Add jump forces
            rb.AddForce(orientation.forward * jumpForce * 1f);
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            //Reset Velocity
            rb.velocity = Vector3.zero;

            //Disable dashForceCounter if doublejumping while dashing
            allowDashForceCounter = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //Walljump
        if (isWallRunning){
            readyToJump = false;

            //normal jump
            if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.up * jumpForce * 1.5f);
                rb.AddForce(normalVector * jumpForce * 0.5f);
            }

            //sidwards wallhop
            if (isWallRight||isWallLeft && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) rb.AddForce(-orientation.up * jumpForce * 1f);
            if (isWallRight && Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * jumpForce * 3.2f);
            if (isWallLeft && Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * jumpForce * 3.2f);

            //Always add forward force
            rb.AddForce(orientation.forward * jumpForce * 1f);

            //Reset Velocity
            rb.velocity = Vector3.zero;

            //Disable dashForceCounter if doublejumping while dashing
            allowDashForceCounter = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Dash()
    {
        //saves current velocity
        dashStartVector = orientation.forward;

        allowDashForceCounter = true;

        readyToDash = false;
        wTapTimes = 0;

        //Deactivate gravity
        rb.useGravity = false;

        //Add force
        rb.velocity = Vector3.zero;
        rb.AddForce(orientation.forward * dashForce);

        Invoke("ActivateGravity", dashTime);
    }
    private void ActivateGravity()
    {
        rb.useGravity = true;

        //Counter currentForce
        if (allowDashForceCounter){
            rb.AddForce(dashStartVector * -dashForce * 0.5f);
        }
    }
    private void StartWallrun()
    {
        rb.useGravity = false;
        isWallRunning = true;
        allowDashForceCounter = false;

        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);

            //Make sure char sticks to wall
            if (isWallRight)
            rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            else
                rb.AddForce(-orientation.right * wallrunForce / 5* Time.deltaTime);
        }
    }
    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }
    private void CheckForWall()
    {
       isWallRight = Physics.Raycast(transform.position, orientation.right, out wallHitR, 1f, whatIsWall);
       isWallLeft = Physics.Raycast(transform.position, -orientation.right, out wallHitL, 1f, whatIsWall);

        if (!isWallLeft && !isWallRight) StopWallRun();
        if (isWallLeft || isWallRight) doubleJumpsLeft = startDoubleJumps;
    }
    private void Climb()
    {
        //Makes possible to climb even when falling down fast
        Vector3 vel = rb.velocity;
        if (rb.velocity.y < 0.5f && !alreadyStoppedAtLadder){
            rb.velocity = new Vector3(vel.x, 0, vel.z);
            //Make sure char get's at wall
            alreadyStoppedAtLadder = true;
            rb.AddForce(orientation.forward * 500 * Time.deltaTime);
        }

        //Push character up
        if (rb.velocity.magnitude < maxClimbSpeed)
        rb.AddForce(orientation.up * climbForce * Time.deltaTime);

        //Doesn't Push into the wall
        if (!Input.GetKey(KeyCode.S)) y = 0;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, wallRunCameraTilt);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

        //While Wallrunning
        //Tilts camera in .5 second
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallRight)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;

        //Tilts camera back again
        if (wallRunCameraTilt > 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
        if (wallRunCameraTilt < 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
    }
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        //if (!grounded || jumping) return; (Dani's settings)
        if (!grounded) return;

        //Slow down sliding
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement:
        ///Counters when no Input and still moving || Input in opposite direction then velocity
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > baseSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * baseSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        } 
    }

    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    /// Handle ground detection
    private void OnCollisionStay(Collision other)
    {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

    #region abilityFunctions

    public void DashInDirection(Vector3 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode.Impulse);
    }

    #endregion
}

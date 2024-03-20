using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CharacterController))]
public class ABC_MovementController : MonoBehaviour {


    // ********************* Settings ********************
    #region Settings

    [Header("Component Settings")]

    /// <summary>
    /// Determines if movement and rotation is allowed
    /// </summary>
    public bool allowMovement = true;

    /// <summary>
    /// Allows for a small delay when turning back on movement
    /// </summary>
    public float enableMovementDelay = 0.005f;

    /// <summary>
    /// Allows for a small delay when turning off movement
    /// </summary>
    public float disableMovementDelay = 0f;

    /// <summary>
    /// If true then the script will integrate with ABC receiving events when movement is prevented
    /// </summary>
    public bool ABCIntegration = true;


    /// <summary>
    /// If true then script will perform in FPS mode
    /// </summary>
    public bool FPSMode = false; 

    [Header("Movement")]

    /// <summary>
    /// Determines how fast the entity will move
    /// </summary>
    public float moveForce = 5f;

#if ABC_Unity_Input_System_Integration == false
    [HideInInspector]
#endif
    /// <summary>
    /// Axis name of move button
    /// </summary>
    public string moveButton = "Move"; 

    /// <summary>
    /// Determines how fast the entity will move when sprinting
    /// </summary>
    public float sprintForce = 7f;

    /// <summary>
    /// Axis name of sprint button
    /// </summary>
    public string sprintButton = "Sprint";

    [Header("LockOn Movement")]
    
    /// <summary>
    /// If enabled the script will integrate with ABC setting the same target as ABC
    /// </summary>
    public bool ABCLockOnIntegration = true;

    /// <summary>
    /// The lock on target - once locked on the entity will always face the target
    /// and move sideways/backwards/forwards. If null the entity will as normal
    /// turn to face the direction of the movement button pressed in relation
    /// to the way the camera is facing. 
    /// </summary>
    public GameObject lockOnTarget;

    /// <summary>
    /// The movement force when locking on to a target
    /// </summary>
    public float lockOnMoveForce = 4f;


    /// <summary>
    /// If true then lock on movement will be used 
    /// </summary>
    public bool useLockOnMovement = true;

    /// <summary>
    /// If configured the entity will temporarily not be locked on to the entity when the button is pressed
    /// </summary>
    public string tempLockOffButton = "Dodge";


    /// <summary>
    /// If configured the entity will temporarily not be locked on to the entity 
    /// </summary>
    public KeyCode tempLockOffKey = KeyCode.Mouse1;

    /// <summary>
    /// The amount of time to not be locked on to the entity before being locked back on
    /// </summary>
    public float tempLockOffDuration = 0.75f;

    [Header("Crosshair Movement")]

    /// <summary>
    /// Key to hold to activate crosshair movement
    /// </summary>
    public KeyCode crosshairMode = KeyCode.None;

    /// <summary>
    /// Determines if crosshair movement can be used
    /// </summary>
    public bool useCrosshairMovement = true;

    /// <summary>
    /// If configured the entity will temporarily not be in crosshair mode
    /// </summary>
    public KeyCode tempCrossOffButton = KeyCode.LeftShift;

    /// <summary>
    /// The amount of time to not be be in crosshair mode before it being turned back on
    /// </summary>
    public float tempCrossOffDuration = 0.75f;



    [Header("Jump and Gravity")]

    /// <summary>
    /// If enabled then the entity can jump
    /// </summary>
    public bool allowJumping = false;

    /// <summary>
    /// Axis name of jump button
    /// </summary>
    public string jumpButton = "Jump";

    /// <summary>
    /// Determines how far the entity will jump
    /// </summary>
    public float jumpForce = 12f;

    /// <summary>
    /// If enable then gravity will be applied to the entity
    /// </summary>
    public bool allowGravity = true;


    [Header("Rotation")]
    /// <summary>
    /// If enabled then the entity is allowed to rotate
    /// </summary>
    public bool allowRotation = true;

    /// <summary>
    /// If true then rotation will be enabled/disabled along with movement toggle
    /// </summary>
    public bool rotationToggleWithMovement = false;

    /// <summary>
    /// Determiens the speed of rotation, the lower the number the longer it takes
    /// </summary>
    public float rotationSpeed = 0.8f;
  
    /// <summary>
    /// Determines how much input is needed by the user before the rotation starts
    /// </summary>
    public float rotationDrag = 0f;

    /// <summary>
    /// Will determine how long after stopmovement was triggered that rotation can reoccur
    /// </summary>
    public float rotationStopMovementLeeway = 0.5f;


    [Header("Animation")]
    /// <summary>
    /// If enabled then motion is turned off allowing movement to come from animation and only rotation will occur
    /// </summary>
    public bool rootMotionMode = false;

    /// <summary>
    /// Name of the normal movement animation to play
    /// </summary>
    public string animationParameter = "Move";

    /// <summary>
    /// Name of the jump animation to play
    /// </summary>
    public string jumpAniParameter = "Jump";

    /// <summary>
    /// Name of the fall animation to play
    /// </summary>
    public string fallAniParameter = "Fall";

    /// <summary>
    /// Name of the side movement animation to play when in lockon movement mode
    /// </summary>
    public string lockOnSideAniParameter = "SideStep";

    /// <summary>
    /// Name of the front animation to play when in lockon movement mode
    /// </summary>
    public string lockOnForwardAniParameter = "ForwardStep";

    /// <summary>
    /// Name of the side movement animation to play when in crosshair movement mode
    /// </summary>
    public string crossHairSideAniParameter = "SideStep";

    /// <summary>
    /// Name of the front animation to play when in crosshair movement mode
    /// </summary>
    public string crossHairForwardAniParameter = "ForwardStep";


    #endregion


    // ********************* Private Properties ********************

    #region Private Properties

    /// <summary>
    /// Value which indicates how much vertical velocity is applied to the motion
    /// </summary>
    private float _verticalVelocity;

    /// <summary>
    /// Property which will work out the vertical velocity value to apply to the motion depending on if the user is grounded, jumping or falling
    /// </summary>
    private float VerticalVelocity {
        get {
            //If the entity is grounded
            if (this.IsGrounded()) {
                if (this.isJumping && this.allowJumping == true && this.allowMovement == true) {  // If jump key has been pressed to change vertical velocity to the jump force defined
                    _verticalVelocity = this.jumpForce;
                } else {
                    _verticalVelocity = 0; // else nothing is happening and the entity is grounded so no vertical velocity is applied
                }
            } else {

                //If gravity is disabled then don't apply a gravity force
                if (this.allowGravity == false)
                    _verticalVelocity = 0;
                else
                    _verticalVelocity += Physics.gravity.y * Time.deltaTime; // else If the entity is not grounded and allow gravity is enabled then apply the gravity value to the vertical velocity
            }

            return _verticalVelocity;
        }
    }

    /// <summary>
    /// Records the current input X amount (Horizontal Axis)
    /// </summary>
    float inputX;

    /// <summary>
    /// Records the current Z amount (Vertical Axis)
    /// </summary>
    float inputZ;

    /// <summary>
    /// Records when movement was last disabled
    /// </summary>
    float timeMovementLastDisabled = 0f; 

    #endregion



    // ********************* Variables ********************
    #region Variables

    /// <summary>
    ///Global variable which indicates how much input force was applied by the user
    /// </summary>
    private float inputForce;

    /// <summary>
    /// Variable which tracks the current speed
    /// </summary>
    private float speedForce;

    /// <summary>
    /// Tracks if the entity is jumping
    /// </summary>
    private bool isJumping = false;

    /// <summary>
    /// Records when a jump last happened 
    /// </summary>
    private float lastJumpTime = 0f;

    /// <summary>
    /// Records when entity reaches land after a jump 
    /// </summary>
    private float lastLandTime = 0f;

    /// <summary>
    /// Global variable which tracks which way the entity needs to move calculated from user input
    /// </summary>
    private Vector3 moveDirection;

    /// <summary>
    /// Main Camera
    /// </summary>
    private Camera Cam;

    /// <summary>
    /// Character Controller component attached to entity
    /// </summary>
    private CharacterController charController;

    /// <summary>
    /// Entities transform
    /// </summary>
    private Transform meTransform;

    /// <summary>
    /// Animator attached to entity
    /// </summary>
    private Animator Ani;

    /// <summary>
    /// Tracks if the script is currently locked temporarily (used for dodging)
    /// </summary>
    private bool tempLocked = false; 

    /// <summary>
    /// Records the ABC SM Component if used for events
    /// </summary>
    private ABC_StateManager ABCEventsSM;

    /// <summary>
    /// Records the ABC controller component for events
    /// </summary>
    private ABC_Controller ABCEventsCont;

    /// <summary>
    /// Records the ABC Entity
    /// </summary>
    private ABC_IEntity ABCEntity;

    #endregion


    // ********************* Private Methods ********************
    #region Private Methods

    /// <summary>
    /// Will check if the Button provided has been setup in the input manager. 
    /// </summary>
    /// <param name="InputName">Name of </param>
    /// <returns>True if input exists, else false.</returns>
    bool IsInputAvailable(string InputName)
    {
        try
        {

#if ABC_Unity_Input_System_Integration            
      return ABC_InputManager.IsButtonSupported(InputName);
#endif

            ABC_InputManager.GetXAxis(InputName);
            ABC_InputManager.GetYAxis(InputName);

            return true;
        }
        catch 
        {
            return false;
        }
    }

    /// <summary>
    /// Returns if the entity is grounded or not
    /// </summary>
    /// <returns>True if the entity is grounded, else false</returns>
    private bool IsGrounded() {        

        //If we on ground then return true
        if (ABCEntity.isInTheAir == false)
            return true;
        else
            return false; 

    }


    /// <summary>
    /// Will intergate with ABC by retriving the component from the current follow target and then subscribing to it's movement and gravity events
    /// </summary>
    private void IntegrateWithABC() {

        this.ABCEntity = ABC_Utilities.GetStaticABCEntity(this.gameObject);
        this.ABCEventsSM = meTransform.GetComponentInChildren<ABC_StateManager>();
        this.ABCEventsCont = meTransform.GetComponentInChildren<ABC_Controller>();

        //subscribe to the events
        if (this.ABCEventsSM != null && this.ABCIntegration == true) {
            this.ABCEventsSM.onEnableMovement += this.EnableMovement;
            this.ABCEventsSM.onDisableMovement += this.DisableMovement;

            this.ABCEventsSM.onEnableGravity += this.EnableGravity;
            this.ABCEventsSM.onDisableGravity += this.DisableGravity;
        }


        if (this.ABCEventsCont != null && this.ABCLockOnIntegration){
            this.ABCEventsCont.onTargetSet += this.LockOnTarget;
        }

    }

    /// <summary>
    /// Will wait for a duration before enabling or disabling movement
    /// </summary>
    /// <param name="AllowMovement">True if movement is enabled, else false</param>
    /// <param name="Duration">Duration to wait before movement is enabled or disabled</param>
    private IEnumerator ToggleMovement(bool AllowMovement, float Duration = 0f) {

        //if already enabled or disabled then return
        if (this.allowMovement == AllowMovement)
            yield break;


        if (Duration > 0f)
        yield return new WaitForSeconds(Duration);

        this.allowMovement = AllowMovement;

        //If set to toggle rotation also
        if (this.rotationToggleWithMovement == true)
            this.allowRotation = AllowMovement;



        //If movement was disabled record when this occured
        if (AllowMovement == false)
            this.timeMovementLastDisabled = Time.time; 


    }




    /// <summary>
    /// Will enable/disable gravity
    /// </summary>
    /// <param name="AllowGravity">True if gravity is enabled, else false</param>
    private IEnumerator ToggleGravity(bool AllowGravity)
    {

        //if already enabled or disabled then return
        if (this.allowGravity == AllowGravity)
            yield break;


        //Disable root motion as we are not using gravity (animations can have weights)
        if (AllowGravity == false)
            this.Ani.applyRootMotion = false;
        else
            this.Ani.applyRootMotion = true;


        //toggle gravity
        this.allowGravity = AllowGravity;

    }


    /// <summary>
    /// Will rotate the entity by the direction and speed provided, as long as the input force is greater then the rotation drag setup
    /// </summary>
    /// <param name="Direction">Vector3 to rotate the entity in</param>
    /// <param name="RotationSpeed">Float indicating the speed of rotation</param>
    /// <param name="InputForce">The input force applied by the user, needs to be greater then the rotation drag for the rotation to happen</param>
    private void Rotate(Vector3 Direction, float RotationSpeed, float InputForce) {

        //If we are not allowed to rotate or the input force is not greater then the setup drag then return here
        if (InputForce <= this.rotationDrag || this.allowRotation == false)
            return;

        // else rotate 
        meTransform.rotation = Quaternion.Slerp(meTransform.rotation, Quaternion.LookRotation(Direction), RotationSpeed);
    }

    /// <summary>
    /// Will move the entity using the character controller component. 
    /// </summary>
    /// <param name="Motion">Vector3 of the movement to apply</param>
    private void Move(Vector3 Motion) {

        //If root motion mode is on then reset any X and Y coords as this is done by the animation
        if (this.rootMotionMode == true) {
            Motion.x = 0f;
            Motion.z = 0f;
        }


        if (this.charController != null && this.charController.enabled == true) 
            charController.Move(Motion * Time.deltaTime);
        

    }

    /// <summary>
    /// Determines the move direction depending on the axis input made by the user and the direction of the camera
    /// </summary>
    private void DetermineMoveDirection() {

        //Work out the current speed force
        this.speedForce = this.moveForce;

        //If jumping that add some more movement to the push
        if (this.isJumping)
            this.speedForce += this.moveForce;

        if (this.sprintButton != string.Empty && this.IsInputAvailable(this.sprintButton) && ABC_InputManager.GetButton(this.sprintButton))
            this.speedForce = this.sprintForce;
        else if (ABC_InputManager.GetKey(KeyCode.LeftShift))
            this.speedForce = this.sprintForce;


        //Get input from user
        this.inputX = ABC_InputManager.GetXAxis("Horizontal") * this.speedForce;
        this.inputZ = ABC_InputManager.GetYAxis("Vertical") * this.speedForce;


#if ABC_Unity_Input_System_Integration
        if (this.moveButton != string.Empty && this.IsInputAvailable(this.moveButton)) {
            this.inputX = ABC_InputManager.GetXAxis(this.moveButton) * this.speedForce;
            this.inputZ = ABC_InputManager.GetYAxis(this.moveButton) * this.speedForce;
        }
#endif

            //Retrieve input force to be used by animations and rotation checks later
            if (this.inputX != 0)
            this.inputForce = new Vector2(inputX, 0).sqrMagnitude;
        else if (this.inputZ != 0)
            this.inputForce = new Vector2(0, inputZ).sqrMagnitude;
        else
            this.inputForce = new Vector2(inputX, inputZ).sqrMagnitude;

        //If both axis are being used then half the value to avoid the extra speed from moving in a diagonal direction
        if (this.inputX != 0 && this.inputZ != 0) {
            this.inputX = this.inputX / 2;
            this.inputZ = this.inputZ / 2;
        }


        //record the direction of the camera
        Vector3 camRight = Cam.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 camForward = Cam.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        //Work out move direction using the camera direction and the input from user
        this.moveDirection = camRight * inputX + camForward * inputZ;

    }


    /// <summary>
    /// Main method to rotate and move the entity
    /// </summary>
    private void RotateAndMoveEntity() {

        //Call lock off handler 
        StartCoroutine(this.TempLockOffHandler());

        // If crosshair movement key is held down prioritise the rotate to camera direction
        // else rotate the entity to face the direction we want to travel if we have not got a lock on target
        // else turn the entity to face lock on target


        if (this.FPSMode == true || this.crosshairMode != KeyCode.None && ABC_InputManager.GetKey(crosshairMode) == true)
            this.meTransform.rotation = Quaternion.Euler(this.meTransform.eulerAngles.x, Camera.main.transform.eulerAngles.y, this.meTransform.eulerAngles.z);
        else if (this.lockOnTarget == null && Time.time - timeMovementLastDisabled > this.rotationStopMovementLeeway)
            this.Rotate(this.moveDirection, this.rotationSpeed, this.inputForce);
        else if (this.lockOnTarget != null)
            this.meTransform.LookAt(lockOnTarget.transform.position);


        //If movement is not allowed then end here
        if (allowMovement == false)
            return;


        this.moveDirection.y = this.VerticalVelocity;
        this.Move(this.moveDirection);



    }

    /// <summary>
    /// Method to manage jumping
    /// </summary>
    private void JumpHandler() {

        //If we have landed on top of an entity then move us back a small amount
        if (this.IsOnTopOfEntity() == true && this.charController != null) 
            charController.Move(-meTransform.forward * 0.2f);        

        //If entity reaches the ground then stop jumping 
        if (this.IsGrounded() && this.isJumping &&  Time.time - this.lastJumpTime > 0.5f) {            
            this.Ani.SetBool(this.jumpAniParameter, false);
            this.isJumping = false;
            this.lastLandTime = Time.time;
           StartCoroutine(ABCEntity.ToggleIK(Time.time, true, 0.5f));
        }   

        //If not allowed to jump then end here (includes a small breather from when the entity lands)
        if (this.allowJumping == false || this.IsGrounded() == false ||  Time.time - this.lastLandTime < 0.3f || this.allowMovement == false)
            return;


        if (this.jumpButton != string.Empty && this.IsInputAvailable(this.jumpButton) && ABC_InputManager.GetButton(this.jumpButton) && this.isJumping == false) {
            this.lastJumpTime = Time.time;
            this.isJumping = true;
            StartCoroutine(ABCEntity.ToggleIK(Time.time, false));


            //Start jumping 
            this.Ani.SetBool(this.jumpAniParameter, true);
            this.Ani.SetTrigger(this.jumpAniParameter+"Trigger");
        }

                              
    }


    /// <summary>
    /// Will animate the entity using settings and the input force
    /// </summary>
    private void Animate() {

        //If movement is not allowed then end here
        if (allowMovement == false)
            return;

        //If no animator has been setup or jumping is happening (Animations done it it's own method) then return here
        if (this.Ani == null || this.tempLocked == true)
            return;

        //If we are falling then start animation
        if (this.IsGrounded() == false && this.isJumping == false && ABC_Utilities.EntityDistanceFromGround(meTransform) > 2f) {

            //Start falling 
            this.Ani.SetBool(this.jumpAniParameter, true);
            this.Ani.SetTrigger(this.fallAniParameter + "Trigger");

            this.isJumping = true;
        }

        //If jumping then make sure no other animations playing
        if (this.isJumping || Time.time - this.lastLandTime < 0.2f) {

            //Reset normal animation            
            Ani.SetFloat(this.animationParameter, 0, 0.0f, Time.deltaTime * 2f);

            //Reset Lock On Movement
            if (this.useLockOnMovement) {
                Ani.SetFloat(this.lockOnSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.lockOnForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            }

            //Reset Crosshair Movement
            if (this.useCrosshairMovement) {
                Ani.SetFloat(this.crossHairSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.crossHairForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            }

            //End here as jumping
            return; 
        }


        if (this.FPSMode == true || this.crosshairMode != KeyCode.None && ABC_InputManager.GetKey(crosshairMode) == true && this.useCrosshairMovement == true) {

            //Reset normal animation            
            Ani.SetFloat(this.animationParameter, 0, 0.0f, Time.deltaTime * 2f);

            //Reset Lock On Movement
            if (this.useLockOnMovement) {
                Ani.SetFloat(this.lockOnSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.lockOnForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            }

            //Animate crosshair movement
            // if forward (Z axis) is being pressed then stop side animations and use forward animation
            if (this.inputZ != 0) {
                Ani.SetFloat(this.crossHairSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.crossHairForwardAniParameter, this.inputForce * this.inputZ, 0.0f, Time.deltaTime * 2f);
            } else { // else side (X axis) is being pressed so stop forward animation and use sideways animation
                Ani.SetFloat(this.crossHairForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.crossHairSideAniParameter, this.inputForce * this.inputX, 0.0f, Time.deltaTime * 2f);
            }

            //End here as in crosshair mode
            return; 

        }



        if (lockOnTarget != null && this.useLockOnMovement) {

            //Reset normal animation            
            Ani.SetFloat(this.animationParameter, 0, 0.0f, Time.deltaTime * 2f);

            //Reset Crosshair Movement
            if (this.useCrosshairMovement) {
                Ani.SetFloat(this.crossHairSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.crossHairForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            }

            //Animate lock on movement
            // if forward (Z axis) is being pressed then stop side animations and use forward animation
            if (this.inputZ != 0) {
                Ani.SetFloat(this.lockOnSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.lockOnForwardAniParameter, this.inputForce * this.inputZ, 0.0f, Time.deltaTime * 2f);
            } else { // else side (X axis) is being pressed so stop forward animation and use sideways animation
                Ani.SetFloat(this.lockOnForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
                Ani.SetFloat(this.lockOnSideAniParameter, this.inputForce * this.inputX, 0.0f, Time.deltaTime * 2f);
            }


            //End here as in lock on mode
            return;

        }


        //If this far just animate normally: 

        //Reset Lock On Movement
        if (this.useLockOnMovement) {
            Ani.SetFloat(this.lockOnSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            Ani.SetFloat(this.lockOnForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
        }

        //Reset Crosshair Movement
        if (this.useCrosshairMovement) {
            Ani.SetFloat(this.crossHairSideAniParameter, 0, 0.0f, Time.deltaTime * 2f);
            Ani.SetFloat(this.crossHairForwardAniParameter, 0, 0.0f, Time.deltaTime * 2f);
        }

        //animate normally if not in air
        if(ABC_Utilities.EntityDistanceFromGround(this.meTransform) < 2)
            Ani.SetFloat(this.animationParameter, this.inputForce, 0.0f, Time.deltaTime * 2f);


    }

    /// <summary>
    /// Will determine if this entity is standing on another entity (something with a bone structure)
    /// </summary>
    /// <returns>True if standing on an entity, else false</returns>
    private bool IsOnTopOfEntity() {

        //Get the object below us
        GameObject objectBelow = ABC_Utilities.GetObjectBelowEntity(ABC_Utilities.GetStaticABCEntity(meTransform.gameObject));

        //If the object below us has a head then return true
        if (objectBelow != null && objectBelow.transform.GetComponent<Animator>() != null && objectBelow.transform.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head) != null) 
            return true;
         else // else if nothing below or no head return false
            return false; 
               
    }

 
    /// <summary>
    /// Will handle the temporarily lock off so if a configured button is pressed
    /// the entity will for a duration not be locked on to the current target 
    /// after the duration the entity will lock on to the target again
    /// </summary>
    private IEnumerator TempLockOffHandler(){

        // if temp lock off button is pressed then temporarily unlock on the target 
        if (this.lockOnTarget != null && (this.tempLockOffButton != string.Empty && this.IsInputAvailable(this.tempLockOffButton) && ABC_InputManager.GetButton(this.tempLockOffButton) || ABC_InputManager.GetKey(this.tempLockOffKey)) ){

            //Let script not temporarily lock is on
            this.tempLocked = true; 

            //store current target 
            GameObject currentLockOnTarget = this.lockOnTarget;

            //turn off lock on
            this.lockOnTarget = null;

            //wait the duration
            yield return new WaitForSeconds(this.tempLockOffDuration);

            //If a new target hasn't been selected then lock back on
            if (this.lockOnTarget == null)
                this.lockOnTarget = currentLockOnTarget;

            //Turn temp lock off
            this.tempLocked = false;

        }


        // if crosshair is on and temp off button is pressed then temporarily stop crosshair mode
        if (this.crosshairMode != KeyCode.None && ABC_InputManager.GetKey(crosshairMode) == true && ABC_InputManager.GetKey(this.tempCrossOffButton)) {

            //Let script not temporarily lock is on
            this.tempLocked = true;

            //store current crosshair key 
            KeyCode currentCrosshairKey = this.crosshairMode;

            //turn crosshair mode key to none to turn it off
            this.crosshairMode = KeyCode.None;

            //wait the duration
            yield return new WaitForSeconds(this.tempCrossOffDuration);

            //turn back on
            this.crosshairMode = currentCrosshairKey;

            //Turn temp lock off
            this.tempLocked = false;
        }

    }   


    #endregion

    // ********************* Public Methods ********************
    #region Public Methods

    /// <summary>
    /// Will enable movement and rotation
    /// </summary>
    public void EnableMovement() {


        //Stop any current toggle movement calls incase one is mid delay
        StopCoroutine("ToggleMovement");

        //Enable movement after delay
        StartCoroutine(this.ToggleMovement(true, this.enableMovementDelay));

    }


    /// <summary>
    /// Will stop movement and rotation
    /// </summary>
    public void DisableMovement() {

        //Stop any current toggle movement calls incase one is mid delay
        StopCoroutine("ToggleMovement");

        //Disable movement without delay
        StartCoroutine(this.ToggleMovement(false, this.disableMovementDelay));
    }


    /// <summary>
    /// Will enable Gravity
    /// </summary>
    public void EnableGravity(){

        //Stop any current toggle movement calls incase one is mid delay
        StopCoroutine("ToggleGravity");

        //Enable movement after delay
        StartCoroutine(this.ToggleGravity(true));

    }


    /// <summary>
    /// Will disable gravity
    /// </summary>
    public void DisableGravity(){

        //Stop any current toggle movement calls incase one is mid delay
        StopCoroutine("ToggleGravity");

        //Enable movement after delay
        StartCoroutine(this.ToggleGravity(false));
    }




    /// <summary>
    /// Will lock on to the target gameobject
    /// </summary>
    /// <param name="Target">Object to lock on too</param>
    public void LockOnTarget(GameObject Target){

        if (Target != this.gameObject)
        this.lockOnTarget = Target;
    }

    #endregion


    // ********************** Game ******************

    #region Game

    void Start() {


        //Delcare animator
        Ani = this.GetComponentInChildren<Animator>();

        //Reset time recorder
        this.timeMovementLastDisabled = 0f;

   
        //Retrieve main camera
        Cam = Camera.main;

        //Determine character controller
        charController = this.GetComponentInChildren<CharacterController>();

        //If another collider added turn off collisions on character controller (checks if higher then 1 as character controller counts as a collider)
        if (meTransform.GetComponentsInChildren<Collider>().Where(col => col.enabled == true).Count() > 1) {
            this.charController.detectCollisions = false;
        } else {
            this.charController.detectCollisions = true;
        }


    }



    private void OnEnable() {
        
        //Record entity transform
        this.meTransform = this.transform;

        //reset jumping
        this.isJumping = false;
        this.lastJumpTime = 0;

        this.IntegrateWithABC();




    }


    void Update() {


        //Execute the jump handler
        this.JumpHandler();

        //Determine input and direction
        this.DetermineMoveDirection();

        //Rotate and move entity
        this.RotateAndMoveEntity();

        //Run animations
        this.Animate();


    }



    private void OnDisable() {

        if (this.ABCEventsSM != null) {
            this.ABCEventsSM.onEnableMovement -= this.EnableMovement;
            this.ABCEventsSM.onDisableMovement -= this.DisableMovement;

            this.ABCEventsSM.onEnableGravity -= this.EnableGravity;
            this.ABCEventsSM.onDisableGravity -= this.DisableGravity;
        }

        if (this.ABCEventsCont != null){
            this.ABCEventsCont.onTargetSet -= this.LockOnTarget;
        }
    }


    #endregion
}

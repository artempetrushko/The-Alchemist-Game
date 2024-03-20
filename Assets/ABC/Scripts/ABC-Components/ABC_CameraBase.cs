using UnityEngine;

public class ABC_CameraBase : MonoBehaviour {

    // ********************* Settings ********************
    #region Settings


    [Header("Camera Settings")]   

    /// <summary>
    /// The speed in which the camera will follow the entity
    /// </summary>
    public float cameraFollowSpeed = 120f;

    /// <summary>
    /// The entity which the camera will follow
    /// </summary>
    public GameObject followTarget;

    /// <summary>
    /// The offset of the follow target where the camera will be placed
    /// </summary>
    public Vector3 followTargetOffset;

    /// <summary>
    /// The input sensitivity, higher the number the faster the camera rotation
    /// </summary>
    public float inputSensitivity = 5f;

    /// <summary>
    /// The angle restriction, the higher the number the more the camera will rotate up and down 
    /// </summary>
    public float clampAngle = 15f;

    /// <summary>
    /// If true then the Y Axis will be reverted (can swap between up rotating up and up rotating down etc)
    /// </summary>
    public bool invertYAxis = true;


    /// <summary>
    /// If true then the camera will follow the 'followtarget' heads and eye transform instead of the object
    /// </summary>
    public bool FPSMode = false;


    [Header("Zoom In Settings")]


    /// <summary>
    /// The offset of the follow target where the camera will be placed
    /// </summary>
    public Vector3 zoomTargetOffset;

    /// <summary>
    /// The key to start zooming in 
    /// </summary>
    public KeyCode zoomKey = KeyCode.None;

    /// <summary>
    /// The speed at which the entity zooms in and out
    /// </summary>
    public float zoomInSpeed = 13f;


    /// <summary>
    /// The angle restriction when zooming, the higher the number the more the camera will rotate up and down 
    /// </summary>
    public float zoomClampAngle = 30f;



    [Header("Lock On Settings")]

    /// <summary>
    /// If assigned then the camera will always look at this object
    /// </summary>
    public GameObject lockOnTarget;

    /// <summary>
    /// The offset of the lock on target, which changes the position where the camera looks at 
    /// </summary>
    public Vector3 lockOnTargetOffset;


    [Header("Misc Settings")]


    /// <summary>
    /// If true then the script will integrate with ABC by using the ABC target as the lock on target
    /// </summary>
    public bool enableABCIntegration;

    /// <summary>
    /// Determines if the mouse is locked and hidden
    /// </summary>
    public bool hideLockMouse = true;

    /// <summary>
    /// Will enable or disable rotation on the camera
    /// </summary>
    public bool enableRotation = true;

#if ABC_Unity_Input_System_Integration == false
    [HideInInspector]
#endif
    /// <summary>
    /// Used for new input system to define a input "Button" to capture rotation axis
    /// </summary>
    public string rotationButton = "Look";




    /// <summary>
    /// If true then rotation will only happen if mouse clicks are held down (mmo style)
    /// </summary>
    public bool rotationOnMouseHold = false;



#endregion



    // ********************* Variables ********************
#region Variables


    /// <summary>
    /// Main Camera
    /// </summary>
    private Camera Cam;

    /// <summary>
    /// Main Camera
    /// </summary>
    private Transform meTransform;

    /// <summary>
    /// Spine of the follow target
    /// </summary>
    private Transform meSpine = null;

    /// <summary>
    /// Head of the follow target
    /// </summary>
    private Transform meHead = null; 

    /// <summary>
    /// Follow target object
    /// </summary>
    private GameObject FollowTarget {

        get{
            if (this.FPSMode == false || this.meHead == null)
                return followTarget;
            else
                return this.meHead.gameObject;
        }

        set {
            this.followTarget = value; 
        }
    }

    /// <summary>
    /// Tracks the current Y rotation so this can be modified via input
    /// </summary>
    private float currentYRot = 0;

    /// <summary>
    /// Will enable or disable Y rotation on the camera
    /// </summary>
    private bool enableYRotation = true; 

    /// <summary>
    /// Tracks the current X rotation so this can be modified via input
    /// </summary>
    private float currentXRot = 0;

    /// <summary>
    /// Records when zoom in mode started 
    /// </summary>
    private float zoomStartTime = 0f;

    /// <summary>
    /// Records the ABC Component if used
    /// </summary>
    private ABC_Controller followTargetABC;


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
    /// Will intergate with ABC by retriving the component from the current follow target and then subscribing to it's on target set event
    /// </summary>
    private void IntegrateWithABC() {

        if (this.enableABCIntegration == false || this.FollowTarget == null)
            return; 


        this.followTargetABC = FollowTarget.GetComponentInChildren<ABC_Controller>();

        //subscribe to the event 
        if (this.followTargetABC != null) {
            this.followTargetABC.onTargetSet += this.SetLockOnTarget;
            this.followTargetABC.onAbilityBeforeTarget += this.ABCAbilityBeforeTargetToggleHandler;


        }

        


    }
    

    /// <summary>
    /// Will rotate the camera depending on user input
    /// </summary>
    private void RotateCamera() {

        //If rotation is not enabled end here
        if (this.enableRotation == false || this.enableRotation == true && this.rotationOnMouseHold == true && ABC_InputManager.GetKey(KeyCode.Mouse0) == false && ABC_InputManager.GetKey(KeyCode.Mouse1) == false)
            return; 

        //Retrieve both the controller and mouse inputs and add them together to get the axis changes (quick way to determine which is being used)

        //If x rotation is blocked then ABC is probably in ability before target mode (stops up and down movement)
        if (this.enableYRotation == true) {

            float yAxis = ((this.IsInputAvailable("RightStickVertical") ? ABC_InputManager.GetYAxis("RightStickVertical") : 0) + ABC_InputManager.GetYAxis("Mouse Y") * this.inputSensitivity);


#if ABC_Unity_Input_System_Integration
            if (this.rotationButton != string.Empty && this.IsInputAvailable(this.rotationButton)) 
                yAxis = ABC_InputManager.GetYAxis(this.rotationButton);
#endif


        //If set to invert the y axis then turn the positive to a negative or vice versa
        if (this.invertYAxis == false)
                yAxis = yAxis * -1;

            this.currentYRot += yAxis;
        }

        float additionalRotation = 0f;

        additionalRotation = ((this.IsInputAvailable("RightStickHorizontal") ? ABC_InputManager.GetXAxis("RightStickHorizontal") : 0) + ABC_InputManager.GetXAxis("Mouse X")) * this.inputSensitivity;

#if ABC_Unity_Input_System_Integration
        if (this.rotationButton != string.Empty && this.IsInputAvailable(this.rotationButton)) 
            additionalRotation =  ABC_InputManager.GetXAxis(this.rotationButton);
#endif


        this.currentXRot += additionalRotation;
     
        float clamp = this.clampAngle;

        if (this.InZoomMode() == true)
            clamp = this.zoomClampAngle; 

        //Apply the angle clamp on the X axis
        this.currentYRot = Mathf.Clamp(currentYRot, -clamp, clamp);

        Quaternion localRotation = Quaternion.Euler(this.currentYRot, this.currentXRot, 0f);
        transform.rotation = localRotation;


    }

    /// <summary>
    /// Will move the camera towards the follow target
    /// </summary>
    private void MoveCamera() {

        //If no follow target exists then end here
        if (FollowTarget == null)
            return;

        //If zooming input is being pressed then move position to the zoom target instantly

        if (this.InZoomMode() == true) {

            //If we have been in zoom for a little while then just make sure we instantly appear at the right position
            if (Time.time - this.zoomStartTime > 0.4f) {
                transform.position = this.FollowTarget.transform.position + this.meTransform.up * this.zoomTargetOffset.y + this.meTransform.forward * this.zoomTargetOffset.z + this.meTransform.right * this.zoomTargetOffset.x;

            } else {  
                
                // else move towards are normal follow target over a duration determined by the zoom in follow speed
                transform.position = Vector3.MoveTowards(transform.position, this.FollowTarget.transform.position + this.meTransform.up * this.zoomTargetOffset.y + this.meTransform.forward * this.zoomTargetOffset.z + this.meTransform.right * this.zoomTargetOffset.x, zoomInSpeed * Time.deltaTime);
            }


        } else {
            // move towards normal follow target over a duration determined by the camera follow speed
            float distance = cameraFollowSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, this.FollowTarget.transform.position + this.meTransform.up * this.followTargetOffset.y + this.meTransform.forward * this.followTargetOffset.z + this.meTransform.right * this.followTargetOffset.x, distance);

        }
      

    }




    /// <summary>
    /// Will lock on to an object by making sure the camera is always rotated to face the current target
    /// </summary>
    private void LockOnTargetHandler() {

        //If no lock on target exists then end here
        if (lockOnTarget == null)
            return;


        //Look at lock on target
        transform.LookAt(lockOnTarget.transform.position + lockOnTargetOffset);

    }





#endregion

    // ********************* Public Methods ********************
#region Public Methods


    /// <summary>
    /// Determines if the camera is in zoom mode
    /// </summary>
    /// <returns>True if camera is in zoom mode, else false</returns>
    public bool InZoomMode() {


        if (this.zoomKey != KeyCode.None && ABC_InputManager.GetKey(this.zoomKey) == true) {

            //Record zoom start time
            if (this.zoomStartTime == 0)
                this.zoomStartTime = Time.time; 

            return true;
        } else {

            //Reset zoom start time
            if (this.zoomStartTime > 0)
                this.zoomStartTime = 0f; 

            return false;
        }
       
    }

    /// <summary>
    /// Will follow the new object provided, setting up any ABC integrations
    /// </summary>
    /// <param name="NewFollowTarget">New Object to follow</param>
    public void SetFollowTarget(GameObject NewFollowTarget) {

        if (this.FollowTarget == NewFollowTarget)
            return;

        //Remove current follow target
        this.ClearFollowTarget();

        //Set the new follow target
        this.FollowTarget = NewFollowTarget;
        this.IntegrateWithABC();



    }

    /// <summary>
    /// Will clear the current follow target removing any event subscriptions to ABC
    /// </summary>
      public void ClearFollowTarget() {

            if (this.FollowTarget == null)
                return;

        this.FollowTarget = null;

        //Unsubscribe to any events
        if (this.followTargetABC != null) {
            this.followTargetABC.onTargetSet -= this.SetLockOnTarget;
            this.followTargetABC.onAbilityBeforeTarget -= this.ABCAbilityBeforeTargetToggleHandler;
        }


    }

    /// <summary>
    /// Sets the lock on target
    /// </summary>
    /// <param name="Target">Object to lock on too</param>
    public void SetLockOnTarget(GameObject NewLockOnTarget) {

        //If the target provided was null or not active then clear the lock on target
        if (NewLockOnTarget == null || NewLockOnTarget.activeInHierarchy == false)
            this.ClearLockOnTarget();


        //Only lock on if it's not the target we following 
        if (NewLockOnTarget != this.FollowTarget)
        this.lockOnTarget = NewLockOnTarget; 


    }

    /// <summary>
    /// Clears the lock on target
    /// </summary>
    public void ClearLockOnTarget() {

        if (this.lockOnTarget == null)
            return;


        //Look at lock on target
        transform.LookAt(lockOnTarget.transform);

        Vector3 rot = transform.localRotation.eulerAngles;
        currentXRot = rot.y;
        currentYRot = rot.x;

        //Clear target
        this.lockOnTarget = null;

    }


    /// <summary>
    /// Will handle what happens when ABC toggles ability before target on/off
    /// </summary>
    /// <param name="AbilityID">ID of ability that completed activating</param>
    /// <param name="Enabled">True to enable X Rotation else false to disable</param>
    public void ABCAbilityBeforeTargetToggleHandler(int AbilityID, bool Enabled) {

        //If ability before target is enabled then stop x rotation
        if (Enabled == true) {
            this.enableYRotation = false;

            if (this.hideLockMouse)
                Cursor.lockState = CursorLockMode.Confined;


        } else {

            this.enableYRotation = true;

            if (this.hideLockMouse)
                Cursor.lockState = CursorLockMode.Locked;

        }


    }

#endregion


    // ********************** Game ******************

#region Game

    void Start() {

        //Record transform
        this.meTransform = this.transform;

        //Record head and spine
        Animator ani = followTarget.GetComponent<Animator>(); 

        if (ani != null) {
            this.meSpine = ani.GetBoneTransform(HumanBodyBones.Spine);

            //Uncomment to get a right eye perspective
            //this.meHead = ani.GetBoneTransform(HumanBodyBones.RightEye);

            if (this.meHead == null)
                this.meHead = ani.GetBoneTransform(HumanBodyBones.Head);

        }

        //Retrieve starting rotation to place camera behind the follow target
        Vector3 rot = FollowTarget.transform.localRotation.eulerAngles;
        currentXRot = rot.y;
        currentYRot = rot.x;






    }

    private void OnEnable() {

        this.IntegrateWithABC();

        //Hide and lock mouse
        if (this.hideLockMouse) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }


    void Update() {

        //Hide and lock mouse
        if (this.hideLockMouse == false && Cursor.visible == false) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        //Rotate Camera
        this.RotateCamera();

        //Lock on to any targets
        this.LockOnTargetHandler();

    }


    private void LateUpdate() {

        //If in FPS or Crosshair mode then animate the spine to face the rotation of the camera
        if (this.meSpine != null && (this.FPSMode == true || this.InZoomMode() == true)) {
            this.meSpine.localRotation *= Quaternion.Euler(this.currentYRot / 3.5f, 0, Camera.main.transform.eulerAngles.x);
        }


        //Move camera
        MoveCamera();



    }


    private void OnDisable() {

        //Unsubscribe to any events
        if (this.followTargetABC != null) {
            this.followTargetABC.onTargetSet -= this.SetLockOnTarget;
            this.followTargetABC.onAbilityBeforeTarget -= this.ABCAbilityBeforeTargetToggleHandler;

        }

    }

#endregion
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Will allow the entity to fly freely around. 
/// WASD: Movement
/// Space: Climb
/// E: Drop
/// Shift: Move Faster
/// Control: Move Slower
/// </summary>
public class FlyCam : MonoBehaviour{
 
    /// <summary>
    /// Camera Sensitivity
    /// </summary>
	public float cameraSensitivity = 90;

    /// <summary>
    /// Speed of climbing
    /// </summary>
	public float climbSpeed = 4;

    /// <summary>
    /// Normal move speed
    /// </summary>
	public float normalMoveSpeed = 10;

    /// <summary>
    /// How much slowness is applied
    /// </summary>
	public float slowMoveFactor = 0.25f;

    /// <summary>
    /// How much speed is applied during fast move
    /// </summary>
	public float fastMoveFactor = 3;
 
    /// <summary>
    /// Current Rotation X
    /// </summary>
	private float rotationX = 0.0f;

    /// <summary>
    /// Current Rotation Y
    /// </summary>
	private float rotationY = 0.0f;

    /// <summary>
    /// Records entities rotation
    /// </summary>
    private void RecordRotation() {
        rotationX += ABC_InputManager.GetXAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        rotationY += ABC_InputManager.GetYAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

    }

    /// <summary>
    /// Will move the entity depending on the input
    /// </summary>
    private void Move() {

        if (ABC_InputManager.GetKey(KeyCode.LeftShift) || ABC_InputManager.GetKey(KeyCode.RightShift)) {

            transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * ABC_InputManager.GetYAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * ABC_InputManager.GetXAxis("Horizontal") * Time.deltaTime;

        } else if (ABC_InputManager.GetKey(KeyCode.LeftControl) || ABC_InputManager.GetKey(KeyCode.RightControl)){

            transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * ABC_InputManager.GetYAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * ABC_InputManager.GetXAxis("Horizontal") * Time.deltaTime;

        } else {

            transform.position += transform.forward * normalMoveSpeed * ABC_InputManager.GetYAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * normalMoveSpeed * ABC_InputManager.GetXAxis("Horizontal") * Time.deltaTime;
        }


        if (ABC_InputManager.GetKey(KeyCode.Space)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
        if (ABC_InputManager.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
    }


    void Start (){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
	}

 
	void Update (){

        this.RecordRotation();
        this.Move();
	
	}
}
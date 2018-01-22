using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float mouseRotationSpeedX;
    public float mouseRotationSpeedY;
    public float walkSpeed;
    public float sprintSpeed;

    private Player playerScript;
    private CharacterController playerController;
    private float verticalRotation;
    private float currentMoveSpeed;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        playerController = GetComponent<CharacterController>();
        playerScript = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerLookAround();
	}

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerLookAround()
    {
        //Look right and left
        if(Input.GetAxis("MouseX") > 0 || Input.GetAxis("MouseX") < 0)
        {
            this.transform.Rotate(0, mouseRotationSpeedY * Input.GetAxis("MouseX") * Time.deltaTime, 0);
        }
        //Look up and down
        if(Input.GetAxis("MouseY") > 0 || Input.GetAxis("MouseY") < 0)
        {
            verticalRotation += Input.GetAxis("MouseY") * mouseRotationSpeedX * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }
    
    public void PlayerMove()
    {
        Vector3 playerMoveLeftRight = transform.right * Input.GetAxis("Horizontal") * currentMoveSpeed * Time.deltaTime;
        Vector3 playerMoveForwardBackward = transform.forward * Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;

        playerController.SimpleMove(playerMoveLeftRight);
        playerController.SimpleMove(playerMoveForwardBackward);

        //Walk
        if (playerScript.playerState == PlayerState.Walking)
        {
            currentMoveSpeed = walkSpeed;
        }

        //Sprint
        if (playerScript.playerState == PlayerState.Sprinting)
        {
            currentMoveSpeed = sprintSpeed;
        }
    }
}

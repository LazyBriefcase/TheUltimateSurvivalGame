using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float mouseRotationSpeedX;
    public float mouseRotationSpeedY;
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;

    private CharacterController playerController;
    private float verticalRotation;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerController = GetComponent<CharacterController>();
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

    void PlayerMove()
    {
        Vector3 playerMoveForwardBackward = transform.forward * Input.GetAxis("Vertical") * verticalMoveSpeed * Time.deltaTime;
        Vector3 playerMoveLeftRight = transform.right * Input.GetAxis("Horizontal") * horizontalMoveSpeed * Time.deltaTime;

        playerController.SimpleMove(playerMoveForwardBackward);
        playerController.SimpleMove(playerMoveLeftRight);
    }
}

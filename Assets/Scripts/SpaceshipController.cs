using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    //Firing Lasers Array
    [SerializeField] GameObject[] lasers;

    //Movement Fields
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;

    //Rotation Fields
    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 4f;
    [SerializeField] float controlRollFactor = -15f;


    PlayerInput playerInput;

    float xThrow;
    float yThrow;
    bool isFiringPressed, isFiring = false;



    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Controls.Movement.started += OnMovementInput;
        playerInput.Controls.Movement.canceled += OnMovementInput;
        playerInput.Controls.Movement.performed += OnMovementInput;
        playerInput.Controls.Firing.started += OnFiringInput;
        playerInput.Controls.Firing.canceled += OnFiringInput;
    }

    void OnEnable()
    {
        playerInput.Controls.Enable();
    }

    void OnDisable()
    {
        playerInput.Controls.Disable();
    }

    void Update()
    {
        handleMovement();
        handleRotation();
        handleFiring();
    }

    void handleMovement()
    {
        float newXPos = transform.localPosition.x + xThrow * Time.deltaTime * moveSpeed;
        float newYPos = transform.localPosition.y + yThrow * Time.deltaTime * moveSpeed;

        newXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        newYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    void handleRotation()
    {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float controlPitch = yThrow * controlPitchFactor;

        float pitch = positionPitch + controlPitch;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void handleFiring()
    {
        if(isFiringPressed && !isFiring){
            isFiring = true;
            toggleLasers(true);
        }else if(!isFiringPressed && isFiring){
            isFiring = false;
            toggleLasers(false);
        }
    }

    void toggleLasers(bool enabled)
    {
        foreach (GameObject laser in lasers){
            var emmissionModule = laser.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = enabled;
        }
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        xThrow = currentMovementInput.x;
        yThrow = currentMovementInput.y;
    }

    void OnFiringInput(InputAction.CallbackContext context)
    {
        isFiringPressed = context.ReadValueAsButton();
    }

}

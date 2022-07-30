using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    //Firing Lasers Array
    [Tooltip("Firing lasers list")] [SerializeField] GameObject[] lasers;

    //Movement Fields
    [Header("Movement settings")]
    
    [Tooltip("How fast ship moves up and down upon player input")]
    [SerializeField] float moveSpeed = 25f;
    [Tooltip("How far the ship can move in the X axis")]
    [SerializeField] float xRange = 5f;
    [Tooltip("How far the ship can move in the Y axis")]
    [SerializeField] float yRange = 3f;

    //Rotation Fields
    [Tooltip("Enable Position and Input Tuning")]
    [SerializeField] bool enableTuning = true;

    [Header("Screen position based tuning")]
    [Tooltip("Rotation on the X axis based on player position")]
    [SerializeField] float positionPitchFactor = -4f;
    [Tooltip("Rotation on the Y axis based on player position")]
    [SerializeField] float positionYawFactor = 4f;


    [Header("Player input based tuning")]
    [Tooltip("Rotation on the X axis based on player input")]
    [SerializeField] float controlPitchFactor = -10f;
    [Tooltip("Rotation on the Z axis based on player input")]
    [SerializeField] float controlRollFactor = -15f;


    UserInput userInput;

    float xThrow;
    float yThrow;
    bool isFiringPressed, isFiring = false;



    void Awake()
    {
        userInput = new UserInput();

        userInput.Controls.Movement.started += OnMovementInput;
        userInput.Controls.Movement.canceled += OnMovementInput;
        userInput.Controls.Movement.performed += OnMovementInput;
        userInput.Controls.Firing.started += OnFiringInput;
        userInput.Controls.Firing.canceled += OnFiringInput;
    }

    void OnEnable()
    {
        userInput.Controls.Enable();
    }

    void OnDisable()
    {
        userInput.Controls.Disable();
    }

    void Update()
    {
        handleMovement();
        if(enableTuning) handleRotation();
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

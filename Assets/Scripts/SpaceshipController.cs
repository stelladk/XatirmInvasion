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
        resumeTimeline();
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

    // TODO: Move function elsewhere
    void resumeTimeline()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            TimelineManager.Instance.ResumeTimeline();
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

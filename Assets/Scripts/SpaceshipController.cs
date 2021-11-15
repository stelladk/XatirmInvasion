using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;

    PlayerInput playerInput;

    float xThrow;
    float yThrow;



    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Controls.Movement.started += OnMovementInput;
        playerInput.Controls.Movement.canceled += OnMovementInput;
        playerInput.Controls.Movement.performed += OnMovementInput;
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
    }

    void handleMovement(){
        float newXPos = transform.localPosition.x + xThrow * Time.deltaTime * moveSpeed;
        float newYPos = transform.localPosition.y + yThrow * Time.deltaTime * moveSpeed;

        newXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        newYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    void OnMovementInput(InputAction.CallbackContext context){
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        xThrow = currentMovementInput.x;
        yThrow = currentMovementInput.y;
    }

}

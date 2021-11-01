using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
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
        float newXPos = transform.localPosition.x + xThrow;
        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);
        Debug.Log(transform.localPosition);
    }

    void OnMovementInput(InputAction.CallbackContext context){
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        xThrow = currentMovementInput.x;
        yThrow = currentMovementInput.y;
    }

}

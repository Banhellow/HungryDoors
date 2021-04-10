using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float movementSpeed = 10;
    [ReadOnly] public Vector2 currentMovementInput;
    [ReadOnly] public bool useItemRequaired = false;
    [ReadOnly] public bool pickupItemRequaired = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ReadInput();
        HandleInput();
    }

    private void ReadInput()
    {
        currentMovementInput.x = Input.GetAxisRaw("Horizontal");
        currentMovementInput.y = Input.GetAxisRaw("Vertical");
        currentMovementInput.Normalize();

        if (Input.GetMouseButtonDown(0))
            useItemRequaired = true;

        if (Input.GetMouseButtonDown(1))
            pickupItemRequaired = true;
    }

    private void HandleInput()
    {
        characterController.Move(new Vector3() * movementSpeed * Time.deltaTime);

        if (useItemRequaired)
        {
            Debug.Log("Item used tmp");
            useItemRequaired = false;
        }

        if (pickupItemRequaired)
        {
            Debug.Log("Item pickup tmp");
            pickupItemRequaired = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
}

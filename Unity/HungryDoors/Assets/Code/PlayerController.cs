using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Camera mainCamera;
    private Plane groundPlane;
    private Vector3 lookAtPosition;

    public Transform playerRotatorTR;
    public float movementSpeed = 10;
    [ReadOnly] public Vector2 currentMovementInput;
    [ReadOnly] public bool useItemRequaired = false;
    [ReadOnly] public bool pickupItemRequaired = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        ReadInput();
        HandleInput();
    }

    private void ReadInput()
    {
        // movement
        currentMovementInput.x = Input.GetAxisRaw("Horizontal");
        currentMovementInput.y = Input.GetAxisRaw("Vertical");
        currentMovementInput.Normalize();

        // rotation
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            lookAtPosition = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, lookAtPosition, Color.blue);
        }

        if (Input.GetMouseButtonDown(0))
            useItemRequaired = true;

        if (Input.GetMouseButtonDown(1))
            pickupItemRequaired = true;
    }

    private void HandleInput()
    {
        // movement rotateed by 45deg to compensate for isometric camera.
        // gravity added manualy
        characterController.Move((Quaternion.Euler(0, 45, 0) * new Vector3(currentMovementInput.x, Physics.gravity.y, currentMovementInput.y)) * movementSpeed * Time.deltaTime);

        playerRotatorTR.LookAt(new Vector3(lookAtPosition.x, playerRotatorTR.position.y, lookAtPosition.z));

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

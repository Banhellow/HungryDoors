using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Character
{
    [Inject]
    public  GUIManager guiManager;
    private LifeController myLifeController;

    [Header("Character controller")]
    public Transform playerRotatorTR;
    public float movementSpeed = 10;
    [ReadOnly] public Vector2 currentMovementInput;
    [ReadOnly] public bool useItemRequaired = false;
    [ReadOnly] public bool pickupItemRequaired = false;

    [Header("Items")]
    [ReadOnly] public Item currentItem;
    public LayerMask pickupLayerMask;
    public Transform pickupSphereCenter;
    public float pickupSphereRadius = 1;
    public Transform rightArmHandleTR;


    private CharacterController characterController;
    private Camera mainCamera;
    private Plane groundPlane;
    private Vector3 lookAtPosition;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myLifeController = GetComponent<LifeController>();

        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (isDead) 
            return;

        ReadInput();
        HandleInput();
        HandleAnimator();
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

            if (currentItem == null)
                return;

            switch (currentItem.data.animationType)
            {
                case AnimationType.attack:
                    animator.SetTrigger(attackParam);
                    break;
                case AnimationType.shoot:
                    animator.SetTrigger(shootParam);
                    break;
            }
            currentItem = currentItem.Use();

        }

        if (pickupItemRequaired)
        {
            Debug.Log("Item pickup tmp");
            pickupItemRequaired = false;
            animator.SetTrigger(pickupParam);

            Collider[] allColliders = Physics.OverlapSphere(pickupSphereCenter.position, pickupSphereRadius, pickupLayerMask);
            for (int i = 0; i < allColliders.Length; i++)
            {
                Item item = allColliders[i].GetComponent<Item>();
                if (item != null)
                {
                    rightArmHandleTR.BB_DestroyAllChildren();
                    currentItem = item;
                    item.OnPickup(rightArmHandleTR);
                    guiManager.UpdatePlayerItem(item);
                    return;
                }
            }

        }
    }

    private void HandleAnimator()
    {
        if (currentMovementInput.magnitude > 0.1f)
            animator.SetBool(isMovingParam, true);
        else
            animator.SetBool(isMovingParam, false);
    }

    public override void OnHealthUpdated(int healthChange)
    {
        base.OnHealthUpdated(healthChange);
        guiManager.UpdatePlayerLife(myLifeController.GetCurrentHealth());
    }

    public override void Die()
    {
        base.Die();
        Debug.Log($"Player dead.");
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }
}

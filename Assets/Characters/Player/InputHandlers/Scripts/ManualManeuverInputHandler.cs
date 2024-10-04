﻿using Characters.InputHandlers.Scripts;
using Characters.Player.Scripts;
using Characters.Player.Scripts.Movement;
using Core.InputHandler.Scripts;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player.InputHandlers.Scripts
{
    public class ManualManeuverInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] MovementManager movementManager;
        [FormerlySerializedAs("playerRotationInputHandler")] [SerializeField]
        RotatePlayerDirection rotatePlayerDirection;
        [SerializeField] CategoryObject pistolCategoryObject;
        [SerializeField] CategoryObject consumableCategoryObject;


        void Start()
        {
            if (movementManager == null) movementManager = MovementManager.Instance;
            if (rotatePlayerDirection == null)
                rotatePlayerDirection = PlayerCharacter.Instance.GetComponent<RotatePlayerDirection>();
        }

        void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            HandleMovementInput();
            HandleItemUseInput();
        }

        void HandleMovementInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementManager.ExecuteMoveUpCommand();
                rotatePlayerDirection.ExecuteRotateUpCommand();
            }

            if (Input.GetKey(KeyCode.S))
            {
                movementManager.ExecuteMoveDownCommand();
                rotatePlayerDirection.ExecuteRotateDownCommand();
            }

            if (Input.GetKey(KeyCode.A))
            {
                movementManager.ExecuteMoveLeftCommand();
                rotatePlayerDirection.ExecuteRotateLeftCommand();
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementManager.ExecuteMoveRightCommand();
                rotatePlayerDirection.ExecuteRotateRightCommand();
            }
        }

        void HandleItemUseInput()
        {
            if (PlayerCharacter.Instance.equippedItem == null) return;


            if (pistolCategoryObject.Contains(PlayerCharacter.Instance.equippedItem))
            {
                if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
                    PlayerCharacter.Instance.PerformAttack(null);
                else if (Input.GetMouseButtonUp(0)) PlayerCharacter.Instance.CeaseUsing();
                else if (Input.GetMouseButton(1))
                    PlayerCharacter.Instance.EnterCombatReadyState();

                else
                    PlayerCharacter.Instance.ReturnToExploreState();
            }

            if (consumableCategoryObject.Contains(PlayerCharacter.Instance.equippedItem))
                if (Input.GetKey(KeyCode.Return))
                    PlayerCharacter.Instance.UseConsumable();
        }
    }
}

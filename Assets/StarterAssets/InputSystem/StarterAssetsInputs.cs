using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public Vector2 aim;
		public bool jump;
		public bool sprint;
		public bool block;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public Action<bool> Block;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(CallbackContext value)
		{
			MoveInput(value.ReadValue<Vector2>());
		}
		public void OnAim(CallbackContext value)
		{
			AimInput(value.ReadValue<Vector2>());
		}

        public void OnJump(CallbackContext value )
		{
			JumpInput(value.ReadValueAsButton());
		}

		public void OnSprint(CallbackContext value)
		{
			SprintInput(value.control.IsPressed());
		}
		public void OnBlock(CallbackContext value)
		{
			BlockInput(value.control.IsPressed());
		}

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}
        private void AimInput(Vector2 vector2)
        {
            aim=vector2;
        }

        public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

        private void BlockInput(bool v)
        {
            block = v;
			Block?.Invoke(v);
        }

        private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}
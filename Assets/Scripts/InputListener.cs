using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private InputControls inputControls;
    private InputControls.CubeControlActions cubeControlsActions;

    private CubeMovement cubeMovement;
    public CubeMovement CubeMovement
    {
        get
        {
            if (cubeMovement == null)
            {
                cubeMovement = GetComponent<CubeMovement>();
            }

            return cubeMovement;
        }
    }

    public static Action onSpawn;
    public static Action onLiftUpCubeSpawn;

    private void OnEnable()
    {
        inputControls = new InputControls();
        cubeControlsActions = inputControls.CubeControl;

        inputControls.Enable();

        cubeControlsActions.StopCube.started += StopCube_Started;
        cubeControlsActions.StopCube.canceled += StopCube_Canseled;
    }

    private void StopCube_Started(InputAction.CallbackContext obj)
    {
        CubeMovement.StopMovement();

        CubeMovement.DisablingScript();
    }

    private void StopCube_Canseled(InputAction.CallbackContext obj)
    {
        onLiftUpCubeSpawn?.Invoke();

        onSpawn?.Invoke();
    }

    private void OnDisable()
    {
        inputControls.Disable();

        cubeControlsActions.StopCube.started -= StopCube_Started;
        cubeControlsActions.StopCube.canceled -= StopCube_Canseled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[AddComponentMenu("_DeepLOM/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private SteamVR_Action_Vector2 joystickInput;
    [SerializeField] private SteamVR_Action_Boolean turnLeft;
    [SerializeField] private SteamVR_Action_Boolean turnRight;
    [SerializeField] private Transform camera;
    [SerializeField] private RotationAngles rotationAngle = RotationAngles.Forty_five;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        float angle = (float)rotationAngle;
        turnLeft.onStateUp += (action, source) => Rotate(-angle);
        turnRight.onStateUp += (action, source) => Rotate(angle);
    }

    private void FixedUpdate()
    {
        #region CharController
        characterController.height = Player.instance.eyeHeight;
        float distToFloor = Vector3.Dot(camera.localPosition, Vector3.up);
        characterController.center = camera.localPosition - 0.5f * distToFloor * Vector3.up;
        #endregion

        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(joystickInput.axis.x, 0, joystickInput.axis.y));
        characterController.Move(Vector3.ProjectOnPlane(direction, Vector3.up) * speed - new Vector3(0, 10, 0));
    }

    private void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
    }
}

enum RotationAngles
{
    Thirty = 30,
    Forty_five = 45,
    Ninety = 90
}


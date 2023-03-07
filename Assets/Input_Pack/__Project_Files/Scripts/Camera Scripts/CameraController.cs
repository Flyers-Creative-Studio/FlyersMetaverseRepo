using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public Transform target;
    public FixedTouchField TouchField;
    private bool isFirstpersonBool;
    //public PlayerView playerViewEnum;
    public LayerMask withOutCamreaCulling;
    public LayerMask withCameraCulling;
    public bool isFirstPerson
    {
        get
        {
            return isFirstpersonBool;
		}
        set
        {
            isFirstpersonBool = value;
            if (value)
			{
                //delay of total blend time of two camera views
                HelperUtil.CallAfterDelay(() => 
                {
                    //playerViewEnum = PlayerView.fpv;
                    Camera.main.cullingMask = withOutCamreaCulling;
                }, 1f);
                
                tpsCamera.Priority = 0;
                fpsCamera.Priority = 1;
            }
            else
			{
				//delay of total blend time of two camera views
				//playerViewEnum = PlayerView.tpv;
				Camera.main.cullingMask = withCameraCulling;
				fpsCamera.Priority = 0;
				tpsCamera.Priority = 1;
			}
        }
	}
    public float CameraAngleSpeed = 1;
    public AudioSource bgMusic;
    public CinemachineVirtualCamera tpsCamera;
    public CinemachineVirtualCamera fpsCamera;

    // cinemachine
    public GameObject CinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float CameraAngleOverride;

    [Tooltip("How far in degrees can you move the camera top")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    private void Start()
    {
        bgMusic.Play();
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    void LateUpdate()
    {
        _cinemachineTargetYaw += TouchField.TouchDist.x * CameraAngleSpeed;
        _cinemachineTargetPitch -= TouchField.TouchDist.y * CameraAngleSpeed;
        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch + CameraAngleOverride,
            -_cinemachineTargetYaw, 0.0f);
    }
}

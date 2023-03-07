using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
  
    [SerializeField] private int jetPackForce = 20;
    private bool isUsing;
   
    private Animator _animator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    public bool FlyEnabled;

    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    public GameObject _mainCamera;
    private bool _hasAnimator;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private Vector3 currentAngle;
    void Start()
    {
       
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        currentAngle = this.transform.GetChild(1).transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
       
       // if (Input.GetButton("Fire1"))
        if (FlyEnabled)
        {
            StartJetPack();
        }
        _hasAnimator = TryGetComponent(out _animator);
        Move();
    }
    void StartJetPack()
    {
        // rb.AddForce(Vector3.up * jetPackForce);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * jetPackForce);

        // move the player

            if (_mainCamera.GetComponent<CameraController>()._cinemachineTargetPitch == 0 && this.transform.localPosition.y < 300)
            {
                _controller.Move(Vector3.up * Time.deltaTime * jetPackForce);
               this.transform.GetChild(1).transform.localEulerAngles = new Vector3(-20, 0, 0);
            }
            else
            {
                if (_mainCamera.GetComponent<CameraController>()._cinemachineTargetPitch >10)
                {
                    _controller.Move(Vector3.down * Time.deltaTime * jetPackForce);
                    currentAngle = new Vector3(Mathf.LerpAngle(currentAngle.x,20, Time.deltaTime*2f), 0, 0);


                this.transform.GetChild(1).transform.localEulerAngles = currentAngle;
                //this.transform.GetChild(1).transform.localEulerAngles = new Vector3(20, 0, 0);
                }
                else if (_mainCamera.GetComponent<CameraController>()._cinemachineTargetPitch < 10)
                {

                if (this.transform.localPosition.y < 300) {
                    _controller.Move(Vector3.up * Time.deltaTime * jetPackForce);
                    currentAngle = new Vector3(Mathf.LerpAngle(currentAngle.x, -20, Time.deltaTime*2f), 0, 0);


                    this.transform.GetChild(1).transform.localEulerAngles = currentAngle;
                  //  this.transform.GetChild(1).transform.localEulerAngles = new Vector3(-20, 0, 0);
                }


                }
            }
        
       
       

        isUsing = true;
        //ParticleLeft.GetComponent<ParticleSystem>().Play();
        //ParticleRight.GetComponent<ParticleSystem>().Play();
    }


    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

           

        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        // update animator if using character
        //if (_hasAnimator)
        //{
        //    _animator.SetFloat(_animIDSpeed, _animationBlend);
        //    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        //}
    } 
  
}

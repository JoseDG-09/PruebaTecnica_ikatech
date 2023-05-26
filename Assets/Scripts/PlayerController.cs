using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float speedRotation = 0;
    [SerializeField] private float force = 0;

    private PlayerControls _playerControls = null;
    private Vector3 direction = Vector3.zero;
    private Rigidbody _rigidbody = null;
    private bool _jump = true;

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
        _playerControls.Gameplay.Move.performed += ReadInput;
        _playerControls.Gameplay.Move.canceled += ReadInput;

        _playerControls.Gameplay.Jump.performed += JumpPlayer;
        _playerControls.Gameplay.Jump.canceled += JumpPlayer;
    }

    void Update()
    {
        MovePlayer();
    }

    private void ReadInput(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        direction.z = input.y;
        direction.x = input.x;
    }

    private void MovePlayer()
    {
        //transform.position += direction * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * direction.z * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, direction.x * speedRotation * Time.deltaTime);
        
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        if (_jump == true)
        {
            _jump = false;
            var input = context.ReadValue<float>();
            _rigidbody.AddForce(Vector3.up * input * force);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && _jump == false)
        {
            _jump = true;
        }
    }
}
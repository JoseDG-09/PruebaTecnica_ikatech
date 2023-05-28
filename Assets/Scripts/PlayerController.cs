using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject panelPause;
    public TMP_Text scoreText;
    private int amountCoins;

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
        
        _playerControls.Gameplay.Pause.performed += PauseGame;
        _playerControls.Gameplay.Pause.canceled += PauseGame;
    }

    private void Start()
    {
        amountCoins = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = amountCoins.ToString();
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
        if (_jump == false)
        {
            _jump = true;
            var input = context.ReadValue<float>();
            _rigidbody.AddForce(Vector3.up * input * force);
        }
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void RegisterCoin()
    {
        amountCoins = PlayerPrefs.GetInt("Score");
        amountCoins++;
        PlayerPrefs.SetInt("Score", amountCoins);
        SpawnCoins.instance.SpawnObject();
        scoreText.text = amountCoins.ToString();
        if (amountCoins == 20)
        {
            GameManager.instance.LoadScene(2);
            PlayerPrefs.SetInt("Score", 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && _jump == true)
        {
            _jump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            RegisterCoin();
            other.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldScript : MonoBehaviour
{
    [Header("----References-------------")]
    [SerializeField] Camera _camera;
    [SerializeField] Transform _shieldSource;
    [SerializeField] Transform _shieldTarget;
    [SerializeField] Transform _shield;
    [SerializeField] SpriteRenderer _shieldRenderer;
    [SerializeField] CapsuleCollider2D _shieldCollider;

    [Header("----Values-------------")]
    [SerializeField] bool[] _isFingerDown;

    private ShieldInput _shieldInput;

    private void Awake()
    {
        _shieldInput = new ShieldInput();
        _shieldSource.gameObject.SetActive(false);
    }

    private void Start()
    {
        //Finger2(false);
    }
    private void Update()
    {
        _shieldInput.Shield.FingerTouch1.performed += ctx => Finger1(true);
        _shieldInput.Shield.FingerTouch1.canceled += ctx => Finger1(false);

        _shieldInput.Shield.FingerTouch2.performed += ctx => Finger2(true);
        _shieldInput.Shield.FingerTouch2.canceled += ctx => Finger2(false);

        UpdateShieldSource();
        UpdateShieldTarget();
        ManageShield();
    }


    private void UpdateShieldSource()
    {
        if (_isFingerDown[0])
        {
            Vector2 fingerScreenPosition = _shieldInput.Shield.FingerPosition1.ReadValue<Vector2>();
            Vector3 fingerWorldPosition = new Vector3(_camera.ScreenToWorldPoint(fingerScreenPosition).x, _camera.ScreenToWorldPoint(fingerScreenPosition).y, 0f);

            _shieldSource.position = fingerWorldPosition;

            Vector3 vectorTowardsTarget = _shieldTarget.position - _shieldSource.position;
            float rotationTowardsTarget = Mathf.Atan2(vectorTowardsTarget.y, vectorTowardsTarget.x) * Mathf.Rad2Deg - 90f;
            _shieldSource.rotation = Quaternion.Euler(new Vector3(0f,0f, rotationTowardsTarget));
        }
    }
    private void UpdateShieldTarget()
    {
        if (_isFingerDown[1])
        {
            Vector2 fingerScreenPosition = _shieldInput.Shield.FingerPosition2.ReadValue<Vector2>();
            Vector3 fingerWorldPosition = new Vector3(_camera.ScreenToWorldPoint(fingerScreenPosition).x, _camera.ScreenToWorldPoint(fingerScreenPosition).y, 0f);

            _shieldTarget.position = fingerWorldPosition;
        }
    }

    private void ManageShield()
    {
        Debug.DrawLine(_shieldSource.position, _shieldTarget.position, Color.red);
        float shieldDistance = Vector2.Distance(_shieldTarget.position, _shieldSource.position);
        float shieldLength = (shieldDistance * 2 + 1f);

        _shieldRenderer.size = new Vector3(1f, shieldLength);

        _shieldCollider.size = new Vector2(1f, shieldLength);
        _shieldCollider.offset = new Vector2(0f, shieldLength/2 - 0.5f);
    }

    private void Finger1(bool isPressed)
    {
        _isFingerDown[0] = isPressed;
        _shieldSource.gameObject.SetActive(isPressed);
    }
    private void Finger2(bool isPressed)
    {
        _isFingerDown[1] = isPressed;
        UpdateShieldSource();
        UpdateShieldTarget();
        _shield.gameObject.SetActive(isPressed);
    }

    private void OnEnable()
    {
        _shieldInput.Enable();
    }
    private void OnDisable()
    {
        _shieldInput.Disable();
    }
}
